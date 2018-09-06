using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWhenPlayerIsOn : MonoBehaviour
{

    private int direction = 1;

    
    float platformSpeed = 0;

    [SerializeField]
    float multiplySpeed = 1;

    [SerializeField]
    float vectorX;
    [SerializeField]
    float vectorY;
    [SerializeField]
    float vectorZ;

    Vector3 go;

    // Update is called once per frame
    void FixedUpdate()
    {
        go = new Vector3(vectorX, vectorY, vectorZ);
        transform.Translate(go * platformSpeed * multiplySpeed * direction * Time.fixedDeltaTime);

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Target")
        {
            if (direction == 1)
            {
                direction = -1;
            }
            else if (direction == -1)
            {
                direction = 1;
            }
        }
        else if (col.tag == "Stop")
        {
            direction = 0;
        }
        //Used so that the player will move along with the platform when on it.
        else if (col.tag == "Player")
        {
            platformSpeed = 1;
            col.transform.parent = transform;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            //platformSpeed = 0;
            col.transform.parent = null;
        }

    }
}