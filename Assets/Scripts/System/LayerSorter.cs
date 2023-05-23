using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSorter : MonoBehaviour
{
    [SerializeField]
    private float offset;

    private Renderer render;
    // Start is called before the first frame update
    void Awake()
    {
        render = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.y + offset);
    }
}
