using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{   
    
    private float health;
    private float lerpTimer;
    public float maxHealth = 100f;
    public float chipSpeed = 2f;
    public Image frontHealthBar;
    public Image backHealthBar;
    private string barRestoreColorHex = "#C9FF9E";
    private Color restoredColor;

   // Start is called before the first frame update
    
    void Start()
    {
        health = maxHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();
        if(Input.GetKeyDown(KeyCode.A))
        {
            TakeDamage(1);
        }

        if(Input.GetKeyDown(KeyCode.S))
        {
            RestoreHealth(1);
        }

    }

    public void UpdateHealthUI()
    {
        Debug.Log(health);
        float fillFront = frontHealthBar.fillAmount;
        float fillBack = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;
        if(fillBack > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.black;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            backHealthBar.fillAmount = Mathf.Lerp(fillBack, hFraction, percentComplete);
        }
        if(fillFront<hFraction)
        {
            backHealthBar.fillAmount = hFraction;
            
            lerpTimer += Time.deltaTime;

            if(ColorUtility.TryParseHtmlString(barRestoreColorHex, out restoredColor))
            {
                backHealthBar.color = restoredColor;
            
            }
            else
            {
                Debug.Log("Color not parsed");
            }

            float percentComplete = lerpTimer / chipSpeed;
            frontHealthBar.fillAmount = Mathf.Lerp(fillFront, hFraction, percentComplete);
        }
    }


    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
    }

    public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        lerpTimer = 0f;
    }


}
