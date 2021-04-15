using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;
    public int maxMana = 100;
    public int currentMana;

    public HealthBar healthBar;
    public ManaBar manaBar;

    private float timeInterval;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        currentMana = maxMana;
        manaBar.SetMaxMana(maxMana);

}

    void Update()
    {
        //Mana regen every second if below max mana
        if (currentMana < maxMana)
        {
            timeInterval += Time.deltaTime * 2;
            if (GameInformation.instance.GetRealmWarp())
            {
                if (timeInterval >= 1f)
                {
                    RegainMana(5);
                    timeInterval = 0;
                }
            }
            else
            {
                if (timeInterval >= 1f)
                {
                    RegainMana(1);
                    timeInterval = 0;
                }
            }
            
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            TakeDamage(23);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            UseMana(20);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Heal(20);
            RegainMana(20);
        }
    }

    public void TakeDamage(int damage)
    {
        //expand implementation so that it goes down to zero rather than not going decreasing it if it would go negative.

        currentHealth -= damage;
        if (currentHealth >= 0)
        {
            healthBar.SetHealth(currentHealth);
        }
        else
        {
            currentHealth = 0;
            healthBar.SetHealth(currentHealth);
        }
    }

    public void UseMana(int manaUsed)
    {
        currentMana -= manaUsed;
        if (currentMana >= 0)
        {
            manaBar.SetMana(currentMana);
        }
        else
            currentMana += manaUsed;
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth <= maxHealth)
        {
            healthBar.SetHealth(currentHealth);
        }
        else
        {
            currentHealth = maxHealth;
            healthBar.SetHealth(currentHealth);
        }
    }
    public void RegainMana(int manaGained)
    {
        currentMana += manaGained;
        if (currentMana <= maxMana)
        {
            manaBar.SetMana(currentMana);
        }
        else
        {
            currentMana = maxMana;
            manaBar.SetMana(currentMana);
        }
    }
}
