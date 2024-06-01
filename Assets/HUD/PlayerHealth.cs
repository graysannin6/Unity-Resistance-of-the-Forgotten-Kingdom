using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{   
    
    private float heath;
    private float lerpTimer;
    public float maxHealth = 8;
    public float chipSpeed = 2f;
    public Image frontHealthBar;
    public Image backHealthBar;

   // Start is called before the first frame update
    
    void Start()
    {
        heath = maxHealth;
        frontHealthBar.fillAmount = 1;
        backHealthBar.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1);
        }

    }

    public void UpdateHealthUI()
    {
        Debug.Log(health);
    }


    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
    }


}
