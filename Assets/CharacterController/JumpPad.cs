using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public PlayerMovement pm;

    void OnTriggerEnter(Collider other)
    {   
        pm.jumpForce = 400f;
    }
}
