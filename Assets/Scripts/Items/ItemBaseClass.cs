using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

/**
 * Author: Axel Bauer
 * This class is the base class of all items and defines some initial methods to access values
 */
public class ItemBaseClass : MonoBehaviour
{
    protected int value;
    protected float weight; //for sound amplitude when dropping

    protected bool taken;
    protected bool isRotating;
    protected Interactable interactable;

    public AudioSource fallSound;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        weight = Constants.ITEM_WEIGHT;
        isRotating = true;
        this.gameObject.AddComponent<Rigidbody>();
        this.gameObject.GetComponent<Rigidbody>().mass = weight;
        this.gameObject.GetComponent<Rigidbody>().useGravity = false;

        this.gameObject.AddComponent<VelocityEstimator>();
        this.gameObject.AddComponent<Interactable>();
        this.gameObject.AddComponent<ComplexThrowable>();

        interactable = this.gameObject.GetComponent<Interactable>();
        interactable.hideHandOnAttach = false;
        interactable.handFollowTransform = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (isRotating)
        {
            this.gameObject.transform.Rotate(new Vector3(0, Constants.ITEM_ROTATION_SPEED, 0.0f));
        }

        if (taken && this.gameObject.GetComponent<Rigidbody>().useGravity == false)
        {
            this.gameObject.GetComponent<Rigidbody>().useGravity = true;
        }

        if (interactable != null && interactable.attachedToHand != null)
        {
            GotGrabbed();
        }
    }

    //EventListener, if collision with ground -> getWeight -> amplitude sound
    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains(StringConstants.HAND_COLLIDER))
        {
            CollisionWithPlayer(collision.gameObject);
        }

        if (collision.gameObject.tag == "Enemy" || collision.gameObject.name == "Enemy" || collision.gameObject.name == "enemy")
        {
            CollisionWithEnemy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Ground" || collision.gameObject.name == "Ground" || collision.gameObject.name == "ground" || collision.gameObject.name == "Ground_Plane")
        {
            CollisionWithGround(collision.gameObject);
        }
    }

    protected virtual void CollisionWithPlayer(GameObject player)
    {
        Debug.Log("Debug: Collission with player");//TODO should be removed before production release
    }

    protected virtual void CollisionWithEnemy(GameObject enemy)
    {
        GotGrabbed();//TODO this is for testing purposes, delete it before publishing
        Debug.Log("Got Grabbed");
    }

    protected virtual void CollisionWithGround(GameObject ground)
    {
        Debug.Log("Play Sound now!");
        isRotating = false;
        fallSound.volume = weight * 100;
        fallSound.Play();
    }

    public virtual void GotGrabbed() // should be called when grabbed -> Player class
    {
        this.taken = true;
    }

    public virtual int NourishedPlayer()//Should be called when eaten -> Player class
    {
        DestroyItem();
        return value;
    }

    protected virtual void DestroyItem()
    {
        Destroy(this.gameObject);
    }

    protected void InstantiateRandomValue(int min, int max)
    {
        value = Random.Range(min, max);
    }
}

// TODO COIN CLASS
// when collect -> animation + sound

// TODO HEALTH CLASS
// when collect -> animation? + sound