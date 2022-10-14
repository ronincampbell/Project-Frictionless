using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Animator EDoorsClosed;
    public Animator OuterClosed;
    public GameObject EColliders;

    void OnTriggerEnter(Collider other)
    {
        EColliders.SetActive(true);
        EDoorsClosed.SetBool("Closed", true);
        OuterClosed.SetBool("OuterClosed", true);
    }
}