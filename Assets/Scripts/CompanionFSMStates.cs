using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class IdleState : FSMState<CompanionMovement>
{
	static readonly IdleState instance = new IdleState();
	public static IdleState Instance { get { return instance; } }

	static IdleState()
	{
	}
	IdleState()
	{
	}

	public override void Enter(CompanionMovement companion)
	{
		Debug.Log("Entering IdleState");
	}

	public override void Execute(CompanionMovement companion)
	{
		Transform player = companion.player;
		if (Vector3.Distance(companion.transform.position, player.position) >= companion.playerMaxDistance)
			companion.GetFSM().ChangeState(FollowPlayerState.Instance);
		// If companion does not have any arrows, point to closest
		if (companion.arrows < 1)
		{

		}
		// If any enemy can be attacked, go to attacking mode
		IEnumerable<GameObject> enemiesThatCanBeAttacked = companion.EnemiesThatCanBeAttacked();
		if (enemiesThatCanBeAttacked.Count() > 0)
			companion.GetFSM().ChangeState(ChooseTargetState.Instance);

	}

	public override void Exit(CompanionMovement companion)
	{
	}

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
}

public class FollowPlayerState : FSMState<CompanionMovement>
{
	static readonly FollowPlayerState instance = new FollowPlayerState();
	public static FollowPlayerState Instance { get { return instance; } }

	static FollowPlayerState()
	{
	}
	FollowPlayerState()
	{
	}

	public override void Enter(CompanionMovement companion)
	{
		Debug.Log("Entering FollowPlayerState");
	}

	public override void Execute(CompanionMovement companion)
	{
		// If further than companion.playerMaxDistance from the player,
		// just move towards the player.
		Transform player = companion.player;
		if (Vector3.Distance(companion.transform.position, player.position) >= companion.playerMaxDistance)
		{
			Vector3 follow = player.position;
			follow.y = companion.transform.position.y;
			companion.transform.position = Vector3.MoveTowards(companion.transform.position, follow, companion.Speed * Time.deltaTime);
		}
		else
			companion.GetFSM().ChangeState(IdleState.Instance);
	}

	public override void Exit(CompanionMovement companion)
	{
	}

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
}

public class ChooseTargetState : FSMState<CompanionMovement>
{
	static readonly ChooseTargetState instance = new ChooseTargetState();
	public static ChooseTargetState Instance { get { return instance; } }

	static ChooseTargetState()
	{
	}
	ChooseTargetState()
	{
	}

	public override void Enter(CompanionMovement companion)
	{
		Debug.Log("Entering ChooseTargetState");
	}

	public override void Execute(CompanionMovement companion)
	{
		// From those enemies that the companion can see,
		// select the closest to the player
		IEnumerable<GameObject> close_enemies = companion.EnemiesThatCanBeAttacked();
		GameObject target_enemy = null;
		float minimum_distance = float.MaxValue;
		foreach (GameObject enemy in close_enemies)
		{
			float distance_to_player = Vector3.Distance(enemy.transform.position, companion.player.position);
			if (distance_to_player < minimum_distance)
			{
				minimum_distance = distance_to_player;
				target_enemy = enemy;
			}
		}
		companion.currentTarget = target_enemy;

		Debug.Log(target_enemy);
		Debug.Log(companion);
		// If the companion is already in weapon range
		// with target, proceed to load the bow
		if (Vector3.Distance(target_enemy.transform.position, companion.player.position) < companion.weaponRangeDistance)
			companion.GetFSM().ChangeState(RechargeState.Instance);
	}

	public override void Exit(CompanionMovement companion)
	{
	}

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
}

public class RechargeState : FSMState<CompanionMovement>
{
	static readonly RechargeState instance = new RechargeState();
	public static RechargeState Instance { get { return instance; } }

	static RechargeState()
	{
	}
	RechargeState()
	{
	}

	public override void Enter(CompanionMovement companion)
	{
		Debug.Log("Entering RechargeState");
	}

	public override void Execute(CompanionMovement companion)
	{
		// If the companion's weapon is not charged, charge it
		if (!companion.weaponIsCharged)
		{
			Debug.Log("Charging weapon");
			companion.weaponIsCharged = true;
		}
		companion.GetFSM().ChangeState(RechargeState.Instance);

	}

	public override void Exit(CompanionMovement companion)
	{
	}

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
}