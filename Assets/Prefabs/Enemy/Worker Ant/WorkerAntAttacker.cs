using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerAntAttacker : MonoBehaviour
{
    public WorkerAnt workerAnt;

    [System.Serializable]
    public struct Status
    {
        public Collider2D attackPoint;
        public ContactFilter2D contactFilter;
        public int damage;
        public bool isAttacking;
    }

    [SerializeField]
    public Status status;

    private Collider2D[] hit = new Collider2D[16];
    // Start is called before the first frame update
    void Start()
    {
        status.isAttacking = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        status.attackPoint.transform.rotation =
            Quaternion.Euler(
                new Vector3(0, 0, workerAnt.movement.direction));

        //check for attack if not attacking
        if (!status.isAttacking)
        {
            int targetHit = status.attackPoint.OverlapCollider(status.contactFilter, hit);

            if (targetHit > 0)
            {
                Attack();
            }
        }
    }

    public void Attack()
    {
        StartCoroutine("AttackIE");
    }
    private IEnumerator AttackIE()
    {
        status.isAttacking = true;
        workerAnt.animator.SetBool("IsAttacking", true);
        yield return new WaitForSeconds(0.2f);

        //check hits
        int targetHit = status.attackPoint.OverlapCollider(status.contactFilter, hit);
        //deal damage
        for (int i = 0; i < targetHit; i++)
        {
            Entity entityHit = hit[i].gameObject.GetComponent<Entity>();
            entityHit.TakeDamage(status.damage);
        }

        yield return new WaitForSeconds(0.4f);
        status.isAttacking = false;
        workerAnt.animator.SetBool("IsAttacking", false);
    }
}
