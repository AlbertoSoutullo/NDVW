using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class followPlayer : MonoBehaviour
{
    float attackRange = 6;
    Transform enemy;

    NavMeshAgent agent;

    public Transform player;
    public float maxTime = 1.0f;
    public float maxDistance = 1.0f;
    Animator animator;
    float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0.0f)
        {
            float sqDistance = (player.position - agent.destination).sqrMagnitude;
            if(sqDistance > maxDistance * maxDistance)
            { 
                agent.destination = player.position ;

            }
            timer = maxTime;
        }
       
        animator.SetFloat("Speed", agent.velocity.magnitude);

       
        float distance = Vector3.Distance(animator.transform.position, enemy.position);
        if (distance > 10)
            animator.SetBool("isAttacking", false);
        if (distance < attackRange)
        {
            animator.transform.LookAt(enemy);
            animator.SetBool("isAttacking", true);
        }
    }
}
