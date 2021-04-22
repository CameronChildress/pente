using System.Collections;
using System.Collections.Generic;
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


    //Variables
    public eState GameState { get; set; } = eState.Title;
    public InputField InputX;
    public InputField InputY;
    


    public static Game Instance { get { return instance; } }
    private static Game instance;

    private void Awake()
    {
        instance = this;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Vector2Int position = GetPosition();
            if (position != -Vector2Int.one)
            {
                PiecePlaced(position);
            }
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

        string xString = InputX.ToString();
        string yString = InputY.ToString();

        if (int.TryParse(xString, out int x) && int.TryParse(yString, out int y))
        {
            position = new Vector2Int(x, y);
        }
        
        return position;
    }

    public bool PiecePlaced(Vector2Int position)
    {
        return Board.Instance.PlacePiece(position, true, out bool isCapture, out bool isWin);
    }
}
