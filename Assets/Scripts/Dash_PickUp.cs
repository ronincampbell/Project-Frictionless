using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash_PickUp : MonoBehaviour
{
    public GameObject player;

    void OnTriggerEnter(Collider other)
    {
        player.GetComponent<Dashing>().enabled = true;
        Destroy(gameObject);
    }

}
