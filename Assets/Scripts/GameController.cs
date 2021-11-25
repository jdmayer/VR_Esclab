using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameController : MonoBehaviour
{
    private static GameController _instance;

    public AudioSource soundtrack;
    public AudioSource startingAndGameOverSound;



    public static GameController instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameController>();

                //Tell unity not to destroy this object when loading a new scene!
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            //If I am the first instance, make me the Singleton
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            //If a Singleton already exists and you find
            //another reference in scene, destroy it!
            if (this != _instance)
                Destroy(this.gameObject);
        }
    }

    public void Play()
    {
        //Play some audio!
        soundtrack.Play();
        //TODO check in which scene you are and then play soundtrack or other depending on that
    }


    public void GameOver(Transform playerPosition) //passed transform for rotation as well as position
    {
        ///*Vector3*/ playerPosition = GetMoveablePlayer().transform.position;

        //TODO load new scene

        //TODO get player and add the position to him/her

        //TODO Player.Reset();

    }

    public void GameWon(Transform playerPosition) //passed transform for rotation as well as position 
    {
        //TODO check if last scene
        //TODO get all Obstacles objects
        //TODO create a ground plane below you which will be static and lower everything else -> it will look like you will go higher! -> Text: Congrats on making the game!
    }

    public static GameObject GetPlayer()
    {
        GameObject player = GameObject.Find(StringConstants.PLAYER);

        if (player == null)
        {
            player = GameObject.Find(StringConstants.PLAYER);
        }

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag(StringConstants.PLAYER);
        }
        if (player == null)
        {
            Debug.LogError("Enemy.cs: Couldn't find player!");
        }

        return player;
    }

    public static GameObject GetMoveablePlayer()
    {
        GameObject moveablePlayer = GameObject.Find(StringConstants.VR_CAMERA);

        if (moveablePlayer == null)
        {
            moveablePlayer = GameObject.Find(StringConstants.VR_CAMERA);
        }

        if (moveablePlayer == null)
        {
            Debug.LogError("Enemy.cs: Couldn't find moveablePlayer!");
        }

        return moveablePlayer;
    }

    public GameObject GetEnemy()
    {
        GameObject enemy = GameObject.Find("Enemy");

        if (enemy == null)
        {
            enemy = GameObject.Find("enemy");
        }

        if (enemy == null)
        {
            enemy = GameObject.Find("Red");
        }


        if (enemy == null)
        {
            enemy = GameObject.Find("AI");
        }

        if (enemy == null)
        {
            enemy = GameObject.FindGameObjectWithTag("Enemy");
        }
        if (enemy == null)
        {
            Debug.LogError("Enemy.cs: Couldn't find player!");
        }

        return enemy;
    }
}