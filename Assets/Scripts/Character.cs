using Assets.Scripts;
using UnityEngine;

/// <summary>
/// Author: Janine Mayer
/// </summary>
public class Character : MonoBehaviour
{
    public Animator FieldOfView;
    public StatusBar HealthBar;
    public AudioSource heartSound;

    private int currHealth;
    private int maxHealth;
    private int painLevel;

    private bool attackable;
    private int currCoins;

    public void SetCurrHealth(int newCurrHealth)
    {
        CheckHealthCondition(currHealth, newCurrHealth);
        this.currHealth = newCurrHealth <= 0 
            ? 0 : newCurrHealth > maxHealth 
            ? maxHealth : newCurrHealth;
        HealthBar?.UpdateStatBar((float)currHealth / (float)maxHealth);
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

    public void setAmountCoins(int amount)
    {
        currCoins += amount;
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
        this.attackable = true;
        this.currCoins = 0;
    }

    public Character() : this(100)
    {
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
    }

    public bool IsAttackable()
    {
        return attackable;
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
