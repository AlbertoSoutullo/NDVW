using System.Collections;
using System.Collections.Generic;
using UnityEngine;




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
