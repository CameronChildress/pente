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



    public bool PlacePiece(Vector2Int position, bool isPlayer1, out bool isCapture, out bool isWin)
    {

        isWin = false;
        isCapture = false;
        //if the current space hasnt been places in yet, make a new space
        if (spaces[position.x, position.y] == null)
        {
            spaces[position.x, position.y] = new BoardSpace();
        }

        //if the current space is occupied, return false (piece cannot be placed)
        if (spaces[position.x, position.y].state == BoardSpace.eSpaceState.Player1 || spaces[position.x, position.y].state == BoardSpace.eSpaceState.Player2)
        {
            isCapture = false;
            isWin = false;
            return false;
        }

        //Player 1 places a piece
        if (isPlayer1)
        {
            //set that position to player 1
            spaces[position.x, position.y].state = BoardSpace.eSpaceState.Player1;

            //make a new piece object to appear on the board
            Instantiate(p1Piece, new Vector3(30 * position.x, 30 * position.y, 0) + new Vector3(74, 74, 0), Quaternion.identity, this.transform);

            //Check if there is a win condition at that place
            isWin = FindPenteWin(spaces, position.y, position.x, BoardSpace.eSpaceState.Player1, 5);

            //Check for a capture
            //isCapture = FindCapture()
        }
        else // Same for player 2
        {
            spaces[position.x, position.y].state = BoardSpace.eSpaceState.Player2;
            Instantiate(p1Piece, new Vector3(30 * position.x, 30 * position.y, 0) + new Vector3(74, 74, 0), Quaternion.identity, this.transform);
            isWin = FindPenteWin(spaces, position.y, position.x, BoardSpace.eSpaceState.Player1, 5);
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
                if (grid[row, col] == null) spaces[row, col] = new BoardSpace();
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




}
