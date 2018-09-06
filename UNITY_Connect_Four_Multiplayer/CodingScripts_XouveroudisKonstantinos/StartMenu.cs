using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

    public InputField player1Name;
    public InputField player2Name;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnEnable()
    {
        //Tell our ‘OnLevelFinishedLoading’ function to start listening for a scene change event as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        //Tell our ‘OnLevelFinishedLoading’ function to stop listening for a scene change event as soon as this script is disabled.
        //Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {

    }

    public void StartGame()
    {
        //In case no names were inputed, set the names to Player 1 & 2.
        if (player1Name.text == "")
            player1Name.text = "Player 1";

        if (player2Name.text == "")
            player2Name.text = "Player 2";

        //Set player names on MyGameController's static variables.
        MyGameController.player1Name = player1Name.text;
        MyGameController.player2Name = player2Name.text;

        //Load Connect Four level.
        SceneManager.LoadScene(1);
    }
}
