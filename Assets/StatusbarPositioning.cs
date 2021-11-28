using UnityEngine;
using Valve.VR;

/// <summary>
/// Author: Janine Mayer
/// Adjust visibility and positionof canvas with statusbars depending on controller input and position
/// </summary>
public class StatusbarPositioning : MonoBehaviour
{
    public Camera Camera;

    public SteamVR_Action_Boolean Display;
    public SteamVR_Input_Sources HandType;

    public GameObject leftController;

    private Canvas canvasObject = null;

    void Start()
    {
        Display.AddOnStateDownListener(DisplayStatsBar, HandType);
        Display.AddOnStateUpListener(HideStatsBar, HandType);

        canvasObject = gameObject.GetComponentInParent<Canvas>();
        canvasObject.enabled = false;
    }

    void Update()
    {
        if (leftController)
        {
            gameObject.transform.position = new Vector3(
                leftController.transform.position.x + 0.5f,
                leftController.transform.position.y + 0.5f,
                leftController.transform.position.z);
        }

        if (Camera)
        {
            gameObject.transform.LookAt(Camera.transform);  
        }
    }


    private void DisplayStatsBar(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        canvasObject.enabled = true;
    }

    private void HideStatsBar(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        canvasObject.enabled = false;
    }
}
