using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Axel Bauer
/// </summary>
public class HealthItem : ItemBaseClass
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        InstantiateRandomValue(Constants.HEALTH_ITEM_VALUE_MIN, Constants.HEALTH_ITEM_VALUE_MAX);
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

    protected override void DestroyItem()
    {
        //TODO maybe fancy shit?
        base.DestroyItem();//calls Destroy()
    }
}