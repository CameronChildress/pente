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

    public bool CheckForScore(Vector2 position)
    {
        //check all directions around for a scoring condition
        //probably just like connect four
        return false;
    }

    public bool CheckForCapture(Vector2 position)
    {
        //if this piece, and another of the same player surrounds 2 pieces, capture and remove the pieces
        return false;
    }


}
