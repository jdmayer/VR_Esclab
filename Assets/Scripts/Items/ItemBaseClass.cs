using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBaseClass : MonoBehaviour
{
    protected int value;
    protected float weight; //for sound amplitude when dropping

    protected bool taken;
    protected bool isRotating;

    // Start is called before the first frame update
    protected void Start()
    {
        weight = Constants.ITEM_WEIGHT;
        isRotating = true;
    }

    // Update is called once per frame
    protected void Update()
    {
        if (isRotating)
        {
            this.gameObject.transform.Rotate(new Vector3(0, Constants.ITEM_ROTATION_SPEED, 0.0f));
        }

        if (taken && this.gameObject.GetComponent<Rigidbody>() == null)
        {
            this.gameObject.AddComponent<Rigidbody>();
            this.gameObject.GetComponent<Rigidbody>().mass = weight;
        }
    }

    //EventListener, if collision with ground -> getWeight -> amplitude sound
    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.name == "Player" || collision.gameObject.name == "player")
        {
            Debug.Log("Debug: Collission with player");//should be removed before production release
        }

        if (collision.gameObject.tag == "Enemy" || collision.gameObject.name == "Enemy" || collision.gameObject.name == "enemy")//this is for testing purposes, delete it before publishing
        {
            GotGrabbed();
            Debug.Log("Got Grabbed");
        }

        if (collision.gameObject.tag == "Ground" || collision.gameObject.name == "Ground" || collision.gameObject.name == "ground" || collision.gameObject.name == "Ground_Plane")
        {
            Debug.Log("Play Sound now!");
            isRotating = false;
            //    audioSource.Play();
        }
    }

    protected void GotGrabbed() // should be called when grabbed -> Player class
    {
        this.taken = true;
    }

    protected void NourishedPlayer()//Should be called when eaten -> Player class
    {
        DestroyItem();
    }

    protected void DestroyItem()
    {
        Destroy(this.gameObject);
    }
}