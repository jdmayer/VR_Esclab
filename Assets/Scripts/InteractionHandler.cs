using Assets.Scripts;
using System.Collections;
using UnityEngine;
using Valve.VR;

/// <summary>
/// Author: Janine Mayer
/// </summary>
public class InteractionHandler : MonoBehaviour
{
    public SteamVR_Action_Boolean Grab;
    public SteamVR_Input_Sources handType;

    private GameObject currentItem;
    private FixedJoint attachedJoint = null;

    private void Awake()
    {
        attachedJoint = GetComponent<FixedJoint>();
    }

    void Start()
    {
        Grab.AddOnStateDownListener(GrabObject, handType);
        Grab.AddOnStateUpListener(ReleaseObject, handType);
    }

    void Update()
    {
    }

    public void GrabObject(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (currentItem == null)
        {
            return;
        }

        //var currentItemRigidBody = currentItem.GetComponent<Rigidbody>();
        //currentItemRigidBody.transform.position = transform.position;
        //attachedJoint.connectedBody = currentItemRigidBody;

        Debug.Log("Grabbed object");
    }

    public void ReleaseObject(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Released object");

    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<HealthItem>())
        {
            var character = GetCharacter();
            if (character == null)
            {
                Debug.Log("No character found! Interaction not possible.");
                return;
            }

            HealthItem item = collider.GetComponent<HealthItem>();
            character.ChangeCurrHealth(item.GetValue());
            item.GotGrabbed();
        }
        else if (collider.GetComponent<ItemBaseClass>())
        {
            currentItem = collider.gameObject;            
        }
        else if (collider.GetComponent<Enemy>())
        {
            var character = GetCharacter();

            if (character == null)
            {
                Debug.Log("No character found! Interaction not possible.");
                return;
            }

            Debug.Log("Well shit is getting started");
            character.ChangeCurrHealth(-10);
        }
    }

    public void OnTriggerExit(Collider collider)
    {
        if (collider.GetComponent<ItemBaseClass>())
        {
            currentItem = null;
        }
    }

    private Character GetCharacter()
    {
        var playerGameObject = GameObject.Find(StringConstants.PLAYER);
        return playerGameObject.GetComponent<Character>();
    }
}
