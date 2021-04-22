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


}
