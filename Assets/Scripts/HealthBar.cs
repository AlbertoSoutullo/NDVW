using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Quaternion startRotation;
    
    public Image healthBarImage;
    public Player player;
    public Color fullColor;
    public Color lowColor;

    public void UpdateHealthBar()
    {
        Debug.Log(fullColor.ToString()); Debug.Log(lowColor.ToString());
        Debug.Log(healthBarImage.ToString()); Debug.Log(player.ToString());
        healthBarImage.fillAmount = Mathf.Clamp(player.health / player.maxHealth, 0, 1f);
        healthBarImage.color = Color.Lerp(lowColor, fullColor, healthBarImage.fillAmount);
    }

}
