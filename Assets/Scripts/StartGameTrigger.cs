using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Intentionally left empty
    }

    // Update is called once per frame
    void Update()
    {
        //Intentionally left empty
    }

    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("HandCollider"))
        {
            //LoadFirstScene
        }
    }
}
