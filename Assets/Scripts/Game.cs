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

    public static Game Instance { get { return instance; } }
    private static Game instance;

    public TMP_InputField InputX;
    public TMP_InputField InputY;

    public StringData player1Data;
    public StringData player2Data;


    Player player1 = new Player();
    Player player2 = new Player();

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
                                GameState = eState.Player2Turn;
                                turnTime = 30;
                                isPlayer1 = !isPlayer1;
                                player1.captures += 1;
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
                                GameState = eState.Player1Turn;
                                turnTime = 30;
                                isPlayer1 = !isPlayer1;
                                player2.captures += 1;
                            }
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

    public bool PiecePlaced(Vector2Int position, out int numOfCaptures, out bool isAWin)
    {
        bool success = Board.Instance.PlacePiece(position, isPlayer1, out int captures, out bool isWin);
        numOfCaptures = captures;
        isAWin = isWin;
        string playername = isPlayer1 ? "player 1" : "player 2";
        Debug.Log($"Captures for {playername} in this turn? : " + captures);
        Debug.Log($"Win for {playername}? : " + isWin);
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
