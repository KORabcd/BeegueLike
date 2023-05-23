using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : Entity
{
    [System.Serializable]
    public struct PlayerMovement
    {
        public Vector2 inputMovement;
        public float drag;
        public float walkAcceleration;
        public float flyAcceleration;
        public AnimationCurve accelerationCurve;
    }

    [System.Serializable]
    public struct PlayerStatus
    {
        public float invincTime;
        public float invincDelay;
    }

    [SerializeField]
    private PlayerMovement playerMovement;

    [SerializeField]
    private PlayerStatus playerStatus;
    public Collider2D col { get; set; }
    public Rigidbody2D rigid { get; set; }
    private void Awake()
    {
        col = GetComponent<Collider2D>();
        rigid = GetComponent<Rigidbody2D>();
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
        if (entityStatus.isFlying) acceleration = playerMovement.flyAcceleration;
        else acceleration = playerMovement.walkAcceleration;

        float speedMax = currentSpeed();

        if (playerMovement.inputMovement.magnitude != 0) // moving
        {
            rigid.drag = 0;
            Vector2 force = playerMovement.inputMovement * acceleration;
            float forceMultiplier = playerMovement.accelerationCurve.Evaluate(
                Mathf.Clamp(
                    ((rigid.velocity/speedMax)- playerMovement.inputMovement).magnitude,
                    0,
                    2
                    )
                );
            rigid.AddForce(force * forceMultiplier);
        }
        else // no keys pressed
        {
            rigid.drag = playerMovement.drag;
        }


        if(rigid.velocity.magnitude>speedMax)
        {
            rigid.velocity *= speedMax / rigid.velocity.magnitude;
        }
    }
    private void InputMovement(InputAction.CallbackContext context)
    {
        playerMovement.inputMovement = context.ReadValue<Vector2>();
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
