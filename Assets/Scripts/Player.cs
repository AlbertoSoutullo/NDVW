using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;
using Random = UnityEngine.Random;

public class Player: MonoBehaviour
{
    public float health, maxHealth;
    public HealthBar healthBar;

    public GameObject hunter;
    
    public void TakeDamage(){
        // Use your own damage handling code, or this example one.
        health -= Mathf.Min( Random.value, health / 4f );            
        healthBar.UpdateHealthBar();
    }
    
    void Update(){
        // Example so we can test the Health Bar functionality
        if(Input.GetKeyDown(KeyCode.Space)){
            TakeDamage();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "ArrowSpawned(Clone)")
        {
            hunter.GetComponent<CompanionMovement>().arrows += 1;
            Destroy(other.gameObject);
        }
    }
}
