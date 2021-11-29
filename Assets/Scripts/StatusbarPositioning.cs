using Assets.Scripts;
using UnityEngine;
using Valve.VR;

/// <summary>
/// Author: Janine Mayer
/// Adjust visibility and positionof canvas with statusbars depending on controller input and position
/// Need to find gameobject instance after scenechange as the previous game object will be destroyed!
/// </summary>
public class StatusbarPositioning : MonoBehaviour
{
    public Camera Camera;

    public SteamVR_Action_Boolean Display;
    public SteamVR_Input_Sources HandType;

    public GameObject leftController;

    private GameObject canvasInstance;

    void Start()
    {
        Display.AddOnStateDownListener(DisplayStatsBar, HandType);
        Display.AddOnStateUpListener(HideStatsBar, HandType);

        canvasInstance = gameObject;
        canvasInstance.SetActive(false);
    }

    void Update()
    {
        if (leftController && canvasInstance)
        {
            canvasInstance.transform.position = new Vector3(
                leftController.transform.position.x,
                leftController.transform.position.y + 0.1f,
                leftController.transform.position.z);
        }

        if (Camera)
        {
            canvasInstance.transform.LookAt(Camera.transform);  
        }
    }


    private void DisplayStatsBar(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (!canvasInstance)
        {
            foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
            {
                if (go.name.Contains(StringConstants.StatsBar_Level))
                {
                    canvasInstance = go;
                }
            }
        }

        canvasInstance.SetActive(true);      
    }

    private void HideStatsBar(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (!canvasInstance)
        {
            foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
            {
                if (go.name.Contains(StringConstants.StatsBar_Level) && go.activeInHierarchy)
                {
                    canvasInstance = go;
                }
            }
        }

        canvasInstance.SetActive(false);
    }
}
