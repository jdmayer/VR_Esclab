using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Axel Bauer
/// </summary>
public class InvincibleItem : ItemBaseClass
{

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        InstantiateRandomValue(Constants.INVINCIBLE_ITEM_VALUE_MIN, Constants.INVINCIBLE_ITEM_VALUE_MAX);
        //these numbers specify the duration of invincibility
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void GotGrabbed()
    {
        base.GotGrabbed();//Call the class header
    }

    public override int NourishedPlayer()
    {
        Character character = GameController.GetPlayer().GetComponent<Character>();
        if (character == null)
        {
            Debug.LogError("CoinItem.cs: No character found!");
        }
        else
        {
            character.SetInvincibility(value);
        }
        return base.NourishedPlayer();
    }

    protected override void DestroyItem()
    {
        base.DestroyItem();//calls Destroy()
    }
}
