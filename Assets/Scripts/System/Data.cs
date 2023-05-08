using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class Data
{
    public static readonly int nextRoomNum = 6;
    public static readonly float roomPositionDx = 7.79422f;
    public static readonly float roomPositionDy = 9f;

    public static Vector3 RoomPositionByCoord(Vector2Int coord)
    {
        float x = (-coord.x * 2 + coord.y * 2) * roomPositionDx;
        float y = (coord.x + coord.y) * roomPositionDx;
        return new Vector3(x, y);
    }
}