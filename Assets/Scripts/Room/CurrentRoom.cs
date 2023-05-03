using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentRoom : MonoBehaviour
{
    public List<Wall> walls;
    public Room room;
    void Start()
    {
        Close();
    }

    public void Open()
    {
        for (int i = 0; i < Data.nextRoomNum; i++)
        {
            if(room.nextRoomAvailable[i])
            {
                walls[i].Open();
            }
            else
            {
                walls[i].Close();
            }
        }
    }
    public void Close()
    {
        for (int i = 0; i < Data.nextRoomNum; i++)
        {
            walls[i].Close();
        }
    }
}