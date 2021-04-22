using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSpace : MonoBehaviour
{
    public enum eSpaceState
    {
        Empty, Player1, Player2
    }

    public eSpaceState state = eSpaceState.Empty;
    

}
