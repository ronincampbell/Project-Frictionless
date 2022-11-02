using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour
{
    // Declare necessary variables
    public GameObject player;

    // When collided, run the player die function and destroy the player controller
    void OnTriggerEnter(Collider other)
    {
        player.GetComponent<DeathScript>().PlayerDie();
        Destroy(gameObject);
    }
}
