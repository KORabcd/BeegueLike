using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [System.Serializable]
    public struct Status
    {
        public float aimDirection;

        public Collider2D attackPoint;
        public ContactFilter2D contactFilter;
        public int damage;
        public float attackDelay;
        public bool isAttacking;
    }
    public Status status;

    protected float attackTimer = 0;
    void Start()
    {
        status.isAttacking = false;
    }
    private void Update()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer < 0) attackTimer = 0;

        status.attackPoint.transform.rotation =
            Quaternion.Euler(
                new Vector3(0, 0, status.aimDirection));
    }
    public bool Attack()
    {
        bool attackAvailable = attackTimer == 0;
        if (attackAvailable && !status.isAttacking) StartCoroutine("AttackIE");

        return attackAvailable;
    }
    protected IEnumerator AttackIE()
    {
        attackTimer = status.attackDelay;

        status.isAttacking = true;
        yield return null;
        status.isAttacking = false;
    }
}
