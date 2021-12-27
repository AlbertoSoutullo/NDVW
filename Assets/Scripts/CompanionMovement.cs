using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class CompanionMovement : MonoBehaviour
{
	FiniteStateMachine<CompanionMovement> stateMachine;

	public float playerMaxDistance = 5;
	public float weaponRangeDistance = 8;
	public int arrows = 10;
	public float visionRangeDistance = 25;
	public bool weaponIsCharged = true;
	public GameObject currentTarget = null;
	public float Speed;

	public Transform player;

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

	// Start is called before the first frame update
	void Start()
	{
		stateMachine = new FiniteStateMachine<CompanionMovement>(this);
		stateMachine.CurrentState = IdleState.Instance;
	}

	// Update is called once per frame
	void Update()
	{
		stateMachine.Update();
	}
}
