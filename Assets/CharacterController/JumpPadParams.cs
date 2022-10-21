using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class JumpPadParams : MonoBehaviour
{
    public PlayerCam cam;
    public float padForce;
    private void OnTriggerEnter(Collider other) 
    {
        cam.DoFov(80f);
    }
}
