using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : ItemBaseClass
{

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        this.value = Constants.HEALTH_VALUE;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public void GotGrabbed()
    {
        base.GotGrabbed();//Call the class header
        //TODO Maybe animation?
    }

    private void DestroyItem()
    {
        //TODO maybe fancy shit?
        base.DestroyItem();//calls Destroy()
    }
}