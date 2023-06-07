using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class WorkerAntAI : MonoBehaviour
{
    public float nextWayPointDistance = 3f;
    public Vector2 direction { get; set; }

    Path path;
    int currentWayPoint;
    bool reached;

    public WorkerAnt workerAnt;
    public Seeker seeker;
    public Rigidbody2D rigid;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdatePath", 0f, 0.1f);
    }

    void UpdatePath()
    {
        if(workerAnt.target == null)
        {
            return;
        }

        if(seeker.IsDone())seeker.StartPath(rigid.position, workerAnt.target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWayPoint = 1;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if(path == null || workerAnt.target == null)
        {
            return;
        }

        if(currentWayPoint >= path.vectorPath.Count-1)
        {
            reached = true;
        }
        else
        {
            reached = false;
        }

        if(reached)
        {
            //idfk
        }
        else
        {
            float distance = Vector2.Distance(rigid.position, path.vectorPath[currentWayPoint]);

            if (distance < nextWayPointDistance)
            {
                currentWayPoint++;
            }

            direction = ((Vector2)path.vectorPath[currentWayPoint] - rigid.position).normalized;
        }
    }
}
