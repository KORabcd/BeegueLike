using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Scanner : MonoBehaviour
{

    public AstarPath pathfinder;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Scan", 0f, 0.5f);
    }

    void Scan()
    {
        if (!pathfinder.isScanning)
        {
            pathfinder.Scan();
        }
    }
}
