using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhinoBeetleSword : Weapon
{
    public Animator animator;

    private Collider2D[] hit = new Collider2D[16];
    protected new IEnumerator AttackIE()
    {
        attackTimer = status.attackDelay;

        status.isAttacking = true;
        animator.SetBool("IsAttacking", true);
        yield return new WaitForSeconds(0.16f);

        //check hits
        int targetHit = status.attackPoint.OverlapCollider(status.contactFilter, hit);
        //deal damage
        for (int i = 0; i < targetHit; i++)
        {
            Entity entityHit = hit[i].gameObject.GetComponent<Entity>();
            entityHit.TakeDamage(status.damage);
        }
        yield return new WaitForSeconds(0.5f);
        status.isAttacking = false;
        animator.SetBool("IsAttacking", false);
    }
}
