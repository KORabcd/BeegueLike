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
    private void LateUpdate()
    {
        PlayerFlip();
    }
    public void UpdateMovement()
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
    public void InputMovement(InputAction.CallbackContext context)
    {
        playerStatus.inputMovement = context.ReadValue<Vector2>();
    }
    void PlayerFlip()
    {
        if(playerStatus.inputMovement.x !=0)
        {
            sprite.flipX = playerStatus.inputMovement.x > 0;
        }
    }
}
