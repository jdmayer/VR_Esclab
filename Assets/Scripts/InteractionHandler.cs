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

    private Character GetCharacter()
    {
        var playerGameObject = GameObject.Find(StringConstants.PLAYER);
        return playerGameObject.GetComponent<Character>();
    }
}
