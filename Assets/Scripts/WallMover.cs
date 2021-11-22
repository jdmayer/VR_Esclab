using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private Material breakable;
    private Material breakableHighlighted;
    private Material breakableSelected;

    private IList<GameObject> breakableWalls = new List<GameObject>();
    private GameObject currentWall;
    private bool isSelected = false;

    void Start()
    {
        breakable =
            Resources.Load(Constants.materialBreakable, typeof(Material)) as Material;
        breakableHighlighted = 
            Resources.Load(Constants.materialBreakableHighlighted, typeof(Material)) as Material;
        breakableSelected =
            Resources.Load(Constants.materialBreakableSelected, typeof(Material)) as Material;

        foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        { 
            if (go.name.Contains(Constants.breakableWall) && go.activeInHierarchy) {
                breakableWalls.Add(go);
            }
        }
    }

    void Update()
    {
        CheckWalls();

        if (currentWall != null && Input.GetKeyDown(KeyCode.M))
        {
            if (isSelected)
            {
                SetWall();
            }
            else
            {
                SelectWall();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RotateWall();
        }
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
            isSelected = false; //TODO - what if it is moved
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
        }
    }

    private bool IsCurrentWallValid()
    {
        if (currentWall == null)
        {
            return false;
        }

        var isVisible = currentWall.GetComponent<Renderer>().isVisible;
        var distance = Vector3.Distance(gameObject.transform.position, currentWall.transform.position);

        return isVisible && distance <= minDistance;
    }

    void SelectWall()
    {
        isSelected = true;
        currentWall.GetComponent<Renderer>().material = breakableSelected;

        Debug.Log("selected wall");
    }

    void SetWall()
    {
        Debug.Log("set wall");
        currentWall.GetComponent<Renderer>().material = breakableHighlighted;

        isSelected = false;
    }



    private void RotateWall()
    {
        if (currentWall == null || !isSelected)
        {
            return;
        }

        currentWall.transform.RotateAround(currentWall.transform.position, Vector3.up, 90);
    }

    // TODO
    // move around
    // rotate
    // set on new position
    // collision check!
}
