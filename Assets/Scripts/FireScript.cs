using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour
{

    public AudioSource nourshingSound;

    // Start is called before the first frame update
    void Start()
    {
        nourshingSound.loop = false;
        nourshingSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        Color objectColor = this.gameObject.GetComponent<MeshRenderer>().material.color;
        if (objectColor.a > 0)
        {
            objectColor.a -= Time.deltaTime * Constants.OPACITY_SPEED_FIRE;
            this.gameObject.GetComponent<MeshRenderer>().material.color = objectColor;
        } else
        {
            Destroy(this.gameObject);
        }

    }


}
