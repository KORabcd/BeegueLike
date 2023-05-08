using System.Collections;
using UnityEngine;
public class Wall : MonoBehaviour
{
    public int wallNumber;
    public Vector2Int nextCoord;
    public GameObject door;
    public GameObject nextTrigger;

    public Vector3 playerInitPos;
    void Start()
    {

    }
    public void Open()
    {
        door.SetActive(false);
        nextTrigger.SetActive(true);
    }

    public void Close()
    {
        door.SetActive(true);
        nextTrigger.SetActive(false);
    }
}