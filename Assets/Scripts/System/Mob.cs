using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [System.Serializable]
    public struct EnemyStatus
    {
        public int damage;
        public float attackSpeed;

        public string type;
    }

    private EnemyStatus enemyStatus;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
