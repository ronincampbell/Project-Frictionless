using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide_PickUp : MonoBehaviour
{
    public GameObject player;

    void OnTriggerEnter(Collider other)
    {
        player.GetComponent<Sliding>().enabled = true;
        Destroy(gameObject);
    }

}
