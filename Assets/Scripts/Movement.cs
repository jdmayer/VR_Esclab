using UnityEngine;

/// <summary>
/// Author: Janine Mayer
/// Script to move object forward, backward and enable rotation around y-axis (yaw)
/// Used to test functionality of character and items without hmd hardware
/// </summary>
public class Movement : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    public float rotationRate = 5f;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.RotateAround(transform.position, new Vector3(0, 1, 0), rotationRate);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.RotateAround(transform.position, new Vector3(0, 1, 0), -rotationRate);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * Time.deltaTime * moveSpeed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * Time.deltaTime * moveSpeed;
        }
    }
}


// https://answers.unity.com/questions/1743970/make-player-not-go-through-walls.html