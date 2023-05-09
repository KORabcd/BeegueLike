using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public Room[,] map = new Room[10, 10];
    [System.Serializable]
    public struct RoomPalette
    {
        public Room emptyRoom;
    }

    [SerializeField]
    public RoomPalette roomPalette;

    public void GenerateMap()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                map[i, j] = roomPalette.emptyRoom;
            }
        }
    }
}
