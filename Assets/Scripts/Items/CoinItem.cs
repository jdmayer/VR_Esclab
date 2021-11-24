using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : ItemBaseClass
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
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

    public override void DestroyItem()
    {
        //TODO maybe fancy shit?
        base.DestroyItem();//calls Destroy()
    }
}
