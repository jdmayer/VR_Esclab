using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Author: Janine Mayer
/// Class to set fill of statbars (generically usable)
/// </summary>
public class StatusBar : MonoBehaviour
{
    public Image Bar;

    public void UpdateStatBar(float fraction)
    {
        Bar.fillAmount = fraction;
    }
}
