using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallrun_PickUp : MonoBehaviour
{
    public GameObject player;

    void OnTriggerEnter(Collider other)
    {
        player.GetComponent<WallRunning>().enabled = true;
        Destroy(gameObject);
    }
}
