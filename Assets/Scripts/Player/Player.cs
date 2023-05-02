using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : Entity
{
    [System.Serializable]
    public struct PlayerStatus
    {
        public float invincTime;
        public float invincDelay;
    }

    [SerializeField]
    private PlayerStatus playerStatus;

    private void Start()
    {
        base.entityStatus.maxHealth = 10;
        base.entityStatus.nowHealth = 8;
        base.entityStatus.walkSpeed = 5;
        base.entityStatus.canFly = true;
    }

    void Update()
    {

        Move();
    }
    void Move()
    {
        float h, v;
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        transform.position += new Vector3(h, v, 0);
    }
}
