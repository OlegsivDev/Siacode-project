using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static event Action OnPlayerTakeDamage;
    public static event Action OnPlayerDeath;
    public float health, maxHealth;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(int damageAmount)
    {
        OnPlayerTakeDamage.Invoke();
        health -= damageAmount;
        if (health <= 0)
        {
            playerDeath();
        } 
    }

    public void playerDeath()
    {
        
    }
}
