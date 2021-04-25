using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PiecePlacer : MonoBehaviour
{
    Vector2Int gridPos;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(pos);

            pos.x = (pos.x - 38.15f) / 19.6f;
            pos.y = (pos.y - 38.15f) / 19.6f;


            Game.Instance.PlacePiece(new Vector2Int((int) pos.x, (int) pos.y));
        }
    }
}
