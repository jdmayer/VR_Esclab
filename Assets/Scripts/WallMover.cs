using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

/// <summary>
/// Author: Janine Mayer
/// Helps to check all breakable walls in the current scene
/// Only one wall can be highlighted and selected at a time
/// If the player goes out of a certain range (minDistance) 
/// the selection or highlight is removed
/// </summary>
public class WallMover : MonoBehaviour
{
    public float minDistance = 1.2f;

    public SteamVR_Action_Boolean Selection;
    public SteamVR_Action_Boolean Rotation;
    public SteamVR_Input_Sources handType;

    public GameObject rightController;

    private Material breakable;
    private Material breakableHighlighted;
    private Material breakableSelected;
    private Material breakableSelectedInvalid;

    private IList<GameObject> breakableWalls = new List<GameObject>();
    private GameObject currentWall;
    private Transform originalPositionWall;
    private bool isSelected = false;


    void Start()
    {
        Selection.AddOnStateDownListener(Select, handType);
        Selection.AddOnStateUpListener(Deselect, handType);
        Rotation.AddOnStateUpListener(RotateWall, handType);

        breakable =
            Resources.Load(StringConstants.MATERIAL_BREAKABLE, typeof(Material)) as Material;
        breakableHighlighted = 
            Resources.Load(StringConstants.MATERIAL_BREAKABLE_HIGHLIGHTED, typeof(Material)) as Material;
        breakableSelected =
            Resources.Load(StringConstants.MATERIAL_BREAKABLE_SELECTED, typeof(Material)) as Material;
        breakableSelectedInvalid =
            Resources.Load(StringConstants.MATERIAL_BREAKABLE_SELECTED_INVALID, typeof(Material)) as Material;

        foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        { 
            if (go.name.Contains(StringConstants.BREAKABLE_WALL) && go.activeInHierarchy) {
                breakableWalls.Add(go);
            }
        }
    }

    void Update()
    {
        CheckWalls();
    }

    public void Select(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (currentWall == null)
        {
            Debug.Log("No wall to select");
            return;
        }

        currentWall.GetComponent<Renderer>().material = breakableSelected;

        StartCoroutine(StringConstants.MOVE_WALL);
        isSelected = true;

    }

    // TODO - check if wall is highlighted after deselection
    public void Deselect(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (currentWall == null)
        {
            Debug.Log("No wall to deselect");
            return;
        }

        SetWall();

        StopCoroutine(StringConstants.MOVE_WALL);

        currentWall.GetComponent<Renderer>().material = breakableHighlighted;
        currentWall = null;

        isSelected = false;
    }

    private void RotateWall(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (currentWall == null || !isSelected)
        {
            return;
        }

        currentWall.transform.RotateAround(currentWall.transform.position, Vector3.up, 90);
    }

    private void CheckWalls()
    {
        if (IsCurrentWallValid())
        {
            return;
        }

        if (currentWall != null)
        {
            currentWall.GetComponent<Renderer>().material = breakable;
            currentWall = null;
            isSelected = false; 
            StopCoroutine(StringConstants.MOVE_WALL);
        }

        GameObject closestVisibleWall = null;
        var distance = minDistance;
        foreach (var wallObject in breakableWalls)
        {
            if (!wallObject.GetComponent<Renderer>().isVisible)
            {
                continue;
            }

            var currDistance = Vector3.Distance(gameObject.transform.position, wallObject.transform.position);
            if (currDistance < distance)
            {
                closestVisibleWall = wallObject;
                distance = currDistance;
            }
        }

        if (closestVisibleWall != null)
        {
            currentWall = closestVisibleWall;
            currentWall.GetComponent<Renderer>().material = breakableHighlighted;
            originalPositionWall = currentWall.transform;
        }
    }

    private bool IsCurrentWallValid()
    {
        if (currentWall == null)
        {
            return false;
        }

        // user is allowed to have a bigger distance when moving the wall
        // happens e.g. due to rotation
        if (isSelected) 
        {
            return true;
        }

        var isVisible = currentWall.GetComponent<Renderer>().isVisible;
        var distance = Vector3.Distance(gameObject.transform.position, currentWall.transform.position);

        return isVisible && distance <= minDistance;
    }

    private void SetWall()
    {
        if (currentWall.GetComponent<Renderer>().material == breakableSelectedInvalid)
        {
            Debug.Log("NOT VALID POSITION!");
            currentWall.transform.rotation = originalPositionWall.rotation;
            currentWall.transform.position = originalPositionWall.position;
        }
    }

    private IEnumerator MoveWall()
    {
        var prevX = rightController.transform.position.x;
        var prevZ = rightController.transform.position.z;

        while (true)
        {
            var currX = rightController.transform.position.x;
            var currZ= rightController.transform.position.z;

            currentWall.transform.position += new Vector3(currX - prevX, 0, currZ - prevZ);

            //currentWall.GetComponent<Renderer>().material = breakableSelectedInvalid;

            prevX = rightController.transform.position.x;
            prevZ = rightController.transform.position.z;

            yield return null;
        }
    }
}
