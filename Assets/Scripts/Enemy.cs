using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{

    int currentAnimation; //idle, running, attacking etc.
    GameObject player;
    float distanceToPlayer = 0;
    AIDestinationSetter destinationObject;

    // Start is called before the first frame update
    void Start()
    {
        currentAnimation = 0;

        //this.gameObject.AddComponent<Rigidbody>();
        //this.gameObject.GetComponent<Rigidbody>().mass = Constants.ENEMY_MASS;

        player = FindPlayer();
        distanceToPlayer = GetDistanceToPlayer();


        destinationObject = this.gameObject.GetComponent<AIDestinationSetter>();
        if (destinationObject == null)
        {
            Debug.LogError("Enemy.cs: Couldn't find DestinationSetter script! Please make sure it is added to the object");
        }
        else
        {
            destinationObject.target = player.transform;
            Debug.Log("Destination set with target: " + destinationObject.target);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //check for distance to player here -> define if either idle, running or attacking depending on currentAnimation
        //only every 10 seconds

        AIPath pathSetter = this.gameObject.GetComponent<AIPath>();

        Debug.Log("Checking if distance is large enough...");
        switch (GetDistanceToPlayer())
        {
            case < Constants.DISTANCE_TO_ENEMY_UNTIL_MOVE:
                if (pathSetter.canMove == false)
                {
                    Debug.Log("Distance large enough, setting player");
                    pathSetter.canMove = true;
                }


                //TODO create pathinding
                //TODO add animation
                break;
            case > Constants.DISTANCE_TO_ENEMY_UNTIL_MOVE:
                //TODO again back to idle aniomation
                //TODO stop pathfinding
                Debug.Log("Distance is not large enough - Null");
                if (pathSetter.canMove)
                {
                    Debug.Log("Distance large enough, setting player");
                    pathSetter.canMove = false;
                }

                break;
        }



    }

    void Attack()
    {
        //Get Health Params of Player here and reduce it!
    }

    float GetDistanceToPlayer()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 enemyPosition = this.gameObject.transform.position;
        //Debug.Log((playerPosition - enemyPosition).magnitude);
        return (playerPosition - enemyPosition).magnitude;
    }

    GameObject FindPlayer()
    {
        GameObject player = GameObject.Find("Player");

        if (player == null)
        {
            player = GameObject.Find("player");
        }

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if (player == null)
        {
            Debug.LogError("Enemy.cs: Couldn't find Player!");
        }

        return player;
    }
}