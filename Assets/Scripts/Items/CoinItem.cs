using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Axel Bauer
/// </summary>
public class CoinItem : ItemBaseClass
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        this.weight = Constants.COIN_WEIGHT;
        InstantiateRandomValue(Constants.COIN_ITEM_VALUE_MIN, Constants.COIN_ITEM_VALUE_MAX);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void GotGrabbed()
    {
        base.GotGrabbed();//Call the class header
        //TODO Maybe animation?

        //call player script
        //player.changecurrcoin(value);
    }

    public override int NourishedPlayer()
    {
        
        Character character = GameController.GetPlayer().GetComponent<Character>();
        if (character == null)
        {
            Debug.LogError("CoinItem.cs: No character found!");
        } else
        {
            character.ChangeCurrCoins(value);
        }
        return base.NourishedPlayer();
    }

    protected override void DestroyItem()
    {
        //TODO maybe fancy shit?
        base.DestroyItem();//calls Destroy()
    }
}
