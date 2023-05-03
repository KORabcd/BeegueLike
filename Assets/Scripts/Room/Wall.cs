using System.Collections;
using UnityEngine;
public class Wall : MonoBehaviour
{
    public GameObject door;
    public GameObject nextTrigger;

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