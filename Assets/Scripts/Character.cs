using Assets.Scripts;
using UnityEngine;

/// <summary>
/// Author: Janine Mayer
/// </summary>
public class Character : MonoBehaviour
{
    public Animator FieldOfView;
    public StatusBar HealthBar;

    private int currHealth;
    private int maxHealth;
    private int painLevel;

    private bool isHoldingItem;

    public void SetCurrHealth(int newCurrHealth)
    {
        CheckHealthCondition(currHealth, newCurrHealth);
        this.currHealth = newCurrHealth < 0 
            ? 0 : newCurrHealth > maxHealth 
            ? maxHealth : newCurrHealth;
        HealthBar?.UpdateStatBar(currHealth / maxHealth);
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
        this.SetPainLevel(maxHealth / 2);
    }

    public Character() : this(100)
    {
    }

    private void CheckHealthCondition(int oldHealth, int newHealth)
    {
        Debug.Log(oldHealth + " => " + newHealth);
        if (FieldOfView && oldHealth >= painLevel && newHealth <= painLevel)
        {
            FieldOfView.SetBool(Constants.animationHurt, true);
            Debug.Log("Character is in critical condition! " + currHealth + " / " + maxHealth);
        }
        else if (FieldOfView && oldHealth <= painLevel && newHealth >= painLevel)
        {
            FieldOfView.SetBool(Constants.animationHurt, false);
            Debug.Log("Character is healed! " + currHealth + " / " + maxHealth);
        }
    }

    //for testing
    private void Update()
    {
        if (Input.GetKey(KeyCode.J))
        {
            ChangeCurrHealth(-5);
        }
        else if (Input.GetKey(KeyCode.K))
        {
            ChangeCurrHealth(5);
        }
    }
}
