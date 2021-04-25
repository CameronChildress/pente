using System.Collections;
using System.Collections.Generic;
using System.Timers;
using TMPro;
using UnityEngine;

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

    public Game Instance { get { return instance; } }
    private Game instance;

    public TMP_InputField InputX;
    public TMP_InputField InputY;



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

                Debug.Log("Start Player 1 Turn");

                if (turnEnd)
                {
                    isPlayer1 = !isPlayer1;
                    GameState = eState.Player2Turn;

                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        Vector2Int position = GetPosition();
                        if (position != -Vector2Int.one)
                        {
                            PiecePlaced(position);
                            GameState = eState.Player2Turn;

                            turnTime = 30;

                            isPlayer1 = !isPlayer1;
                        }
                        else Debug.Log("Position was negative: " + position.ToString());
                    }
                }
                break;
            case eState.Player2Turn:
                Debug.Log("Start Player 2 Turn");

                if (turnEnd)
                {
                    isPlayer1 = !isPlayer1;
                    GameState = eState.Player1Turn;
                }
                else
                {

                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        Vector2Int position = GetPosition();
                        if (position != -Vector2Int.one)
                        {
                            PiecePlaced(position);
                            GameState = eState.Player1Turn;

                            turnTime = 30;

                            isPlayer1 = !isPlayer1;
                        }
                        else Debug.Log("Position was negative: " + position.ToString());
                    }
                }

                break;
            case eState.EndGame:
                GameState = eState.Title;
                break;
            default:
                break;
        }

    }


    public bool PiecePlaced(Vector2Int position)
    {
        bool success = Board.Instance.PlacePiece(position, isPlayer1, out bool isCapture, out bool isWin);
        string playername = isPlayer1 ? "player 1" : "player 2";
        Debug.Log($"Capture for {playername}? : " + isCapture);
        Debug.Log($"Win for {playername}? : " + isWin);
        return success;
    }

    public Vector2Int GetPosition()
    {
        Vector2Int position = -Vector2Int.one;

        string xString = InputX.text;
        string yString = InputY.text;

        if (int.TryParse(xString, out int x) && int.TryParse(yString, out int y))
        {
            position = new Vector2Int(x, y);
        }

        return position;
    }

    public bool GameTimer()
    {
        turnTime -= Time.deltaTime;

        if(turnTime >= 4.995 && turnTime <= 5.005)
        {
            Debug.Log("5 Seconds left");
        }

        if( turnTime >= -1.995 && turnTime <= 0.005)
        {
            turnTime = 30;
            Debug.Log("Turn Over");
            return true;
        }

        return false;


    }

}
