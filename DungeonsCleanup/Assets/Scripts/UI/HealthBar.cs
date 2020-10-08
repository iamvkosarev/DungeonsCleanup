using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    [SerializeField] TextMeshProUGUI healthText;    

    public void SetMaxHealth(int health)
    {
        healthSlider.maxValue = health;
        healthText.text = $"{healthSlider.value}/{healthSlider.maxValue}";

    }
    public void SetHealth(int health)
    {
        healthSlider.value = health;
        healthText.text = $"{health}/{healthSlider.maxValue}";
    }
    public int GetMaxHelath()
    {
        return (int)healthSlider.maxValue;
    }

    public int GetHelath()
    {
        return (int)healthSlider.value;
    }
}
