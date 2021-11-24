using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : ItemBaseClass
{

    public HealthItem() : base(Constants.HEALTH_ITEM_VALUE_MIN, Constants.HEALTH_ITEM_VALUE_MAX)
    {

    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void GotGrabbed()
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