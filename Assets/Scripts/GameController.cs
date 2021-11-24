using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    }

    //TODO OnSceneChange Event -> getScene

    //function PlayerDied() get position ->
        //loadScene -> PlayerDied
            //Todo (innerhalb Scene -> )
            //Raum -> Mit Auswahlelementen -> 
}