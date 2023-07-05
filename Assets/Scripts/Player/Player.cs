using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
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
    public struct Status
    {
        public float invincTime;
        public float invincDelay;
    }

    [SerializeField]
    private PlayerMovement playerMovement;

    [SerializeField]
    private Status status;
    public Collider2D col { get; set; }
    public Rigidbody2D rigid { get; set; }
    public Animator animator { get; set; }

    public SpriteRenderer sprite;

    public Weapon weapon;
    private void Awake()
    {
        col = GetComponent<Collider2D>();
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
    public void InputMovement(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        playerMovement.inputMovement = value;
    }

    public void InputLook(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        Vector2 mousePos = CameraManager.Instance.cam.ScreenToWorldPoint(value);
        Vector2 mousePosRelative = mousePos - rigid.position;
        
        float direction = Vector2.SignedAngle(Vector2.left, mousePosRelative);
        weapon.status.aimDirection = direction;
    }
    public void InputAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("attack");
            weapon.Attack();
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


    public new IEnumerator DeadIE()
    {
        PlayerInput PI = GetComponent<PlayerInput>();
        PI.enabled = false;
        animator.SetBool("Dead", true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Sun");
    }
}
