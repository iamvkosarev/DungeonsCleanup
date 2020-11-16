using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    [SerializeField] Image expSlider;
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

    public void SetExpSliderParam(int currentExp, int needExp)
    {
        Debug.Log($"Опыт для замены: {currentExp} и {needExp}");
        float result;
        if (needExp != 0)
        {
            result = (float)currentExp / (float)needExp;
        }
        else
        {
            result = 0;
        }
        Debug.Log($"Изменить слайдер опыта на {result} !!!!");
        expSlider.fillAmount = result;
    }
}
