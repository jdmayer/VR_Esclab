using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    protected override void DestroyItem()
    {
        //TODO maybe fancy shit?
        base.DestroyItem();//calls Destroy()
    }
}
