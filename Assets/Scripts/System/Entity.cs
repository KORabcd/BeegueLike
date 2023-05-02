using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [System.Serializable]
    public struct EntityStatus
    {
        public int maxHealth;
        public int nowHealth;

        public float walkSpeed;
        public float flySpeed;

        public bool canFly;
        public bool isFly;

        public bool isDead;
    }

    public EntityStatus entityStatus;

}
