using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthSlider;
    [SerializeField] private Image expSlider;
    [SerializeField] private Image selectedItemIcon;
    private int maxHealth;
    private int health;
    //[SerializeField] TextMeshProUGUI healthText;    

    public void SetMaxHealth(int health)
    {
        this.maxHealth = health;
        SetHPSliderParam();
        //healthText.text = $"{healthSlider.value}/{healthSlider.maxValue}";

    }
    public void SetHealth(int health)
    {
        this.health = health;
        SetHPSliderParam();
        //healthText.text = $"{health}/{healthSlider.maxValue}";
    }
    public void SetSelectedItem(Sprite newIcon)
    {
        selectedItemIcon.sprite = newIcon;
        selectedItemIcon.color = new Color(1, 1, 1, 1);
    }
    public void RemoveSelectedItem()
    {
        selectedItemIcon.sprite = null;
        selectedItemIcon.color = new Color(1, 1, 1, 0);
    }
    public int GetMaxHelath()
    {
        return maxHealth;
    }

    public int GetHelath()
    {
        return health;
    }
    public void SetHPSliderParam()
    {
        float result;
        if (maxHealth != 0)
        {
            result = (float)health / (float)maxHealth;
        }
        else
        {
            result = 0;
        }
        healthSlider.fillAmount = result;
    }
    public void SetExpSliderParam(int currentExp, int needExp)
    {
        float result;
        if (needExp != 0)
        {
            result = (float)currentExp / (float)needExp;
        }
        else
        {
            result = 0;
        }
        expSlider.fillAmount = result;
    }
}
