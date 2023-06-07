using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Coord
{
    public int x;
    public int y;
}

public static class Data
{

    public static readonly float roomPositionDx = 10.3923f;
    public static readonly float roomPositionDy = 12;
    public static readonly Vector3 playerInitPos = new Vector3(0, -8);

    public static Vector3 RoomPositionByCoord(Vector2Int coord)
    {
        float x = (-coord.x * 2 + coord.y * 2) * roomPositionDx;
        float y = (coord.x + coord.y) * roomPositionDy;
        return new Vector3(x, y);
    }
}