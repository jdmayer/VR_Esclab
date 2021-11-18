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

    private bool isHoldingItem;

    public void SetCurrHealth(int newCurrHealth)
    {
        this.currHealth = newCurrHealth;
    }

    public void ChangeCurrHealth(int healthDifference)
    {
        this.currHealth += healthDifference;
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

    public Character(int maxHealth)
    {
        this.SetMaxHealth(maxHealth);
        this.SetCurrHealth(maxHealth);
    }

    public Character() : this(100)
    {
    }
}
