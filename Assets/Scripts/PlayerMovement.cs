using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    public float rotationSpeed;
    
    private Rigidbody _rigidbody;
    private Animator _animationController;

    //private static readonly int Speed = Animator.StringToHash("speed");

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = this.GetComponent<Rigidbody>();
        _animationController = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        bool runPressed = Input.GetKey("left shift");
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
        Vector3 runMovement = new Vector3(moveHorizontal * 1.5f, 0f, moveVertical * 1.5f);

        if (runPressed)
        {
            this._animationController.SetBool("isRunning", true);
            this._rigidbody.velocity = runMovement * (speed * Time.deltaTime);
        }
        else
        {
            this._animationController.SetBool("isRunning", false);
            this._rigidbody.velocity = movement * (speed * Time.deltaTime);
        }


        //this._animationController.SetFloat(Speed, movement.magnitude);

        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation =
                Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            this._animationController.SetBool("isWalking", true);
        }
        else
            this._animationController.SetBool("isWalking", false);

    }
}
