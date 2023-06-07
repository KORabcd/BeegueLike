using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public int height, width;
    public int inactiveRoomCnt;
    public int specialRoomCnt;
    public Room[,] map;
    public int[,] mapType;
    [System.Serializable]
    public struct RoomPalette
    {
        public Room emptyRoom;
        public Room inactiveRoom;
    }
    [SerializeField]
    public RoomPalette roomPalette;
    public void GenerateMap()
    {
        map = new Room[10, 10];
        do
        {
            mapType = MapGenerator.GenerateMapTypes(height, width, inactiveRoomCnt, specialRoomCnt);
            Debug.Log("asdf");
        } while (MapGenerator.BFS(mapType, height, width) == false);
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Debug.Log("(" + i + "," + j + ")" + ":" + mapType[i, j]);
                map[i, j] = roomPalette.emptyRoom;
            }
        }
    }

    public void MaterializeMap()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                RoomManager.Instance.rooms[i, j] = Instantiate(map[i, j], transform);
                RoomManager.Instance.rooms[i, j].gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                foreach (Wall wall in RoomManager.Instance.rooms[i, j].walls)
                {
                    int x = i + wall.nextCoord.x;
                    int y = j + wall.nextCoord.y;
                    if (x < 0 || y < 0)
                    {
                        RoomManager.Instance.rooms[i, j].nextRoomAvailable[wall.wallNumber] = false;
                    }
                    else
                    {
                        RoomManager.Instance.rooms[i, j].nextRoomAvailable[wall.wallNumber] = true;
                    }
                }

            }
        }
    }
}