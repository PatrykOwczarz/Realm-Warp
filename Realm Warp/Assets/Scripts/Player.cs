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

    private bool isDead = false;

    private Animator animator;

    public HealthBar healthBar;
    public ManaBar manaBar;

    private float timeInterval;

    void Start()
    {
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        currentMana = maxMana;
        manaBar.SetMaxMana(maxMana);

}

    void Update()
    {
        // Mana regen every second if below max mana
        if (currentMana < maxMana)
        {
            timeInterval += Time.deltaTime * 2;
            // in dark realm, regen 5 mana per second.
            if (GameInformation.instance.GetRealmWarp())
            {
                if (timeInterval >= 1f)
                {
                    RegainMana(5);
                    timeInterval = 0;
                }
            }
            // in regular realm regen 1 mana per second.
            else
            {
                if (timeInterval >= 1f)
                {
                    RegainMana(1);
                    timeInterval = 0;
                }
            }
            
        }

        // testing methods for other aspects of the game.
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
        // every method to do with health or mana updates the respective mana or health bar.
        // reduce health by the damage specified in the parameter, if the damage would cause health to go into the negatives, cap at 0.
        currentHealth -= damage;
        if (currentHealth > 0)
        {
            // the damage animation loses all motion when it is triggered so it may cause problems with more than one instance of damage happening.
            // the line of code bellow toggles the animation.
            //animator.SetTrigger("Damaged");
            healthBar.SetHealth(currentHealth);
        }
        else
        {
            currentHealth = 0;
            animator.SetTrigger("Dead");
            isDead = true;
            healthBar.SetHealth(currentHealth);
        }
    }

    public void UseMana(int manaUsed)
    {
        // reduce mana by manaUsed specified in the parameter, if the manaUse would cause the mana to go into the negatives, cap at 0.
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
        // same implementation as TakeDamage except instead of 0, maxHealth is used.    
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
        // same implementation as Heal except instead of maxHealth, maxMana is used.
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

    public bool GetIsDead()
    {
        return isDead;
    }
}
