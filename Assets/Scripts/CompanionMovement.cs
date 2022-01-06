
using System;
using System.Collections;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class CompanionMovement : MonoBehaviour
{
    public Transform player;
    
    private static readonly int speedForAnimations = Animator.StringToHash("speed");

    private Animator _animationController;
    private Rigidbody _rigidbody;
    public UnityEngine.AI.NavMeshAgent _navMeshAgent;
    
    FiniteStateMachine<CompanionMovement> stateMachine;

    public float playerMaxDistance = 5;
    public float weaponRangeDistance = 8;
    public int arrows = 10;
    public float visionRangeDistance = 25;
    public bool weaponIsCharged = true;
    public GameObject currentTarget = null;
    public float Speed;

    public FiniteStateMachine<CompanionMovement> GetFSM()
    {
      return stateMachine;
    }

    public IEnumerable<GameObject> SeeEnemies()
    {
      return GameObject.FindGameObjectsWithTag("Enemy").Where(enemy => Vector3.Distance(transform.position, enemy.transform.position) < visionRangeDistance);
    }

    // An enemy can be attacked if the companion can locate itself than weaponRangeDistance
    // while maintaining itself closer than playerMaxDistance to the player
    public IEnumerable<GameObject> EnemiesThatCanBeAttacked()
    {
      return GameObject.FindGameObjectsWithTag("Enemy")
                     .Where(enemy => Vector3.Distance(transform.position, enemy.transform.position) < visionRangeDistance
                               && Vector3.Distance(player.position, enemy.transform.position) < (weaponRangeDistance + playerMaxDistance));
    }

    // Enemies that are closer than weaponRangeDistance to the companion
    public IEnumerable<GameObject> EnemiesInWeaponRange()
    {
      return GameObject.FindGameObjectsWithTag("Enemy")
               .Where(enemy => Vector3.Distance(transform.position, enemy.transform.position) < weaponRangeDistance);
    }
    
    public float DistanceWithPlayer()
    {
        return Vector3.Distance(this.transform.position, this.player.position);
    }

    // Returns the closest enemy that can be attacked by the companion
    public GameObject GetClosestEnemy()
    {
        // Iterate over all those enemies that can be attacked
        // (close enough to player so that the companion can attack them
        // while not being to far from the player)
        IEnumerable<GameObject> closeEnemies = EnemiesThatCanBeAttacked();
        GameObject targetEnemy = null;
        float minimumDistance = float.MaxValue;
        // Find the closest enemy to player that can be attacked
        foreach (GameObject enemy in closeEnemies)
        {
            float distanceToPlayer = Vector3.Distance(enemy.transform.position, this.player.position);
            if (distanceToPlayer < minimumDistance)
            {
                minimumDistance = distanceToPlayer;
                targetEnemy = enemy;
            }
        }
        return targetEnemy;
    }

    // Simple function to stop any walking animation (NavMeshAgent too)
    public void StopWalking()
    {
        this._navMeshAgent.SetDestination(this.transform.position);
        this._animationController.SetFloat(speedForAnimations, 0);
    }

    // Simple function to walk to a certain destination using the NavMeshAgent
    public void WalkTo(Vector3 destination)
    {
        this._navMeshAgent.SetDestination(destination);
        this._animationController.SetFloat("speed", this._rigidbody.velocity.magnitude);
        this.transform.LookAt(destination);
    }

    IEnumerator OnCompleteAttackAnimation()
    {
        yield return new WaitUntil(() => this._animationController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
        this._animationController.SetBool("shoot", false);
    }

    public bool IsAttackAnimationFinished()
    {
        return this._animationController.GetCurrentAnimatorStateInfo(0).IsName("Shoot")
               && this._animationController.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f;
    }

    public void Attack()
    {
        this._animationController.SetBool("shoot", true);
        arrows -= 1;
    }

    public void Point()
    {
        this._animationController.SetBool("noAmmo", true);
    }

    public void FinishPointing()
    {
        this._animationController.SetBool("noAmmo", false);
    }

    public void FinishAttack()
    {
		this._animationController.SetBool("shoot", false);
    }

    // Start is called before the first frame update
    void Start()
    {
        this._animationController = GetComponent<Animator>();
        this._rigidbody = GetComponent<Rigidbody>();
        this._navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        
        this.stateMachine = new FiniteStateMachine<CompanionMovement>(this);
        GetFSM().ChangeState(IdleState.Instance);
    }

    // Update is called once per frame
    void Update ()
    {
        GetFSM().Update();

    }
}
