using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    protected override void CollisionWithPlayer(GameObject player)
    {
        base.CollisionWithPlayer(player);
        player.GetComponent<Character>().SetCurrHealth(value);
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