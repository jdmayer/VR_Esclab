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
    public AudioSource HeartSound;

    public Camera Camera;

    private int currHealth;
    private int maxHealth;
    private int painLevel;

    private int currCoins;
    private int maxCoins;

    private bool isInvincible;

    private float nextRecharge = 0.0f;
    private float rechargeTime = 1000.0f;

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

            var gameController = GetGameController();
            gameController?.GameOver(Camera.transform);        
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
            StopAllCoroutines();

            var gameController = GetGameController();
            gameController?.GameWon(Camera.transform);
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

    public Character() : this(100, 10)
    {
    }

    public void ResetCharacter()
    {
        SetCurrCoins(0);
        SetCurrHealth(this.maxHealth);
        this.isInvincible = true;

        SetCharacterStats();
    }

    private void CheckHealthCondition(int oldHealth, int newHealth)
    {
        if (newHealth <= painLevel)
        {
            if (FieldOfView && oldHealth >= painLevel)
            {
                Debug.Log("Character is in critical condition! " + currHealth + " / " + maxHealth);

                FieldOfView.SetBool(StringConstants.ANIMATION_ISHURT, true);
                HeartSound.Play();
                HeartSound.volume = 1 - ((float)newHealth / (float)painLevel);
            }

            if (oldHealth != newHealth)
            {
                HeartSound.volume = 1 - ((float)newHealth / (float)painLevel);
            }
        }
        else if (FieldOfView && oldHealth <= painLevel && newHealth >= painLevel)
        {
            Debug.Log("Character is healed! " + currHealth + " / " + maxHealth);

            FieldOfView.SetBool(StringConstants.ANIMATION_ISHURT, false);

            if (newHealth > painLevel)
            {
                HeartSound.Stop();
            }
            else
            {
                HeartSound.volume = 1 - ((float)newHealth / (float)painLevel);
            }
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
        SetMaxCoins(PlayerPrefs.GetInt(CharacterStats.MaxCoins));
        SetCurrCoins(PlayerPrefs.GetInt(CharacterStats.CurrCoins));
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

    private GameController GetGameController()
    {
        var gameControllerObject = GameObject.Find(StringConstants.GAME_CONTROLLER);
        return gameControllerObject ? gameControllerObject.GetComponent<GameController>() : null;
    }

    private IEnumerator UpdateHealth()
    {
        while (true)
        {
            nextRecharge -= Time.deltaTime;

            if (nextRecharge < 0)
            {
                ChangeCurrHealth(1);
                nextRecharge = rechargeTime;
            }

            yield return null;
        }
    }

    public void Start()
    {
        UpdateValuesWithCharacterStats();
        StartCoroutine(StringConstants.UPDATE_HEALTH);
    }

    //Test status update and reaction
    private void Update()
    {
        if (Input.GetKey(KeyCode.J))
        {
            ChangeCurrHealth(-1);
        }
        else if (Input.GetKey(KeyCode.K))
        {
            ChangeCurrHealth(1);
        }
        else if (Input.GetKey(KeyCode.I))
        {
            ChangeCurrCoins(1);
        }
        else if (Input.GetKey(KeyCode.U))
        {
            ChangeCurrCoins(-1);
        }
        else if (Input.GetKey(KeyCode.O))
        {
            Debug.Log("Health: " + currHealth + " / " + maxHealth);
            Debug.Log("Coins: " + currCoins + " / " + maxCoins);
        }
    }
}
