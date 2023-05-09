using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<Wall> walls;
    public bool[] nextRoomAvailable;

    public Animator animator;
    void Awake()
    {
        Open();
    }

    public void Open()
    {
        for (int i = 0; i < walls.Count; i++)
        {
            if (nextRoomAvailable[i])
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
        for (int i = 0; i < walls.Count; i++)
        {
            walls[i].Close();
        }
    }
    public void EnablePhysics()
    {
        gameObject.layer = 6;
        foreach (Wall wall in walls)
        {
            wall.gameObject.layer = 8;
        }
    }

    public void DisablePhysics()
    {
        gameObject.layer = 7;
        foreach (Wall wall in walls)
        {
            wall.gameObject.layer = 6;
        }
    }
}