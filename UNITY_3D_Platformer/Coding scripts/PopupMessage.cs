using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupMessage : MonoBehaviour {

    [SerializeField] Text popup;
    [SerializeField] string message;
    [SerializeField] Color color;
    [SerializeField] Animator animator;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            popup.color = color;
            popup.text = message;
            animator.Play("Text_Slide", -1, 0f);
        }
    }
}
