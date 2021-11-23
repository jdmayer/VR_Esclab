using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{

    int currentAnimation; //idle, running, attacking etc.
    bool isFlying;
    bool isAsleep;
    bool isAttacking;
    float speed;

    GameObject player;
    float distanceToPlayer = 0;
    AIDestinationSetter destinationObject;
    AIPath pathSetter;
    Animator animator;

    private float waitTime = 1.0f;
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Enemy.cs: Couldn't find Animator Component! Please check Inspector if Enemy.cs script is on the right object or the Animator component exists!");
        }

        currentAnimation = -1;
        isFlying = false;
        isAsleep = true;
        isAttacking = false;

        //this.gameObject.AddComponent<Rigidbody>();
        //this.gameObject.GetComponent<Rigidbody>().mass = Constants.ENEMY_MASS;

        player = FindPlayer();
        distanceToPlayer = GetDistanceToPlayer();


        destinationObject = this.gameObject.GetComponent<AIDestinationSetter>();
        if (destinationObject == null)
        {
            Debug.LogError("Enemy.cs: Couldn't find DestinationSetter script! Please make sure it is added to the object");
        } else
        {
            destinationObject.target = player.transform;
            Debug.Log("Destination set with target: " + destinationObject.target);
        }

        pathSetter = this.gameObject.GetComponent<AIPath>();
        if (pathSetter == null)
        {
            Debug.LogError("Enemy.cs: Couldn't find DestinationSetter script! Please make sure it is added to the object");
        }   
    }

    // Update is called once per frame
    void Update()
    {
        //check for distance to player here -> define if either idle, running or attacking depending on currentAnimation
        //only every 10 seconds?

        //Debug.Log(pathSetter.velocity.magnitude.ToString());


        animator.SetInteger("animationState",currentAnimation);
        animator.SetBool("isFlying", isFlying);
        animator.SetBool("isSleeping", isAsleep);
        animator.SetBool("isAttacking",isAttacking);


        //Debug.Log("Checking if distance is large enough...");
        switch (GetDistanceToPlayer())
            {
                //--------------------------------------------------------------------------------------------- IF PLAYER IS/COMES IN RANGE
                case < Constants.DISTANCE_TO_ENEMY_UNTIL_MOVE:
                    if (pathSetter.canMove == false)
                    {
                        //Debug.Log("Distance large enough, setting player");
                        pathSetter.canMove = true;
                    }

                    currentAnimation = 1;
                    isAsleep = false;
                    isFlying = false;

                    if (pathSetter.velocity.magnitude <= 0.5)//-------------------------------------------- IF ENEMY DOESNT MOVE
                    {

                    if (GetDistanceToPlayer() <= Constants.ATTACKING_DISTANCE)//--------------------------------------------------------- CHECK IF Close to Player
                        {
                            timer += Time.deltaTime;
                            if (timer > waitTime)
                            {
                                Attack();
                                timer = timer - waitTime;
                            Debug.Log("Some time has passed");
                            }
                            
                            isAttacking = true;
                            isFlying = false;
                            currentAnimation = -1;
                        } else
                        {
                            currentAnimation = 0;
                            isAttacking = false;
                        }
                } else //------------------------------------------------------------------------------ IF ENEMY MOVES
                    {
                        currentAnimation = 1;
                    }


                //TODO create pathinding
                //TODO add animation
                break;

                //--------------------------------------------------------------------------------------------- IF PLAYER GOES OUT OF RANGE
                case > Constants.DISTANCE_TO_ENEMY_UNTIL_MOVE:
                    

                    isFlying = decideIfFlying();//only when to distance is again large enough, decide if the dragon should fly

                    //Debug.Log("Distance is not large enough - Null");
                    if (pathSetter.canMove)
                    {
                        //Debug.Log("Distance large enough, setting player");
                        pathSetter.canMove = false;
                        
                    if (isAsleep)
                    {
                        currentAnimation = -1;

                    } else if (isFlying)
                    {
                        currentAnimation = -1;
                        isAttacking = false;
                    }
                    else
                    {
                        currentAnimation = 0;
                    }
                }

                break;
            }


        /*Debug.Log("currentAnimation" + currentAnimation);
        Debug.Log("distance to player" + GetDistanceToPlayer());
        Debug.Log("isFlying: " + isFlying);
        Debug.Log("isSleeping: " + isAsleep);
        Debug.Log("isAttacking: " + isAttacking);*/
    }

    bool decideIfFlying()
    {
        //use random choice here -> if yes, then current Animation = -1; do this only every 20 seconds!
        currentAnimation = -1;
        return true;
    }

    bool decideIfSleeping()
    {
        //use random choice here -> if yes, then current Animatoin = -1
        currentAnimation = -1;
        return true;
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
        return (playerPosition-enemyPosition).magnitude;
    }

    GameObject FindPlayer()
    {
        GameObject player = GameObject.Find("FollowHead");

      //GameObject player = GameObject.Find("Player");

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
