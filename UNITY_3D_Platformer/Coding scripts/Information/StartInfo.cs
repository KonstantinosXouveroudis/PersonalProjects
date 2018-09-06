using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartInfo : MonoBehaviour {

    //An array of the messages we want to display at the start.
    public InfoMessage[] messages;

	void OnTriggerEnter(Collider col)
    {
        //When the player collides on this object, set the text and color for every message in the array.
        if (col.tag == "Player")
        {
            foreach(InfoMessage m in messages)
            {
                m.popup.text = m.message;
                m.popup.color = m.color;
                //m.animator.Play("Text_Slide", -1, 0f);
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        //When the player gets out of the object, destroy the messages.
        if (col.tag == "Player")
        {
            foreach (InfoMessage m in messages)
            {
                Destroy(m.popup);
            }
        }
    }
}
