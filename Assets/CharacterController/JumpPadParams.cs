using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class JumpPadParams : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip BounceSFX;
    public float volume=1f;
    public PlayerCam cam;
    public float padForce;
    private void OnTriggerEnter(Collider other) 
    {
        cam.DoFov(80f);
        audioSource.PlayOneShot(BounceSFX, volume);
    }
}
