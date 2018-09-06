using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCameraKeyboard : MonoBehaviour {

    public float speed;
    private Vector3 localRotation;
	
	void LateUpdate () {
 

        //Press H to rotate camera to the left.
        if (Input.GetKey(KeyCode.H))
        {
            localRotation = new Vector3(0, -speed * Time.deltaTime, 0);
            transform.Rotate(localRotation);
        }

        //Press K to rotate camera to the right.
        if (Input.GetKey(KeyCode.K))
        {
            localRotation = new Vector3(0, speed * Time.deltaTime, 0);
            transform.Rotate(localRotation);
        }


        /* Addition inputs to rotate the camera up (U) and down (J).
        if (Input.GetKey(KeyCode.U))
        {
            localRotation = new Vector3(speed * Time.deltaTime, 0, 0);
            transform.Rotate(localRotation);   
        }

        if (Input.GetKey(KeyCode.J))
        {
            localRotation = new Vector3(-speed * Time.deltaTime, 0, 0);
            transform.Rotate(localRotation);
        }
        */
    }
}
