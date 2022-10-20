using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float YSpeed;
    public float ZSpeed;
    public float XSpeed;
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(XSpeed, YSpeed, ZSpeed));    
    }
}
