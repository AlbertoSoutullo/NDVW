using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CompanionMovement : MonoBehaviour
{
    public float MinDistance;
    public float Speed;
    
    public Transform player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update () {
        if (Vector3.Distance(transform.position, player.position) >= MinDistance)
        {
            Vector3 follow = player.position;
            
            follow.y = transform.position.y;
            
            transform.position = Vector3.MoveTowards(transform.position, follow, Speed * Time.deltaTime);
        }
    }
}
