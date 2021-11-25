using Assets.Scripts;
using System.Collections;
using UnityEngine;

/// <summary>
/// Author: Janine Mayer
/// </summary>
public class Character : MonoBehaviour
{
    public Animator FieldOfView;
    public StatusBar HealthBar;
    public StatusBar CoinBar;
    public AudioSource heartSound;

    private int currHealth;
    private int maxHealth;
    private int painLevel;

    private int currCoins;
    private int maxCoins;

    private bool isInvincible;

#region Getter and Setter
    public void SetCurrHealth(int newCurrHealth)
    {
        CheckHealthCondition(currHealth, newCurrHealth);
        this.currHealth = newCurrHealth <= 0 
            ? 0 : newCurrHealth > maxHealth 
            ? maxHealth : newCurrHealth;

        if (HealthBar)
        {
            HealthBar.UpdateStatBar((float)currHealth / (float)maxHealth, currHealth, maxHealth);
        }

        if (this.currHealth == 0)
        {
            Debug.Log("Character died");
            StopAllCoroutines();
            //call game over
        }
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
   
    public void SetCurrCoins(int newCurrCoins)
    {
        newCurrCoins = newCurrCoins > maxCoins 
            ? maxCoins : newCurrCoins < 0 
            ? 0 : newCurrCoins;

        currCoins = newCurrCoins;

        if (CoinBar)
        {
            CoinBar.UpdateStatBar((float)currCoins / (float)maxCoins, currCoins, maxCoins);
        }

        if (currCoins == maxCoins)
        {
            Debug.Log("Won the game!");
            // call win logic
        }
    }

    public void ChangeCurrCoins(int coinChange)
    {
        SetCurrCoins(this.currCoins + coinChange);
    }

    public int GetCurrCoin()
    {
        return this.currCoins;
    }

    public void SetMaxCoins(int maxCoins)
    {
        this.maxCoins = maxCoins;
    }

    public int GetMaxCoins()
    {
        return this.maxCoins;
    }

#endregion // Getter and Setter

    public Character(int maxHealth, int maxCoins)
    {
        this.SetMaxHealth(maxHealth);
        this.SetCurrHealth(maxHealth);
        this.SetPainLevel(maxHealth / 2);

        this.SetMaxCoins(maxCoins);
        this.SetCurrCoins(0);

        this.isInvincible = false;
    }

    public Character() : this(100, 20)
    {
    }

    public void ResetCharacter()
    {
        SetCurrCoins(0);
        SetCurrHealth(this.maxHealth);
        this.isInvincible = true;

        SetCharacterStats();
    }

    // TODO after scene chanage probably re-trigger pulsate animation?
    private void CheckHealthCondition(int oldHealth, int newHealth)
    {
        if (newHealth <= painLevel)
        {
            if (FieldOfView && oldHealth >= painLevel)
            {
                Debug.Log("Character is in critical condition! " + currHealth + " / " + maxHealth);

                FieldOfView.SetBool(StringConstants.ANIMATION_ISHURT, true);
                heartSound.Play();
                heartSound.volume = 0.5f;
            }

            if (oldHealth != newHealth)
            {
                heartSound.volume += oldHealth > newHealth ? 0.2f : -0.1f;
            }
        }
        else if (FieldOfView && oldHealth <= painLevel && newHealth >= painLevel)
        {
            Debug.Log("Character is healed! " + currHealth + " / " + maxHealth);

            FieldOfView.SetBool(StringConstants.ANIMATION_ISHURT, false);
            heartSound.Stop();
        }
    }

    public void SetCharacterStats()
    {
        if (maxHealth == 0)
        {
            return;
        }

        PlayerPrefs.SetInt(CharacterStats.MaxHealth, maxHealth);
        PlayerPrefs.SetInt(CharacterStats.CurrHealth, currHealth);
        PlayerPrefs.SetInt(CharacterStats.PainLevel, painLevel);
        PlayerPrefs.SetInt(CharacterStats.MaxCoins, maxCoins);
        PlayerPrefs.SetInt(CharacterStats.CurrCoins, currCoins);
    }

    public void UpdateValuesWithCharacterStats()
    {
        var savedMaxHealth = PlayerPrefs.GetInt(CharacterStats.MaxHealth);
        if (savedMaxHealth == 0)
        {
            return;
        }

        SetMaxHealth(savedMaxHealth);
        SetCurrHealth(PlayerPrefs.GetInt(CharacterStats.CurrHealth));
        SetPainLevel(PlayerPrefs.GetInt(CharacterStats.PainLevel));
        SetPainLevel(PlayerPrefs.GetInt(CharacterStats.MaxCoins));
        SetPainLevel(PlayerPrefs.GetInt(CharacterStats.CurrCoins));
    }

    public void GetsAttacked(int strength)
    {
        //change coins?
        //change health
    }

    public bool IsInvincible()
    {
        return isInvincible;
    }

    public void SetInvincibility(int modeTime)
    {
        isInvincible = true;

        StartCoroutine(StringConstants.INVINCIBILITY_MODE, modeTime);
    }

    private IEnumerator InvincibilityMode(int modeTime)
    {
        Debug.Log("*** Invincibility mode started ***");
        yield return new WaitForSeconds(modeTime);
        Debug.Log("*** Invincibility mode ended ***");
        isInvincible = false;
    }

    public void Start()
    {
        UpdateValuesWithCharacterStats();
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
