using System.Collections;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Vector2Int position { get; set; }
    public bool[] nextRoomAvailable { get; set; }
}