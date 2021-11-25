using Assets.Scripts;
using UnityEngine;

/// <summary>
/// Author: Janine Mayer
/// </summary>
public class InteractionHandler : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
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
            item.GotGrabbed();

            //character.ChangeCurrHealth(item.GetValue()); -> TODO die Funktion NourishedPlayer verwenden, da dadurch das Item nur einmal eingenommen werden kann und gleich danach zerstoert wird
            
            //wuerde da auch also einen Unterschied machen, wie ein Item noch zu sich genommen werden kann, sonst waere ja die "gotGrabbed"-Methode obsolet ;)
        }
        else if (collider.GetComponent<ItemBaseClass>())
        {
            // add coin stuff
            // probably something else aswell
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
            // check when there is something to do
        }
    }

    private Character GetCharacter()
    {
        var playerGameObject = GameObject.Find(StringConstants.PLAYER);
        return playerGameObject.GetComponent<Character>();
    }
}
