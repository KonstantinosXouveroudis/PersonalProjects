using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManagement : MonoBehaviour {

    Collider objectCollider;
    float distToGround;

	// Use this for initialization
	void Start () {
        objectCollider = gameObject.GetComponent<Collider>();
        distToGround = objectCollider.bounds.extents.y;
	}
	
    bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("space") && isGrounded())
        {
            FindObjectOfType<AudioManager>().Play("JumpSound");
        }
	}
}
