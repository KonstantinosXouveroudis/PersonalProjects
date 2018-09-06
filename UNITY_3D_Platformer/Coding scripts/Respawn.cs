using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour {

    private Animator anim;

    //Level numbers in the Built Settings start from 0.
    //Setting as public static so that when we change scenes/levels, it will change to that level's number. 
    public static int levelNumber = 0;

    private Vector3 startPos;
    private Quaternion startRot;
    private int respawnCounter = 0;

    private int randomNum;

    // Use this for initialization
    void Start () {

        randomNum = Random.Range(0, 2);     // 0 or 1, for Goal Sound Effect.
        anim = GetComponent<Animator>();    //Link to the Animator Controller of the object.

        //Saving the position and rotation in which the player starts the level.
        startPos = transform.position;
        startRot = transform.rotation;
	}
	
    void Update() {

        if (Input.GetKeyDown("1"))
        {
            anim.Play("WAIT01", -1, 0f);
        }


        /*
        if (Input.GetKeyDown("space"))
        {
            FindObjectOfType<AudioManager>().Play("JumpSound");
        }
        */

    }

    void NextLevel() {
        levelNumber++;
        //If levelNumber is bigger than the number of the last level, set levelNumber to 0 so that the levels will loop.
        if (levelNumber > 3)
        {
            levelNumber = 0;
        }
        SceneManager.LoadScene(levelNumber);
    }
	
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Respawn")
        {
            transform.position = startPos;
            transform.rotation = startRot;
            respawnCounter++;

            //If the player dies more than 5 times in the same stage, the animation will stop playing when respawning.
            if (respawnCounter <= 5) 
                GetComponent<Animator>().Play("LOSE00", -1, 0f);

            GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f); //Player won't move while respawning.
            GetComponent<Rigidbody>().angularVelocity = new Vector3(0f, 0f, 0f); //Player won't turn while respawning.
            
        }
        else if (col.tag == "Checkpoint")
        {
            startPos = col.transform.position;
            startRot = col.transform.rotation;

            //Look through the AudioManager and play sound effect.
            FindObjectOfType<AudioManager>().Play("GetCheckpoint");

            Destroy(col.gameObject);
        }
        else if (col.tag == "Goal")
        {
            respawnCounter = 0;
            GetComponent<Animator>().Play("WIN00", -1, 0f);

            //Look through the AudioManager and play sound effect.
            FindObjectOfType<AudioManager>().Play("GetGoal");

            //50% chance of playing either sound effect.
            if(randomNum == 0)
                FindObjectOfType<AudioManager>().Play("PQ_Aha");
            else
                FindObjectOfType<AudioManager>().Play("PQ_Laugh");

            Invoke("NextLevel", 2f);
            Destroy(col.gameObject);
        }
    }
}
