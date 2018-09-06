using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextOnCollisionController : MonoBehaviour {

    private static TextOnCollision popupText;
    private static GameObject canvas;
    
	public static void Initialize()
    {
        canvas = GameObject.Find("Canvas");
        if(!popupText)
            popupText = Resources.Load<TextOnCollision>("prefabs/PopupText");
    }

    public static void CreateText(string text)
    {
        TextOnCollision instance = Instantiate(popupText);

        //instance.transform.SetParent(canvas.transform, false);
        instance.SetText(text);
    }
}
