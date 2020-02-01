using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeSection : MonoBehaviour
{
    public Rigidbody prevNode;
    public float margin = 0.5f;
    public float maxDist = 1.2f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
       
    }
    
    private void FixedUpdate()
    {
        var vec = prevNode.transform.position - transform.position;
        
        if (vec.magnitude > maxDist)
        {
            rb.MovePosition(prevNode.transform.position - vec.normalized * maxDist);
        }
    }
}
