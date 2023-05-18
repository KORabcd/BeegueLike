using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{

    [System.Serializable]
    public struct EnemyStatus
    {
        public AnimationCurve motionCurve;
        public float movementDrag;

        public int damage;

        public string type;
    }

    [SerializeField]
    private EnemyStatus enemyStatus;

    public Rigidbody2D rigid;
    public EnemyAI enemyAI;

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

        if (enemyAI.direction.magnitude != 0) // moving
        {
            rigid.drag = 0;
            Vector2 force = enemyAI.direction * acceleration;
            float forceMultiplier = enemyStatus.motionCurve.Evaluate(
                Mathf.Clamp(
                    ((rigid.velocity / speedMax) - enemyAI.direction).magnitude,
                    0,
                    2
                    )
                );
            rigid.AddForce(force * forceMultiplier);
        }
        else // no keys pressed
        {
            rigid.drag = enemyStatus.movementDrag;
        }


        if (rigid.velocity.magnitude > speedMax)
        {
            rigid.velocity *= speedMax / rigid.velocity.magnitude;
        }

        base.entityStatus.currentMovement = rigid.velocity;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
