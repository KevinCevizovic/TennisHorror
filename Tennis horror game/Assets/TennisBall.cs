using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TennisBall : MonoBehaviour
{
    int heading;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        heading = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
