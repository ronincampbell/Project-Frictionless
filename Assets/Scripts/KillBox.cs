using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour
{
    public GameObject player;

    void OnTriggerEnter(Collider other)
    {
        player.GetComponent<DeathScript>().PlayerDie();
        Debug.Log(player);
        Destroy(gameObject);
    }
}
