using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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

    public static Game Instance { get { return instance; } }
    private static Game instance;
    

    //Variables
    public eState GameState { get; set; } = eState.Title;
    public TMP_InputField InputX;
    public TMP_InputField InputY;

    bool isPlayer1 = true;

    private void Awake()
    {
        instance = this;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector2Int position = GetPosition();
            if (position != -Vector2Int.one)
            {
                PiecePlaced(position);
                isPlayer1 = !isPlayer1;
            }
            else Debug.Log("Position was negative: " + position.ToString());
        }
        //switch (GameState)
        //{
        //    case eState.Title:
        //        GameState = eState.StartGame;
        //        break;
        //    case eState.StartGame:
        //        GameState = eState.Player1Turn;
        //        break;
        //    case eState.Player1Turn:
        //        if (PiecePlaced(TakeTurn()))
        //        {
        //            GameState = eState.Player2Turn;
        //        }
        //        else
        //        {

        //        }
        //        break;
        //    case eState.Player2Turn:
        //        if(PiecePlaced(TakeTurn()))
        //        {
        //            GameState = eState.Player1Turn;
        //        }
        //        else 
        //        {
                    
        //        }
        //        GameState = eState.Player1Turn;
        //        break;
        //    case eState.EndGame:
        //        GameState = eState.Title;
        //        break;
        //    default:
        //        break;
        //}

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

    public bool PiecePlaced(Vector2Int position)
    {
        return Board.Instance.PlacePiece(position, isPlayer1, out bool isCapture, out bool isWin);
    }
}
