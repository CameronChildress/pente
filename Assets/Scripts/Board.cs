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

    private void Start()
    {
        for (int i = 0; i < 19; i++)
        {
            for (int j = 0; j < 19; j++)
            {
                spaces[i, j] = new BoardSpace() { state = BoardSpace.eSpaceState.Empty };
            }
        }
    }



    public bool PlacePiece(Vector2Int position, bool isPlayer1, out int captures, out bool isWin)
    {

        isWin = false;
        captures = 0;
        BoardSpace.eSpaceState currentPlayer = isPlayer1 ? BoardSpace.eSpaceState.Player1 : BoardSpace.eSpaceState.Player2;

        //Player 1 places a piece
        if (isPlayer1 && spaces[position.x, position.y].state == BoardSpace.eSpaceState.Empty)
        {

            //set that position to player 1
            spaces[position.x, position.y].state = currentPlayer;

            //make a new piece object to appear on the board
          

            //Check if there is a win condition at that place
            isWin = FindPenteWin(spaces, position.x, position.y, currentPlayer, 5);

            spaces[position.x, position.y].state = BoardSpace.eSpaceState.Player1;
            Instantiate(p1Piece, new Vector3(19.6f * position.x, 19.6f * position.y, 0) + new Vector3(48, 48, 0), Quaternion.identity, this.transform);
            //Debug.Log("My nuts player 1");

            //Check for a capture
            captures = FindCapture(position, isPlayer1);
        }
        else if(!isPlayer1 && spaces[position.x, position.y].state == BoardSpace.eSpaceState.Empty) // Same for player 2
        {

            //set that position to player 2
            spaces[position.x, position.y].state = currentPlayer;

            
            Instantiate(p2Piece, new Vector3(19.6f * position.x, 19.6f * position.y, 0) + new Vector3(48, 48, 0), Quaternion.identity, this.transform);
            //Debug.Log("My nuts player 2");


            //make a new piece object to appear on the board
            spaces[position.x, position.y].pieceObject = Instantiate(p2Piece, new Vector3(30 * position.x, 30 * position.y, 0) + new Vector3(74, 74, 0), Quaternion.identity, this.transform);

            //check for a match of 5 win
            isWin = FindPenteWin(spaces, position.x, position.y, currentPlayer, 5);

            //check for a capture
            captures = FindCapture(position, isPlayer1);
        }
        else if(spaces[position.x, position.y].state == BoardSpace.eSpaceState.Player1 || spaces[position.x, position.y].state == BoardSpace.eSpaceState.Player2)
        {
            captures = 0;
            isWin = false;
            return false;
        }

        return true;
    }

    bool FindPenteWin(BoardSpace[,] grid, int startRow, int startCol, BoardSpace.eSpaceState value, int matchAmount)
    {
        // <-> Horizontal <->
        if (FindLinearMatch(grid, startRow, startCol, value, 0, 1, matchAmount)) return true;
        //  | Vertical | 
        if (FindLinearMatch(grid, startRow, startCol, value, 1, 0, matchAmount)) return true;
        //  / Diagnol-up /
        if (FindLinearMatch(grid, startRow, startCol, value, -1, 1, matchAmount)) return true;
        //  \ Diagnol-down \
        if (FindLinearMatch(grid, startRow, startCol, value, 1, 1, matchAmount)) return true;

        return false;
    }

    bool FindLinearMatch(BoardSpace[,] grid, int startRow, int startCol, BoardSpace.eSpaceState value, int rowShift, int colShift, int matchAmount, bool reverse = true)
    {
        //variable to count matching values
        int matchCount = 0;

        int row = startRow;
        int col = startCol;

        //for loop that will run twice, flipping the sign on the second loop
        for (int flip = 1; flip >= -1; flip -= 2)
        {
            for (int i = 0; i <= matchAmount; i++)
            {
                if (flip == -1)
                {
                    //Set i to 1 if its been flipped and not checking the starter piece
                    i = 1;
                }

                //Determine if row or col are out of bounds, break if so
                if (row >= grid.GetLength(0) || row < 0) break;
                if (col >= grid.GetLength(1) || col < 0) break;

                //If piece of grid equals the value passed in, increment count
                if (grid[row, col].state == value)
                {
                    matchCount++;
                }
                else if (i != 0)
                {
                    break;
                }

                // flip: 1 on first loop, -1 on second loop
                row += rowShift * flip;
                col += colShift * flip;
            }

            //IF reverse is false, break out of loop
            if (!reverse) break;

            //Restart row and col AND shift so it doesn't count starter piece twice
            row = startRow + (rowShift * flip * -1);
            col = startCol + (colShift * flip * -1);
        }

        return (matchCount >= matchAmount);
    }

    int FindCapture(Vector2Int position, bool isPlayer1)
    {
        int captures = 0;
        //set the states to correct ones
        BoardSpace.eSpaceState current = isPlayer1 ? BoardSpace.eSpaceState.Player1 : BoardSpace.eSpaceState.Player2;
        BoardSpace.eSpaceState other   = isPlayer1 ? BoardSpace.eSpaceState.Player2 : BoardSpace.eSpaceState.Player1;

        //Horiz
        if (FindCaptureDirection(position, current, other, 1, 0)) captures++;
        if (FindCaptureDirection(position, current, other, -1, 0)) captures++;
        //Vert
        if (FindCaptureDirection(position, current, other, 0, 1)) captures++;
        if (FindCaptureDirection(position, current, other, 0, -1)) captures++;
        //Diagonal
        if (FindCaptureDirection(position, current, other, 1, 1)) captures++;
        if (FindCaptureDirection(position, current, other, 1, -1)) captures++;
        if (FindCaptureDirection(position, current, other, -1, 1)) captures++;
        if (FindCaptureDirection(position, current, other, -1, -1)) captures++;




        return captures;
    }


    bool FindCaptureDirection(Vector2Int position, BoardSpace.eSpaceState current, BoardSpace.eSpaceState other, int xShift, int yShift)
    {
        //Horizontal facing right
        //potentialCapture = FindLinearMatch(spaces, position.x + 1, position.y, other, 0, 1, 2, false);
        bool potentialCapture = (spaces[position.x + xShift, position.y + yShift].state == other && spaces[position.x + (xShift * 2), position.y + (yShift * 2)].state == other);
        if (potentialCapture)
        {
            //if the end is the same as the piece placed...
            if (spaces[position.x  + (xShift * 3), position.y + (yShift * 3)].state == current)
            {
                //remove the in between pieces
                spaces[position.x + xShift, position.y + yShift].state = BoardSpace.eSpaceState.Empty;
                spaces[position.x + (xShift * 2), position.y + (yShift * 2)].state = BoardSpace.eSpaceState.Empty;

                Destroy(spaces[position.x + xShift, position.y + yShift].pieceObject);
                Destroy(spaces[position.x + (2 * xShift), position.y + (2 * yShift)].pieceObject);

                return true;
            }
        }
        return false;
    }



}
