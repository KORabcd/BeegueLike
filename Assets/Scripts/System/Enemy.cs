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

}