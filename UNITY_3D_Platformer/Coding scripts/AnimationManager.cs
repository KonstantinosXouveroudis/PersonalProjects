using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {

    private Animator anim;

    private float idleTimer = 0f;

	// Use this for initialization
	void Start () {
        idleTimer = 0f;
        anim = GetComponent<Animator>(); //Get object's attached animator.
    }
	
	// Update is called once per frame
	void Update () {

        //Increase timer.
        idleTimer += Time.deltaTime;

        if (Input.GetKeyDown("1"))
        {
            anim.Play("WAIT01", -1, 0f);
        }

        //If the player stays idle for a minute or more, play waiting animation and set timer to 0.
        if(idleTimer >= 60)
        {
            anim.Play("WAIT01", -1, 0f);
            idleTimer = 0;
        }

        //If player enters any input, reset the idle timer.
        if (Input.anyKey)
        {
            idleTimer = 0;
        }
    }
}
