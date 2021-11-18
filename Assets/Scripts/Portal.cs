using Assets.Scripts;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Author: Janine Mayer
/// Portal and Player need to have a Box Collider component
/// IsTrigger needs to be checked in the Box Collider component of Portal
/// Player needs to possess a Rigidbody component to trigger method
/// Scenes need to be added to the Build Settings so the SceneManager can load them
/// </summary>
public class Portal : MonoBehaviour
{
    private void ChangeScene()
    {
        var currentScene = SceneManager.GetActiveScene().name;
        var currentSceneIndex = Array.IndexOf(Constants.labyrinthScenes, currentScene);

        var newSceneIndex = currentSceneIndex + 1 == Constants.labyrinthScenes.Length ? 0 : currentSceneIndex + 1;
        var newScene = Constants.labyrinthScenes[newSceneIndex];

        Debug.Log("Portal entered, move from " + currentScene + " to " + newScene);
        SceneManager.LoadScene(newScene);
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.name == Constants.player || collision.name == Constants.manualPlayer)
        {
            ChangeScene();
        }
    }
}
