using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerAnt : Entity
{
    [System.Serializable]
    public struct Movement
    {
        public Vector2 dest;
        public float walkAcceleration;
        public float direction;
        public float drag;
    }

    [System.Serializable]
    public struct Status
    {
        public float detectRange;
    }

    [SerializeField]
    public Movement movement;

    [SerializeField]
    public Status status;
    public Transform target { get; set; }
    public Collider2D col { get; set; }
    public Rigidbody2D rigid { get; set; }
    public Animator animator { get; set; }

    public WorkerAntAttacker attacker;


    public WorkerAntAI AI;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void Start()
    {
        target = EnemyManager.Instance.target;
    }
    private void FixedUpdate()
    {
        UpdateMovement();
    }

    public void UpdateMovement()
    {
        float acceleration = movement.walkAcceleration;
        float speedMax = currentSpeed();

        if (movement.dest.magnitude != 0) // moving
        {
            rigid.drag = 0;
            Vector2 force = movement.dest * acceleration;
            rigid.AddForce(force);
            movement.direction = Vector2.SignedAngle(Vector2.left, movement.dest);
        }
        else // no keys pressed
        {
            rigid.drag = movement.drag;
        }


        if (rigid.velocity.magnitude > speedMax)
        {
            rigid.velocity *= speedMax / rigid.velocity.magnitude;
        }
    }
    public void Attack()
    {
        attacker.Attack();
    }
}
