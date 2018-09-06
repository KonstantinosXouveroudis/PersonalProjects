using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextOnCollision : MonoBehaviour {

    public Animator animator;
    public Text popupText;

	// Use this for initialization
	void Start () {

        //Receive clips of our object's animator.
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);

        Destroy(gameObject, clipInfo[0].clip.length);
        popupText = animator.GetComponent<Text>();
	}

    public void SetText(string text)
    {
        animator.GetComponent<Text>().text = text;
    }
}
