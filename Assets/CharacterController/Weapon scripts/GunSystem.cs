using UnityEngine;
using TMPro;


public class GunSystem : MonoBehaviour
{
    // Firing system by Brakeys on YouTube | Reloading and ammo based on code by Dave / GameDevelopment on YouTube | Ammo indicator by me

   public float damage = 10f;
   public float range = 100f;
   public float fireRate = 15f;
   public float impactForce = 30f;
   public float reloadTime = 2f;
   public int magazineSize = 30;
   int bulletsLeft, bulletsShot, thirdOfMag;

   bool reloading = false;

   public Camera fpsCam;
   public ParticleSystem muzzleFlash;
   public GameObject impactEffect;
   public TextMeshProUGUI text;
   public GameObject bullet1, bullet2, bullet3;
   public Animator reloadAnim;
   public GameObject PauseMenu;
   public GameObject backdrop;

    public AudioSource audioSource;
    public AudioClip fireSFX;
    public AudioClip reloadSFX;
    public float volume=0.5f;

   private float nextTImeToFire = 0f;

   private void Start() 
   {
        bulletsLeft = magazineSize;
        thirdOfMag = magazineSize / 3;
   }
   
   private void Update() 
   {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= nextTImeToFire && !reloading && bulletsLeft > 0 && !PauseMenu.activeInHierarchy && !backdrop.activeInHierarchy)
        {
            nextTImeToFire = Time.time + 1f/fireRate;
            Shoot();
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
           reloadAnim.SetBool("Firing", false);
        }

        if(Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();

        CheckAmmo();
   }

   private void Shoot()
   {
        muzzleFlash.Play();
        bulletsLeft--;
        reloadAnim.SetBool("Firing", true);
        audioSource.PlayOneShot(fireSFX, volume);

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
        reloadAnim.SetBool("Reloading", true);
        audioSource.PlayOneShot(reloadSFX, volume);
        Invoke("ReloadFinished", reloadTime);
   } 

   private void ReloadFinished()
   {
        bulletsLeft = magazineSize;
        bullet1.SetActive(true);
        bullet2.SetActive(true);
        bullet3.SetActive(true);
        reloading = false;
        reloadAnim.SetBool("Reloading", false);
   }

   // Check Ammo amount for UI Elements 

   private void CheckAmmo()
   {
     text.SetText(bulletsLeft + " / " + magazineSize);

     if (bulletsLeft <= 2 * thirdOfMag)
     {
          bullet3.SetActive(false);
     }

     if (bulletsLeft <= thirdOfMag)
     {
          bullet2.SetActive(false);
     }

     if (bulletsLeft == 0)
     {
          bullet1.SetActive(false);
     }

   }
}
