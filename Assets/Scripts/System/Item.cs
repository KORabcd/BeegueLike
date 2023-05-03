using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [System.Serializable]
    public struct ItemStatus
    {
        public int id;
        public string name;
        public string description;
    }
    public ItemStatus itemStatus;
}
