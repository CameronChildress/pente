using System.Collections;
using System.Collections.Generic;
using System.Timers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public enum eState
    {
        Title,
        StartGame,
        Player1Turn,
        Player2Turn,
        EndGame
    }



    //Variables
    public eState GameState { get; set; } = eState.Title;

    public static Game Instance { get { return instance; } }
    private static Game instance;

    public TMP_InputField InputX;
    public TMP_InputField InputY;

    public TMP_Text messageBox;
    public TMP_Text timerBox;

    public StringData player1name;
    public StringData player2name;

    public GameObject btnRematch;

    float turnTime = 30;
    bool isPlayer1 = true;
    

    private void Awake()
    {
        instance = this;
    }

    public void Update()
    {

       bool turnEnd = GameTimer();

        switch (GameState)
        {
            case eState.Title:
                GameState = eState.StartGame;
                break;
            case eState.StartGame:
                GameState = eState.Player1Turn;
                break;
            case eState.Player1Turn:
                //Debug.Log("Start Player 1 Turn");
                if (turnEnd)
                {
                    isPlayer1 = !isPlayer1;
                    GameState = eState.Player2Turn;
                }
                else
                {
                    if (Input.GetMouseButtonDown(0))
                    {

                        Vector2Int position = PiecePlacer.GetMousePosition();
                        if (position != -Vector2Int.one)
                        {
                            if (PiecePlaced(position, out int captures, out bool isWin))
                            {
                                if (isWin)
                                {
                                    GameState = eState.EndGame;
                                }
                                else
                                {
                                    GameState = eState.Player2Turn;
                                }
                                turnTime = 30;
                                isPlayer1 = !isPlayer1;
                            }
                        }
                        else Debug.Log("Position was negative: " + position.ToString());
                    }
                }
                break;
            case eState.Player2Turn:
                //Debug.Log("Start Player 2 Turn");
                if (turnEnd)
                {
                    isPlayer1 = !isPlayer1;
                    GameState = eState.Player1Turn;
                }
                else
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        Vector2Int position = PiecePlacer.GetMousePosition();
                        if (position != -Vector2Int.one)
                        {
                            if (PiecePlaced(position, out int captures, out bool isWin))
                            {
                                if (isWin)
                                {
                                    GameState = eState.EndGame;
                                }
                                else
                                {
                                    GameState = eState.Player1Turn;
                                }
                                turnTime = 30;
                                isPlayer1 = !isPlayer1;
                            }
                        }
                        else Debug.Log("Position was negative: " + position.ToString());
                    }
                }
                break;
            case eState.EndGame:
                //GameState = eState.Title;
                btnRematch.SetActive(true); ;
                break;
            default:
                break;
        }
    }

    public bool PiecePlaced(Vector2Int position, out int numOfCaptures, out bool isAWin)
    {
        bool success = Board.Instance.PlacePiece(position, isPlayer1, out int captures, out bool isWin);
        numOfCaptures = captures;
        isAWin = isWin;
        string playername = isPlayer1 ? player1name.value : player2name.value;
        //Debug.Log($"Captures for {playername} in this turn? : " + captures);
        if (isWin)
        {
            messageBox.text = $"Win for {playername}!";
            GameState = eState.EndGame;
        }
        return success;
    }

    //public Vector2Int GetPosition()
    //{
    //    Vector2Int position = -Vector2Int.one;

    //    string xString = InputX.text;
    //    string yString = InputY.text;

    //    if (int.TryParse(xString, out int x) && int.TryParse(yString, out int y))
    //    {
    //        position = new Vector2Int(x, y);
    //    }

    //    return position;
    //}

    public bool GameTimer()
    {
        turnTime -= Time.deltaTime;

        timerBox.text = "" + (int) turnTime;

        if(turnTime >= 4.995 && turnTime <= 5.005)
        {
            messageBox.text = "5 Seconds left";
        }

        if( turnTime >= -1.995 && turnTime <= 0.005)
        {
            messageBox.text = "New Turn";
            turnTime = 30;
            return true;
        }

        return false;


    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }

}
