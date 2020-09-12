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
        healthSlider.value = health;
        healthText.text = $"{health}/{health}";

    }
    public void SetHealth(int health)
    {
        healthSlider.value = health;
        healthText.text = $"{health}/{healthSlider.maxValue}";
    }
}
