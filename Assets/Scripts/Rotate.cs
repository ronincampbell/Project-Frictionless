using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    // Declare necessary variables
    public float YSpeed;
    public float ZSpeed;
    public float XSpeed;
    
    // Rotate the attached gameObject at a constant rate of speed
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(XSpeed, YSpeed, ZSpeed));    
    }
}
