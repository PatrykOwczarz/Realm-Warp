using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManaBar : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI amountText;

    public void SetMaxMana(int mana)
    {
        slider.maxValue = mana;
        amountText.text = mana.ToString();
        slider.value = mana;
    }

    public void SetMana(int mana)
    {
        slider.value = mana;
        amountText.text = mana.ToString();
    }
}
