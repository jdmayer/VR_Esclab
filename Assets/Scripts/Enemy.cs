using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    int currentAnimation; //idle, running, attacking etc.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //check for distance to player here -> define if either idle, running or attacking depending on currentAnimation
    }
}
