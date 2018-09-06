using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmark : MonoBehaviour {
    //public string popupMessage;
    
    void Start()
    {
        TextOnCollisionController.Initialize();
    }

	public void EnterLandmark(string popupMessage)
    {
        TextOnCollisionController.CreateText(popupMessage);

    }
}
