using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Axel Bauer
/// </summary>
public class Battery : MonoBehaviour
{
    public AudioSource soundeffect;

    // Start is called before the first frame update
    void Start()
    {
        soundeffect.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
