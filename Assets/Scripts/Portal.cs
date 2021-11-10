using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Janine Mayer
/// </summary>
public class Portal
{
    public void ChangeScene()
    {
        //get current labyrinth
        //iterate through static array of labyrinth thingies
        Debug.Log("clicked on change Scene");
    }

    void OnCollisionEnter(Collision collision)
    {
        //Ouput the Collision to the console
        Debug.Log("Collision : " + collision.gameObject.name);
    }
}
