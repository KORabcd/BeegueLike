using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentRoom : MonoBehaviour
{
    public Room room;
    [SerializeField]
    public Vector2Int roomCoord { get; set; }
}