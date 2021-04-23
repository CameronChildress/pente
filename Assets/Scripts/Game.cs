using System.Collections;
using System.Collections.Generic;
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

    private void Awake()
    {
        instance = this;
    }

    public void Update()
    {
        switch (GameState)
        {
<<<<<<< Updated upstream
            case eState.Title:
                GameState = eState.StartGame;
                break;
            case eState.StartGame:
                GameState = eState.Player1Turn;
                break;
            case eState.Player1Turn:
                if (PiecePlaced())
                {
                    GameState = eState.Player2Turn;
                }
                else
                {
=======
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
>>>>>>> Stashed changes

                }
                break;
            case eState.Player2Turn:
                if(PiecePlaced())
                {
                    GameState = eState.Player1Turn;
                }
                else 
                {
                    
                }
                GameState = eState.Player1Turn;
                break;
            case eState.EndGame:
                GameState = eState.Title;
                break;
            default:
                break;
        }

    }

    public bool PiecePlaced()
    {
        bool validPlacement = false;
        if (validPlacement) return true;
        return false;

    }

<<<<<<< Updated upstream

=======
    public bool PiecePlaced(Vector2Int position)
    {
        bool success = Board.Instance.PlacePiece(position, isPlayer1, out bool isCapture, out bool isWin);
        Debug.Log("Win? : " + isWin);
        return success;
    }
>>>>>>> Stashed changes
}
