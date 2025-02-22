using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestPlayerHealth : MonoBehaviour
{
    
    public PlayerHealth playerHealth;
    public Button damageButton;
    public Button healButton;

    private void Start()
    {
        damageButton.onClick.AddListener(() => playerHealth.TakeDamage(1));
        healButton.onClick.AddListener(() => playerHealth.Heal(1));
    }
}
