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

        public Vector2 currentMovement;

        public float walkSpeedMax;
        public float walkAcceleration;

        public float flySpeedMax;
        public float flyAcceleration;

        public bool canFly;
        public bool isFly;

        public bool isDead;
    }

    [SerializeField]
    public EntityStatus entityStatus;
}
