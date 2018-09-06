using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    //private float speed = 1;
    private int direction = 1;

    [SerializeField]
    float platformSpeed = 1;

    //[SerializeField]
    //string which_way;

    [SerializeField]
    float vectorX;
    [SerializeField]
    float vectorY;
    [SerializeField]
    float vectorZ;

    Vector3 go;
    
      

	// Update is called once per frame
	void FixedUpdate () {
        go = new Vector3(vectorX, vectorY, vectorZ);
        transform.Translate(go * platformSpeed * direction * Time.fixedDeltaTime);

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
            col.transform.parent = transform;
            
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player") {
            col.transform.parent = null;
        }
    }

    /*
OLDER CODE:
    //Setting the way in which the platform will be going.
    //Putting it on start() so it won't change mid-level.
    switch (which_way)
    {
        case "up":
            go = Vector3.up;
            break;

        case "down":
            go = Vector3.down;
            break;

        case "forward":
            go = Vector3.forward;
            break;

        case "back":
            go = Vector3.back;
            break;

        case "up_forward":
            go = Vector3.up + Vector3.forward;
            break;

        case "up_back":
            go = Vector3.up + Vector3.back;
            break;

        case "down_forward":
            go = Vector3.down + Vector3.forward;
            break;

        case "down_back":
            go = Vector3.down + Vector3.back;
            break;

        default:
            go = new Vector3(0, 0, 0);
            break;
    }*/
}
