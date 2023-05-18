using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public float nextWayPointDistance = 3f;
    public Vector2 direction { get; set; }

    Path path;
    int currentWayPoint;
    bool reached;

    public Seeker seeker;
    public Rigidbody2D rigid;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdatePath", 0f, 0.1f);
    }

    void UpdatePath()
    {
        if(seeker.IsDone())seeker.StartPath(rigid.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(path == null)
        {

        }
        else
        {
            if(currentWayPoint >= path.vectorPath.Count)
            {
                reached = true;
            }
            else
            {
                reached = false;
            }
        }

        if(reached)
        {
            //idfk
        }
        else
        {
            direction = ((Vector2)path.vectorPath[currentWayPoint] - rigid.position).normalized;
            float distance = Vector2.Distance(rigid.position, path.vectorPath[currentWayPoint]);

            if (distance < nextWayPointDistance)
            {
                currentWayPoint++;
            }
        }
    }
}
