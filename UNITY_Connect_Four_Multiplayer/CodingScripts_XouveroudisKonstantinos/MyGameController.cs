using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Note: This is for the most part NOT MY CODE. I just edited it a little to allow for a 2nd player to play.

public class MyGameController : MonoBehaviour {

    enum Piece
    {
        Empty = 0,
        Yellow = 1,
        Red = 2
    }

    [Range(3, 8)]
    public int numRows = 6;
    [Range(3, 8)]
    public int numColumns = 7;

    [Tooltip("How many pieces have to be connected to win.")]
    public int numPiecesToWin = 4;

    [Tooltip("Allow diagonally connected Pieces?")]
    public bool allowDiagonally = true;

    public float dropTime = 4f;

    // Gameobjects 
    public GameObject pieceRed;
    public GameObject pieceYellow;
    public GameObject pieceField;

    public GameObject winningText;
    public string player1WonText;
    public string player2WonText;
    public string drawText = "Draw!";

    public static string player1Name;
    public static string player2Name;

    public GameObject btnPlayAgain;
    bool btnPlayAgainTouching = false;
    Color btnPlayAgainOrigColor;
    Color btnPlayAgainHoverColor = new Color(255, 143, 4);

    GameObject gameObjectField;

    // temporary gameobject, holds the piece at mouse position until the mouse has clicked
    GameObject gameObjectTurn;

    /// <summary>
    /// The Game field.
    /// 0 = Empty
    /// 1 = Yellow
    /// 2 = Red
    /// </summary>
    int[,] field;

    bool isPlayer1Turn = true;
    bool isLoading = true;
    bool isDropping = false;
    bool mouseButtonPressed = false;

    bool gameOver = false;
    bool isCheckingForWinner = false;

    private Vector3 spawnPos;


    //Personal Variables
    public Text player1Controls;
    public Text player2Controls;
    public Text player1ScoreDisplay;
    public Text player2ScoreDisplay;
    private static int player1Score = 0;
    private static int player2Score = 0;

    // Use this for initialization
    void Start() {

        //blue, cyan, gray, green, grey, magenta, red, white, yellow
        //pieceYellow.GetComponent<Renderer>().sharedMaterial.color = Color.cyan;

        player1WonText = player1Name + " wins!";
        player2WonText = player2Name + " wins!";

        int max = Mathf.Max(numRows, numColumns);

        if (numPiecesToWin > max)
            numPiecesToWin = max;

        CreateField();
    }


    //This is called each time a scene is loaded.
    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        //Updating the score on the canvas.
        //Without this method, the UI Text will reset when the scene is reloaded.
        //player1ScoreDisplay.text = "Player 1: " + player1Score;
        player1ScoreDisplay.text = player1Name + ": " + player1Score;
        player2ScoreDisplay.text = player2Name + ": " + player2Score;
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

    void CreateField()
    {
        winningText.SetActive(false);
        btnPlayAgain.SetActive(false);

        isLoading = true;

        gameObjectField = GameObject.Find("Field");
        if (gameObjectField != null)
        {
            DestroyImmediate(gameObjectField);
        }
        gameObjectField = new GameObject("Field");

        // create an empty field and instantiate the cells
        field = new int[numColumns, numRows];
        for (int x = 0; x < numColumns; x++)
        {
            for (int y = 0; y < numRows; y++)
            {
                field[x, y] = (int)Piece.Empty;
                GameObject g = Instantiate(pieceField, new Vector3(x, y * -1, -1), Quaternion.identity) as GameObject;
                g.transform.parent = gameObjectField.transform;
            }
        }

        isLoading = false;
        gameOver = false;

        // center camera
        Camera.main.transform.position = new Vector3(
            (numColumns - 1) / 2.0f, -((numRows - 1) / 2.0f), Camera.main.transform.position.z);

        winningText.transform.position = new Vector3(
            (numColumns - 1) / 2.0f, -((numRows - 1) / 2.0f) + 1, winningText.transform.position.z);

        btnPlayAgain.transform.position = new Vector3(
            (numColumns - 1) / 2.0f, -((numRows - 1) / 2.0f) - 1, btnPlayAgain.transform.position.z);
    }

    /// <summary>
    /// Spawns a piece at mouse position above the first row
    /// </summary>
    /// <returns>The piece.</returns>
    GameObject SpawnPiece()
    {
        int middleColumn = (int) numColumns / 2;

        //Vector3 spawnPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spawnPos = new Vector3(middleColumn, numRows + 1, 0);

        GameObject g = Instantiate(
                isPlayer1Turn ? pieceYellow : pieceRed, // is players turn = spawn yellow, else spawn red
                new Vector3(
                Mathf.Clamp(spawnPos.x, 0, numColumns - 1),
                gameObjectField.transform.position.y + 1, 0), // spawn it above the first row
                Quaternion.identity) as GameObject;

        return g;
    }

