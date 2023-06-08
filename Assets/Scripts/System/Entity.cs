using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [System.Serializable]
    public struct EntityStatus
    {
        public int maxHealth;
        public int currentHealth;

        public float walkSpeed;

        public float flySpeed;

        public bool canFly;
        public bool isFlying;

        public bool isDead;
    }

    [SerializeField]
    public EntityStatus entityStatus;

    public float currentSpeed()
    {
        if (!entityStatus.isFlying) return entityStatus.walkSpeed;
        else return entityStatus.flySpeed;
    }

    public void TakeDamage(int damage)
    {
        entityStatus.currentHealth -= damage;
        if (entityStatus.currentHealth < 0)entityStatus.currentHealth = 0;

        if(entityStatus.currentHealth == 0)
        {
            entityStatus.isDead = true;
            Dead();
        }
    }

    public void Dead()
    {
        StartCoroutine("DeadIE");
    }

    public IEnumerator DeadIE()
    {
        Destroy(gameObject);
        yield return null;
    }
}
