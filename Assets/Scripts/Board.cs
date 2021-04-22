using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Board Instance { get { return instance; } }
    private Board instance;

    private Space[,] spaces = new Space[19, 19];

    private void Awake()
    {
        instance = this;
    }

    private bool CheckForWin(Vector2Int position)
    {
        //check all directions around for a scoring condition
        //probably just like connect four
        return false;
    }

    private bool CheckForCapture(Vector2Int position)
    {
        //if this piece, and another of the same player surrounds 2 pieces, capture and remove the pieces
        return false;
    }

    public bool PlacePiece(Vector2Int position, bool isPlayer1, out bool isCapture, out bool isWin)
    {
        if (spaces[position.x, position.y].state != Space.eSpaceState.Empty)
        {
            
            isCapture = false;
            isWin = false;
            return false;
        }
        else
        {
            if(isPlayer1)
            {
                spaces[position.x, position.y].state = Space.eSpaceState.Player1;

            }
            else
            {
                spaces[position.x, position.y].state = Space.eSpaceState.Player2;

            }
        }

        isCapture = CheckForCapture(position);
        isWin = CheckForWin(position);





        return true;
    }


}
