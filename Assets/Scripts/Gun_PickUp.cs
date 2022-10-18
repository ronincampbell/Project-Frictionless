using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_PickUp : MonoBehaviour
{
    public GameObject Gun;
    public GameObject UIText;
    public GameObject Bullets;

    void OnTriggerEnter(Collider other)
    {
        Gun.SetActive(true);
        UIText.SetActive(true);
        Bullets.SetActive(true);
        Destroy(gameObject);
    }
}
