using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public static PlayerHealth instance;
    private HealthBar healthBar;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        currentHealth = maxHealth;
        healthBar = HealthBar.instance;
        healthBar.UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar?.UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Debug.Log("Death -- to be added!");
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        healthBar?.UpdateHealthBar();
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
