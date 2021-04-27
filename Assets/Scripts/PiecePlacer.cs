using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class PiecePlacer
{
    //Gets the grid postion of a mouse click
    public static Vector2Int GetMousePosition()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(pos);

        pos.x = (pos.x - 38.15f) / 19.6f;
        pos.y = (pos.y - 38.15f) / 19.6f;

        Vector2Int intPos = new Vector2Int((int)pos.x, (int)pos.y);
        return intPos;
    }

}


