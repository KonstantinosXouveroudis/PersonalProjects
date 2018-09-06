using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayOnPlatform : MonoBehaviour
{


    private Rigidbody rb;

    void OnTriggeEnter(Collider col)
    {
        if (col.tag == "Plaform")
        {
            
            transform.Translate(col.attachedRigidbody.velocity * Time.fixedDeltaTime);
            //player.velocity = platform.velocity;
            //col.transform.parent = transform;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
        //    col.transform.parent = null;
        }



    }
}
