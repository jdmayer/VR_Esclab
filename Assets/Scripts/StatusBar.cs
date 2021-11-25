using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

/// <summary>
/// Author: Janine Mayer
/// Class to set fill of statbars (generically usable)
/// </summary>
public class StatusBar : MonoBehaviour
{
    public Image Bar;
    public Camera Camera;

    public TextMeshProUGUI CurrText;
    public TextMeshProUGUI MaxText;

    public SteamVR_Action_Boolean Display;
    public SteamVR_Input_Sources HandType;

    private Canvas parentCanvas;

    void Start()
    {
        Display.AddOnStateDownListener(DisplayStatsBar, HandType);
        Display.AddOnStateUpListener(HideStatsBar, HandType);

        parentCanvas = gameObject.GetComponentInParent<Canvas>();
        parentCanvas.enabled = false;
    }

    private void Update()
    {
        parentCanvas.transform.LookAt(Camera.transform);
    }

    private void DisplayStatsBar(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        parentCanvas.enabled = true;
    }

    private void HideStatsBar(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        parentCanvas.enabled = false;
    }

    public void UpdateStatBar(float fraction, int currValue, int maxValue)
    {
        Bar.fillAmount = fraction;
        CurrText.text = currValue.ToString();
        MaxText.text = maxValue.ToString();
    }
}