    void UpdatePlayAgainButton()
    {

        RaycastHit hit;
        //ray shooting out of the camera from where the mouse is
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit) && hit.collider.name == btnPlayAgain.name)
        {
            btnPlayAgain.GetComponent<Renderer>().material.color = btnPlayAgainHoverColor;
            //check if the left mouse has been pressed down this frame
            if (Input.GetMouseButtonDown(0) || Input.touchCount > 0 && btnPlayAgainTouching == false)
            {
                btnPlayAgainTouching = true;

                //CreateField();
                //Application.LoadLevel(0); //Obselete.
                SceneManager.LoadScene(1);
            }
        }
        else
        {
            btnPlayAgain.GetComponent<Renderer>().material.color = btnPlayAgainOrigColor;
        }

        if (Input.touchCount == 0)
        {
            btnPlayAgainTouching = false;
        }
    }

    // Update is called once per frame
    void Update () {

        if (isLoading)
            return;

        if (isCheckingForWinner)
            return;

        if (gameOver)
        {
            winningText.SetActive(true);
            btnPlayAgain.SetActive(true);

            UpdatePlayAgainButton();

            return;
        }

        if (isPlayer1Turn)
        {
            if (gameObjectTurn == null)
            {
                gameObjectTurn = SpawnPiece();
            }
            else
            {

                if (Input.GetKeyDown(KeyCode.D))
                    MovePiece(1);
                if (Input.GetKeyDown(KeyCode.A))
                    MovePiece(-1);
                /*
                Vector3 startPosition = gObject.transform.position;
                Vector3 endPosition = new Vector3();

                // round to a grid cell
                int x = Mathf.RoundToInt(startPosition.x);
                startPosition = new Vector3(x, startPosition.y, startPosition.z);
                */

                if (Input.GetKeyDown(KeyCode.S) && !mouseButtonPressed && !isDropping)
                {
                    mouseButtonPressed = true;

                    StartCoroutine(dropPiece(gameObjectTurn));
                }
                else
                {
                    mouseButtonPressed = false;
                }
            }
        }
        else
        {
            if (gameObjectTurn == null)
            {
                gameObjectTurn = SpawnPiece();
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                    MovePiece(1);
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                    MovePiece(-1);

                if (Input.GetKeyDown(KeyCode.DownArrow) && !mouseButtonPressed && !isDropping)
                {
                    mouseButtonPressed = true;

                    StartCoroutine(dropPiece(gameObjectTurn));
                }
                else
                {
                    mouseButtonPressed = false;
                }
            }
        }
    }

    void MovePiece(int direction)
    {
        //If for some reason this stops working, try turning into into a Vector 3. Don't forget about the z-axis.
        Vector3 currentPosition = gameObjectTurn.transform.position;

        if( (direction == -1 && currentPosition.x != 0) || (direction == 1 && currentPosition.x != numRows) )
            gameObjectTurn.transform.position = new Vector3(currentPosition.x + direction, currentPosition.y, currentPosition.z);

    }

    /// <summary>
    /// This method searches for a empty cell and lets 
    /// the object fall down into this cell
    /// </summary>
    /// <param name="gObject">Game Object.</param>
    IEnumerator dropPiece(GameObject gObject)
    {
        isDropping = true;

        Vector3 startPosition = gObject.transform.position;
        Vector3 endPosition = new Vector3();

        // round to a grid cell
        int x = Mathf.RoundToInt(startPosition.x);
        startPosition = new Vector3(x, startPosition.y, startPosition.z);

        // is there a free cell in the selected column?
        bool foundFreeCell = false;
        for (int i = numRows - 1; i >= 0; i--)
        {
            if (field[x, i] == 0)
            {
                foundFreeCell = true;
                field[x, i] = isPlayer1Turn ? (int)Piece.Yellow : (int)Piece.Red;
                endPosition = new Vector3(x, i * -1, startPosition.z);

                break;
            }
        }

        if (foundFreeCell)
        {
            // Instantiate a new Piece, disable the temporary
            GameObject g = Instantiate(gObject) as GameObject;
            gameObjectTurn.GetComponent<Renderer>().enabled = false;

            float distance = Vector3.Distance(startPosition, endPosition);

            float t = 0;
            while (t < 1)
            {
                t += Time.deltaTime * dropTime * ((numRows - distance) + 1);

                g.transform.position = Vector3.Lerp(startPosition, endPosition, t);
                yield return null;
            }

            g.transform.parent = gameObjectField.transform;

            // remove the temporary gameobject
            DestroyImmediate(gameObjectTurn);

            // run coroutine to check if someone has won
            StartCoroutine(Won());

            // wait until winning check is done
            while (isCheckingForWinner)
                yield return null;

            isPlayer1Turn = !isPlayer1Turn;
        }

        isDropping = false;

        yield return 0;
    }

    /// <summary>
    /// Check for Winner
    /// </summary>
    IEnumerator Won()
    {
        isCheckingForWinner = true;

        for (int x = 0; x < numColumns; x++)
        {
            for (int y = 0; y < numRows; y++)
            {
                // Get the Laymask to Raycast against, if its Players turn only include
                // Layermask Yellow otherwise Layermask Red
                int layermask = isPlayer1Turn ? (1 << 8) : (1 << 9);

                // If its Players turn ignore red as Starting piece and wise versa
                if (field[x, y] != (isPlayer1Turn ? (int)Piece.Yellow : (int)Piece.Red))
                {
                    continue;
                }

                // shoot a ray of length 'numPiecesToWin - 1' to the right to test horizontally
                RaycastHit[] hitsHorz = Physics.RaycastAll(
                    new Vector3(x, y * -1, 0),
                    Vector3.right,
                    numPiecesToWin - 1,
                    layermask);

                // return true (won) if enough hits
                // Also increase score of winner.
                if (hitsHorz.Length == numPiecesToWin - 1)
                {


                    if(isPlayer1Turn)
                        player1ScoreDisplay.text = player1Name + ": " + ++player1Score;
                    else
                        player2ScoreDisplay.text = player2Name + ": " + ++player2Score;

                    gameOver = true;
                    break;
                }

                // shoot a ray up to test vertically
                RaycastHit[] hitsVert = Physics.RaycastAll(
                    new Vector3(x, y * -1, 0),
                    Vector3.up,
                    numPiecesToWin - 1,
                    layermask);

                if (hitsVert.Length == numPiecesToWin - 1)
                {
                    if (isPlayer1Turn)
                        player1ScoreDisplay.text = player1Name + ": " + ++player1Score;
                    else
                        player2ScoreDisplay.text = player2Name + ": " + ++player2Score;

                    gameOver = true;
                    break;
                }

                // test diagonally
                if (allowDiagonally)
                {
                    // calculate the length of the ray to shoot diagonally
                    float length = Vector2.Distance(new Vector2(0, 0), new Vector2(numPiecesToWin - 1, numPiecesToWin - 1));

                    RaycastHit[] hitsDiaLeft = Physics.RaycastAll(
                        new Vector3(x, y * -1, 0),
                        new Vector3(-1, 1),
                        length,
                        layermask);

                    if (hitsDiaLeft.Length == numPiecesToWin - 1)
                    {
                        if(isPlayer1Turn)
                            player1ScoreDisplay.text = player1Name + ": " + ++player1Score;
                        else
                            player2ScoreDisplay.text = player2Name + ": " + ++player2Score;

                        gameOver = true;
                        break;
                    }

                    RaycastHit[] hitsDiaRight = Physics.RaycastAll(
                        new Vector3(x, y * -1, 0),
                        new Vector3(1, 1),
                        length,
                        layermask);

                    if (hitsDiaRight.Length == numPiecesToWin - 1)
                    {
                        gameOver = true;
                        break;
                    }
                }

                yield return null;
            }

            yield return null;
        }

        // if Game Over update the winning text to show who has won
        if (gameOver == true)
        {
            winningText.GetComponent<TextMesh>().text = isPlayer1Turn ? player1WonText : player2WonText;
        }
        else
        {
            // check if there are any empty cells left, if not set game over and update text to show a draw
            if (!FieldContainsEmptyCell())
            {
                gameOver = true;
                winningText.GetComponent<TextMesh>().text = drawText;
            }
        }

        isCheckingForWinner = false;

        yield return 0;
    }

    /// <summary>
    /// check if the field contains an empty cell
    /// </summary>
    /// <returns><c>true</c>, if it contains empty cell, <c>false</c> otherwise.</returns>
    bool FieldContainsEmptyCell()
    {
        for (int x = 0; x < numColumns; x++)
        {
            for (int y = 0; y < numRows; y++)
            {
                if (field[x, y] == (int)Piece.Empty)
                    return true;
            }
        }
        return false;
    }

}