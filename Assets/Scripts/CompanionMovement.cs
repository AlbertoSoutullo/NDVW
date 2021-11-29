using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CompanionMovement : MonoBehaviour
{
    public float MinDistance;
    public float Speed;
    
    public Transform player;
    
    private static readonly int speedForAnimations = Animator.StringToHash("speed");
    
    private Animator _animationController;
    private Rigidbody _rigidbody;
    
    // Start is called before the first frame update
    void Start()
    {
        this._animationController = GetComponent<Animator>();
        this._rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update () {
        if (Vector3.Distance(transform.position, player.position) >= MinDistance)
        {
            Vector3 follow = player.position;
            
            follow.y = transform.position.y;
            
            transform.position = Vector3.MoveTowards(transform.position, follow, 
                Speed * Time.deltaTime);
            
            this._animationController.SetFloat(speedForAnimations, this._rigidbody.velocity.magnitude);
        }
    }
}
