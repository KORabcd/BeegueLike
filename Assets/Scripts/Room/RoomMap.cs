using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMap : MonoBehaviour
{
    public static RoomMap Instance;
    public List<Room> rooms;

    void Awake()
    {
        Instance = this;
    }

}