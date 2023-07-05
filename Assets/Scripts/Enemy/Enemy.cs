using System.Collections;
using UnityEngine;
public class Enemy : Entity
{
    [System.Serializable]
    public struct DropItem
    {
        public Item item;
        public float percentage;
    }

    [SerializeField]
    public DropItem[] dropItems;
    public void ItemDrop()
    {
        Transform roomTransform = RoomManager.Instance.rooms[RoomManager.Instance.currentRoom.roomCoord.x, RoomManager.Instance.currentRoom.roomCoord.y].transform;

        float rand = Random.Range(1.0f,100.0f);
        float num = 0;
        for (int i = 0; i < dropItems.Length; i++)
        {
            if (rand < num + dropItems[i].percentage)
            {
                Instantiate(dropItems[i].item.gameObject, gameObject.transform.position, Quaternion.identity, roomTransform);
                break;
            }
            else
                num += dropItems[i].percentage;
        }
    }
}