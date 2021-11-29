using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Axel Bauer
/// </summary>
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

    private void OnTriggerEnter(Collider other)
    {
        var gameController = GameObject.Find("GameController");
        var gc = gameController.GetComponent<GameController>() as GameController;

        if (gc == null)
        {
            Debug.LogError("StartGameTrigger.cs: Couldn't find GameController!");
        }
        else
        {
            startGame.Play();
            gc.GameStart();
        }
    }
}
