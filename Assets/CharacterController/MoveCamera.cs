using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code by Dave / Game Development on YouTube

public class MoveCamera : MonoBehaviour
{

    public Transform cameraPosition;

    // Update is called once per frame
    public void Update()
    {
        transform.position = cameraPosition.position;
    }
}
