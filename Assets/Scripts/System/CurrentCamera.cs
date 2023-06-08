using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentCamera : MonoBehaviour
{
    public static CurrentCamera Instance;
    // Start is called before the first frame update
    public Camera cam;
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
