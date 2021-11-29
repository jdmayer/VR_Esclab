using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameTrigger : MonoBehaviour
{
    public AudioSource startGame;
    // Start is called before the first frame update
    void Start()
    {
        //Intentionally left empty
    }

    // Update is called once per frame
    void Update()
    {
        //Intentionally left empty
    }

    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains(StringConstants.HAND_COLLIDER))
        {
            GameController gc = GameObject.Find(StringConstants.GAME_CONTROLLER).GetComponent<GameController>();
            if (gc == null)
            {
                Debug.LogError("StartGameTrigger.cs: Couldn't find GameController!");
            } else
            {
                startGame.Play();
                gc.GameStart();
            }
        }
    }
}
