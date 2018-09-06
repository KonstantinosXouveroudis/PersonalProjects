using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform2 : MonoBehaviour {

    //Using [SerializeField] so that the variables show up in the inspector.
    [SerializeField]
    Transform platform;

    [SerializeField]
    Transform startTransform;

    [SerializeField]
    Transform endTransform;

    [SerializeField]
    float platformSpeed;

    Vector3 direction;
    Transform destination;

	// Use this for initialization
	void Start () {
        SetDestination(startTransform);
	}

    //Used for physics adjustment
    //FixedUpdate() is run in a Fixed Timestep, which can be edited in Unity's Project Settings.
    void FixedUpdate()
    {
        //platform.rigidbody.MovePosition(platform.position * Vector3.forward * platformSpeed * Time.fixedDeltaTime); //OLD
        platform.GetComponent<Rigidbody>().MovePosition(platform.position + direction * platformSpeed * Time.fixedDeltaTime);

        //Used to change destination.
        if (Vector3.Distance(platform.position,destination.position) < (platformSpeed * Time.fixedDeltaTime))
        {
            SetDestination(destination == startTransform ? endTransform : startTransform);
            //If (destination == startTransform) is true, then set destination to endTransform.
            //If (destination == startTransform) is false, then set destination to startTransform)
        }
    }

    void OnDrawGizmos()
    {
        //We draw Wirecubes representing the Start and End Positions (green and red respectively)
        //of the Platform in the Scene.
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(startTransform.position, platform.localScale);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(endTransform.position, platform.localScale);
    }

    void SetDestination(Transform dest)
    {
        destination = dest;
        direction = (destination.position - platform.position).normalized;
    }
}
