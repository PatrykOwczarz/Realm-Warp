using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// The mana bar script was based on the following guide:
// https://www.youtube.com/watch?v=BLfNP4Sc_iA
// This applies to the actual UI, the logic implemented for the mana bar was created to fit the requirement of my game.
public class ManaBar : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI amountText;

    // sets the max value of mana for the mana bar.
    public void SetMaxMana(int mana)
    {
        slider.maxValue = mana;
        amountText.text = mana.ToString();
        slider.value = mana;
    }

    // sets the current value of mana in the mana bar.
    public void SetMana(int mana)
    {
        slider.value = mana;
        amountText.text = mana.ToString();
    }
}
