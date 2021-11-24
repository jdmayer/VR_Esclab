using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using static Character;

public class Enemy : MonoBehaviour
{

    int currentAnimation; //idle, running, attacking etc.
    bool isFlying;
    bool isAsleep;
    bool isAttacking;
    float speed;

    GameObject moveablePlayer;
    GameObject player;
    Character characterComponent;
    float distanceTomoveablePlayer = 0;
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
        moveablePlayer = FindmoveablePlayer();
        distanceTomoveablePlayer = GetDistanceTomoveablePlayer();

        characterComponent = player.GetComponent<Character>();
        if (characterComponent == null)
        {
            Debug.LogError("Enemy.cs: Couldn't fetch Character Component. Check if the script was added to the Player!");
        }

        destinationObject = this.gameObject.GetComponent<AIDestinationSetter>();
        if (destinationObject == null)
        {
            Debug.LogError("Enemy.cs: Couldn't find DestinationSetter script! Please make sure it is added to the object");
        } else
        {
            destinationObject.target = moveablePlayer.transform;
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
        //check for distance to moveablePlayer here -> define if either idle, running or attacking depending on currentAnimation
        //only every 10 seconds?

        //Debug.Log(pathSetter.velocity.magnitude.ToString());


        animator.SetInteger("animationState",currentAnimation);
        animator.SetBool("isFlying", isFlying);
        animator.SetBool("isSleeping", isAsleep);
        animator.SetBool("isAttacking",isAttacking);


        //Debug.Log("Checking if distance is large enough...");
        switch (GetDistanceTomoveablePlayer())
            {
                //--------------------------------------------------------------------------------------------- IF moveablePlayer IS/COMES IN RANGE
                case < Constants.DISTANCE_TO_ENEMY_UNTIL_MOVE:
                    if (pathSetter.canMove == false)
                    {
                        //Debug.Log("Distance large enough, setting moveablePlayer");
                        pathSetter.canMove = true;
                    }

                    currentAnimation = 1;
                    isAsleep = false;
                    isFlying = false;

                    if (pathSetter.velocity.magnitude <= 0.5)//-------------------------------------------- IF ENEMY DOESNT MOVE
                    {

                    if (GetDistanceTomoveablePlayer() <= Constants.ATTACKING_DISTANCE)//--------------------------------------------------------- CHECK IF Close to moveablePlayer
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

                //--------------------------------------------------------------------------------------------- IF moveablePlayer GOES OUT OF RANGE
                case > Constants.DISTANCE_TO_ENEMY_UNTIL_MOVE:
                    

                    isFlying = decideIfFlying();//only when to distance is again large enough, decide if the dragon should fly

                    //Debug.Log("Distance is not large enough - Null");
                    if (pathSetter.canMove)
                    {
                        //Debug.Log("Distance large enough, setting moveablePlayer");
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
        Debug.Log("distance to moveablePlayer" + GetDistanceTomoveablePlayer());
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
        //Get Health Params of moveablePlayer here and reduce it!
        if (!characterComponent.IsAttackable())
        {
            characterComponent.ChangeCurrHealth(Random.Range(Constants.HEALTH_DAMAGE_APRX-3, Constants.HEALTH_DAMAGE_APRX + 3));
        }
    }

    float GetDistanceTomoveablePlayer()
    {
        Vector3 moveablePlayerPosition = moveablePlayer.transform.position;
        Vector3 enemyPosition = this.gameObject.transform.position;
        //Debug.Log((moveablePlayerPosition - enemyPosition).magnitude);
        return (moveablePlayerPosition-enemyPosition).magnitude;
    }

    GameObject FindmoveablePlayer()
    {
        GameObject moveablePlayer = GameObject.Find("FollowHead");

      //GameObject moveablePlayer = GameObject.Find("moveablePlayer");

        if (moveablePlayer == null)
        {
            moveablePlayer = GameObject.Find("followhead");
        }

        if (moveablePlayer == null)
        {
            Debug.LogError("Enemy.cs: Couldn't find moveablePlayer!");
        }

        return moveablePlayer;
    }

    GameObject FindPlayer()
    {
        GameObject player = GameObject.Find("Player");

        //GameObject moveablePlayer = GameObject.Find("moveablePlayer");

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
            Debug.LogError("Enemy.cs: Couldn't find player!");
        }

        return player;
    }
}
