using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class Data
{
    public static readonly int nextRoomNum = 6;
    public static readonly List<Vector2Int> nextRoomCord = new List<Vector2Int>
    {
        new Vector2Int(1,1),
        new Vector2Int(0,1),
        new Vector2Int(-1,0),
        new Vector2Int(-1,-1),
        new Vector2Int(0,-1),
        new Vector2Int(1,0),
    };
}