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

    Rigidbody2D rigid;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        base.entityStatus.maxHealth = 10;
        base.entityStatus.nowHealth = 8;
        base.entityStatus.walkSpeed = 5;
        base.entityStatus.canFly = true;
    }
    private void Update()
    {

    }
    private void FixedUpdate()
    {
        PlayerMove();
    }
    void PlayerMove()
    {
        float h, v;
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        rigid.velocity = new Vector2(h, v).normalized * base.entityStatus.walkSpeed;
    }
}
