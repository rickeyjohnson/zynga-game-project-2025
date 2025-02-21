using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int maxHealth = 100;
    private int currentHealth;
    public static Health instance;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        currentHealth = maxHealth;
        SliderManager.instance.slider.maxValue = maxHealth;
        SliderManager.instance.slider.value = currentHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        SliderManager.instance.UpdateSlider(-damage);

        if (currentHealth <= 0)
        {
            Debug.Log("Death -- to be added!");
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        SliderManager.instance.UpdateSlider(amount);
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
