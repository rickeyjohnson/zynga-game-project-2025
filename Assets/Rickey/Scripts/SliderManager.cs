using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    int healthBar = 100;
    public Slider slider;
    public static SliderManager instance;

    void Start()
    {
        slider.maxValue = Health.instance.GetCurrentHealth();
    }

    void Awake()
    {
        instance = this;
    }

    public void UpdateSlider(int value)
    {
        healthBar += value;
        slider.value = healthBar;
    }

}
