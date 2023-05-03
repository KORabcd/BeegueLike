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

    Rigidbody2D rigid;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {

    }

    void Update()
    {
        UpdateMovement();
        transform.position += Time.deltaTime * (Vector3)base.entityStatus.currentMovement;
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
}
