using Assets.Scripts;
using UnityEngine;

/// <summary>
/// Author: Janine Mayer
/// Utilisation of OnTriggerEnter and OnTriggerExit as walls may not be kinematic which is a condition to use OnCollisionEnter and OnCollisionExit
/// </summary>
public class PositionChecker : MonoBehaviour
{    
    private Material breakableSelected;
    private Material breakableSelectedInvalid;

    private int collisionCounter = 0;
 
    void Start()
    {
        breakableSelected =
            Resources.Load(StringConstants.MATERIAL_BREAKABLE_SELECTED, typeof(Material)) as Material;
        breakableSelectedInvalid =
            Resources.Load(StringConstants.MATERIAL_BREAKABLE_SELECTED_INVALID, typeof(Material)) as Material;
    }

    void Update()
    {
    }

    private void OnTriggerEnter(Collider collider)
    {
        collisionCounter++;
        gameObject.GetComponent<Renderer>().material = breakableSelectedInvalid;
    }

    private void OnTriggerExit(Collider collider)
    {
        collisionCounter--;

        if (collisionCounter == 0)
        {
            gameObject.GetComponent<Renderer>().material = breakableSelected;
        }
    }
}
