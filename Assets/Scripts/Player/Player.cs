using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : Entity
{
    [System.Serializable]
    public struct PlayerStatus
    {
        public AnimationCurve motionCurve;
        public Vector2 inputMovement;
        public float movementDrag;
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
    }
    private void UpdateMovement()
    {
        float acceleration;
        if (base.entityStatus.isFly) acceleration = base.entityStatus.flyAcceleration;
        else acceleration = base.entityStatus.walkAcceleration;

        float speedMax;
        if (base.entityStatus.isFly) speedMax = base.entityStatus.flySpeedMax;
        else speedMax = base.entityStatus.walkSpeedMax;

        if (playerStatus.inputMovement.magnitude != 0) // moving
        {
            rigid.drag = 0;
            Vector2 force = playerStatus.inputMovement * acceleration;
            float forceMultiplier = playerStatus.motionCurve.Evaluate(
                Mathf.Clamp(
                    ((rigid.velocity/speedMax)-playerStatus.inputMovement).magnitude,
                    0,
                    2
                    )
                );
            rigid.AddForce(force * forceMultiplier);
        }
        else // no keys pressed
        {
            rigid.drag = playerStatus.movementDrag;
        }


        if(rigid.velocity.magnitude>speedMax)
        {
            rigid.velocity *= speedMax / rigid.velocity.magnitude;
        }

        base.entityStatus.currentMovement = rigid.velocity;
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

    public void DisablePhysics()
    {
        gameObject.layer = 6;
    }

    public void EnablePhysics()
    {
        gameObject.layer = 7;
    }

    public void ResetMovement()
    {
        rigid.velocity = Vector2.zero;
    }

}
