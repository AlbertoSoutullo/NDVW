using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CompanionMovement : MonoBehaviour
{
	FiniteStateMachine<CompanionMovement> stateMachine;

	public float playerMaxDistance = 20;
    public float Speed;
    
    public Transform player;
    
    // Start is called before the first frame update
    void Start()
	{
		stateMachine = new FiniteStateMachine<CompanionMovement>(this);
		stateMachine.CurrentState = FollowPlayerState.Instance;
	}

	// Update is called once per frame
	void Update ()
	{
		stateMachine.Update();
    }
}
