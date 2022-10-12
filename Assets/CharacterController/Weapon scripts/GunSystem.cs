using UnityEngine;
using TMPro;


public class GunSystem : MonoBehaviour
{
    // Firing system by Brakeys on YouTube | Reloading and ammo based on code by Dave / GameDevelopment on YouTube

   public float damage = 10f;
   public float range = 100f;
   public float fireRate = 15f;
   public float impactForce = 30f;
   public float reloadTime = 2f;
   public int magazineSize = 30;
   int bulletsLeft, bulletsShot;

   bool reloading = false;

   public Camera fpsCam;
   public ParticleSystem muzzleFlash;
   public GameObject impactEffect;
   public TextMeshProUGUI text;

   private float nextTImeToFire = 0f;

   private void Start() 
   {
        bulletsLeft = magazineSize;
   }
   
   private void Update() 
   {
        if (Input.GetKey(KeyCode.Mouse0) && Time.time >= nextTImeToFire && !reloading && bulletsLeft > 0)
        {
            nextTImeToFire = Time.time + 1f/fireRate;
            Shoot();
        }

        if(Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();

        text.SetText(bulletsLeft + " / " + magazineSize);
   }

   private void Shoot()
   {
        muzzleFlash.Play();

        bulletsLeft--;

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject imapactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(imapactGO, 2f);
        }
   }
   
   private void Reload()
   {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
   } 

   private void ReloadFinished()
   {
        bulletsLeft = magazineSize;
        reloading = false;
   }
}
