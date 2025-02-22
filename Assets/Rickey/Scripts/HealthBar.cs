using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    private PlayerHealth playerHealth;
    public static HealthBar instance;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        playerHealth = PlayerHealth.instance;
        healthSlider.maxValue = playerHealth.maxHealth;
        healthSlider.value = playerHealth.GetCurrentHealth();
    }

    public void UpdateHealthBar()
    {
        if (playerHealth != null && healthSlider != null)
        {
            healthSlider.value = playerHealth.GetCurrentHealth();
        }
    }
}
