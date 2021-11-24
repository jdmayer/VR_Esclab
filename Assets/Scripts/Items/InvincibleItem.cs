using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibleItem : ItemBaseClass
{
    // Start is called before the first frame update
    InvincibleItem() : base(Constants.INVINCIBLE_ITEM_VALUE_MIN,Constants.INVINCIBLE_ITEM_VALUE_MAX)
    {
      
    }

    protected override void Start()
    {
        base.Start();
        this.value = Constants.HEALTH_VALUE;
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
