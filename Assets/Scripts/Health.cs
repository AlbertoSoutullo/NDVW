using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
	public static int maxHealth = 3;
	public int currentHealth;
	public static bool isGameOver;

    void Start()
    {
        isGameOver = false;
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (isGameOver)
        {
        	SceneManager.LoadScene("Level");
        }
    }


    public static void TakeDamage(int amount)
    {
    	currentHealth -= amount;
    	if (currentHealth <= 0)
    	{
    		isGameOver = true;
    	}
    	yield return new WaitForSeconds(1.5f);
    }
}
