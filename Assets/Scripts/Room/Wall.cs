using System.Collections;
using UnityEngine;
public class Wall : MonoBehaviour
{
    public int wallNumber;
    public Vector2Int nextCoord;
    public WallSprite spritePrefab;
    public WallSprite sprite { get; set; }
    public GameObject door;
    public GameObject nextTrigger;

    public Vector3 playerInitPos;
    void Awake()
    {
        sprite = Instantiate(spritePrefab, transform);
    }
    public void Open()
    {
        sprite.animator.SetBool("Open", true);
        door.SetActive(false);
        nextTrigger.SetActive(true);
    }

    public void Close()
    {
        sprite.animator.SetBool("Open", false);
        door.SetActive(true);
        nextTrigger.SetActive(false);
    }
}