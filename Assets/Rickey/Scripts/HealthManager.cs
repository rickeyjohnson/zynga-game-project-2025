using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 10; // lets say average thing has 10 hearts
    private int currentHealth;
    public event Action OnDeath;

    private void Awake()
    {
        currentHealth = maxHealth;
        if (currentHealth <= 0) { Die(); }
    }

    private void Die()
    {
        OnDeath?.Invoke();
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
