using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CompanionMovement : MonoBehaviour
{
    public Transform player;
    
    private static readonly int speedForAnimations = Animator.StringToHash("speed");
    
    private Animator _animationController;
    private Rigidbody _rigidbody;
    private NavMeshAgent _navMeshAgent;
    
    // Start is called before the first frame update
    void Start()
    {
        this._animationController = GetComponent<Animator>();
        this._rigidbody = GetComponent<Rigidbody>();
        this._navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update ()
    {
        this._navMeshAgent.SetDestination(player.position);
        Debug.Log(this._rigidbody.velocity);
        this._animationController.SetFloat(speedForAnimations, this._rigidbody.velocity.magnitude);
        this.transform.LookAt(player);
    }
}
