using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static Board Instance { get { return instance; } }
    private static Board instance;
    public GameObject p1Piece;
    public GameObject p2Piece;

    private BoardSpace[,] spaces = new BoardSpace[19, 19];

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
        //Debug.Log("eat my balls pente");
        if (spaces[position.x, position.y] == null)
        {
            spaces[position.x, position.y] = new BoardSpace();
        }
        if (isPlayer1)
        {
            spaces[position.x, position.y].state = BoardSpace.eSpaceState.Player1;
            Instantiate(p1Piece, new Vector3(19.6f * position.x, 19.6f * position.y, 0) + new Vector3(48, 48, 0), Quaternion.identity, this.transform);
            //Debug.Log("My nuts player 1");

        }
        else
        {
            spaces[position.x, position.y].state = BoardSpace.eSpaceState.Player2;
            Instantiate(p2Piece, new Vector3(19.6f * position.x, 19.6f * position.y, 0) + new Vector3(48, 48, 0), Quaternion.identity, this.transform);
            //Debug.Log("My nuts player 2");

        }
        if (spaces[position.x, position.y].state != BoardSpace.eSpaceState.Empty)
        {
            
            isCapture = false;
            isWin = false;
            return false;
        }
        else
        {

        }

        isCapture = CheckForCapture(position);
        isWin = CheckForWin(position);

        return true;
    }


}
