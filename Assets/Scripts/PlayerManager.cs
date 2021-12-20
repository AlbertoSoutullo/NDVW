using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    public static int playerHP = 100;
    public TextMeshProUGUI playerHPText;
    
    public static bool isGameOver;
    void Start()
    {
    	isGameOver = false;
    	playerHP = 100;
    }

    void Update()
    {
        playerHPText.text = "+"+ playerHP;
        if (isGameOver)
        {
        	SceneManager.LoadScene("Level");
        }
    }

    public static void TakeDamage(int damageAmount)
    {
    	playerHP -= damageAmount;
    	if (playerHP <= 0)
    		isGameOver = true;

    	yield return new WaitForSeconds(1.5f);
    }

}
