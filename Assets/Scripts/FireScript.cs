using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Axel Bauer
/// </summary>
public class FireScript : MonoBehaviour
{

    public AudioSource nourshingSound;
    private float runtime = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        nourshingSound.loop = false;
        nourshingSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        var particleSystem = this.gameObject.GetComponent<ParticleSystem>();
        runtime -= Time.deltaTime;

        if (runtime < 0)
        {
            particleSystem.Stop();
            Destroy(this.gameObject);
        }
    }
}
