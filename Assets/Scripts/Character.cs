using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Janine Mayer
/// </summary>
public class Character 
{

    private int currHealth;
    private int maxHealth;
    private int painLevel;

    private bool isHoldingItem;

    public void SetCurrHealth(int newCurrHealth)
    {
        CheckHealthCondition(currHealth, newCurrHealth);
        this.currHealth = newCurrHealth;
    }

    public void ChangeCurrHealth(int healthDifference)
    {
        SetCurrHealth(this.currHealth + healthDifference);
    }

    public int GetCurrHealth()
    {
        return this.currHealth;
    }

    public void SetMaxHealth(int newMaxHealth)
    {
        this.maxHealth = newMaxHealth;
    }

    public int GetMaxHealth()
    {
        return this.maxHealth;
    }

    public void SetPainLevel(int painLevel)
    {
        this.painLevel = painLevel < maxHealth && painLevel > 0 
            ? painLevel
            : maxHealth / 2;
    }

    public int GetPainLevel()
    {
        return this.painLevel;
    }

    public Character(int maxHealth)
    {
        this.SetMaxHealth(maxHealth);
        this.SetCurrHealth(maxHealth);
    }

    public Character() : this(100)
    {
    }

    private void CheckHealthCondition(int oldHealth, int newHealth)
    {
        if (oldHealth > painLevel && newHealth < painLevel)
        {
            // blurr vision
            // adjust audio
            Debug.Log("Character is in critical condition! " + currHealth + " / " + maxHealth);
        }
        else if (oldHealth < painLevel && newHealth > painLevel)
        {
            // stop all distraction
            Debug.Log("Character is healed! " + currHealth + " / " + maxHealth);
        }
    }
}
