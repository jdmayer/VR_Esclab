using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Author: Janine Mayer
/// Class to set fill of statbars (generically usable)
/// </summary>
public class StatusBar : MonoBehaviour
{
    public Image Bar;

    public TextMeshProUGUI CurrText;
    public TextMeshProUGUI MaxText;

    void Start()
    {
    }

    private void Update()
    {        
    }

    public void UpdateStatBar(float fraction, int currValue, int maxValue)
    {
        Bar.fillAmount = fraction;
        CurrText.text = currValue.ToString();
        MaxText.text = maxValue.ToString();
    }
}
