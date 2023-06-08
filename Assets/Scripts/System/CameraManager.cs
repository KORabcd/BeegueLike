using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;
    public Camera cam;

    Vector3 offset = new Vector3(0f, 1f, -10f);
    float smoothTime = 0.25f;
    Vector3 velocity = Vector3.zero;

    [SerializeField] Transform target;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
