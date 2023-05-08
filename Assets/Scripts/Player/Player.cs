using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : Entity
{
    [System.Serializable]
    public struct PlayerStatus
    {
        public Vector2 inputMovement;
        public float invincTime;
        public float invincDelay;
    }

    [SerializeField]
    private PlayerStatus playerStatus;
    public Rigidbody2D rigid;
    public SpriteRenderer sprite;
    private void Awake()
    {
    }
    private void Start()
    {

    }

    void Update()
    {

    }
    private void FixedUpdate()
    {
        UpdateMovement();
        Vector2 movePosition = (Vector2)transform.position + Time.deltaTime * base.entityStatus.currentMovement;
        rigid.MovePosition(movePosition);
    }
    private void UpdateMovement()
    {
        float acceleration;
        if (base.entityStatus.isFly) acceleration = base.entityStatus.flyAcceleration;
        else acceleration = base.entityStatus.walkAcceleration;

        float speedMax;
        if (base.entityStatus.isFly) speedMax = base.entityStatus.flySpeedMax;
        else speedMax = base.entityStatus.walkSpeedMax;

        Vector2 inputMovement = playerStatus.inputMovement * speedMax;
        Vector2 deltaMovement = inputMovement - base.entityStatus.currentMovement;
        if (deltaMovement.magnitude > acceleration* Time.deltaTime) deltaMovement *= acceleration* Time.deltaTime / deltaMovement.magnitude;
        base.entityStatus.currentMovement += deltaMovement;
    }
    private void InputMovement(InputAction.CallbackContext context)
    {
        playerStatus.inputMovement = context.ReadValue<Vector2>();
    }
    void PlayerFlip()
    {
        //이미지 뒤집지 말고 스프라이트로 하자
        if(playerStatus.inputMovement.x !=0)
        {
            sprite.flipX = playerStatus.inputMovement.x > 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NextRoomTrigger"))
        {
            Wall passWall = collision.transform.parent.GetComponent<Wall>();
            RoomManager.Instance.MoveRoom(passWall);
        }
    }

    
}
