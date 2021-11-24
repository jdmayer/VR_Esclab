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
    private GameObject originalPositionWall;
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

    public void Deselect(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (currentWall == null)
        {
            Debug.Log("No wall to deselect");
            return;
        }

        SetWall();

        StopCoroutine(StringConstants.MOVE_WALL);

        isSelected = false;

        //reset highlight in case wall is still in range
        if (IsCurrentWallValid())
        {
            currentWall.GetComponent<Renderer>().material = breakableHighlighted;
        }
        else
        {
            currentWall.GetComponent<Renderer>().material = breakable;
        }

        currentWall = null;
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
            originalPositionWall = new GameObject();
            originalPositionWall.transform.position = new Vector3(currentWall.transform.position.x, currentWall.transform.position.y, currentWall.transform.position.z); 
            originalPositionWall.transform.rotation = currentWall.transform.rotation;
        }
    }

    private bool IsCurrentWallValid()
    {
        if (currentWall == null)
        {
            isSelected = false;
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
        // due to long names the check for names does not work!
        if (currentWall.GetComponent<Renderer>().material.color == breakableSelectedInvalid.color)
        {
            currentWall.transform.position = new Vector3(originalPositionWall.transform.position.x, originalPositionWall.transform.position.y, originalPositionWall.transform.position.z);
            currentWall.transform.rotation = originalPositionWall.transform.rotation;
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

            prevX = rightController.transform.position.x;
            prevZ = rightController.transform.position.z;

            yield return null;
        }
    }
}
