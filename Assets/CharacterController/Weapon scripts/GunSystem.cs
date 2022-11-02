using UnityEngine;
using TMPro;


public class GunSystem : MonoBehaviour
{
    // Base Firing system by Brakeys on YouTube | Reloading and ammo based on code by Dave / GameDevelopment on YouTube

   // Declare necessary variables
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
        // Set the magazine to full and define what a third of the magazine capacity is
        bulletsLeft = magazineSize;
        thirdOfMag = magazineSize / 3;
   }
   
   private void Update() 
   {
        // If player tries to fire, and all conditions are met, shoot and set the delay for the next shot
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= nextTImeToFire && !reloading && bulletsLeft > 0 && !PauseMenu.activeInHierarchy && !backdrop.activeInHierarchy)
        {
            nextTImeToFire = Time.time + 1f/fireRate;
            Shoot();
        }

        // If player stops firing set firing to false
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
           reloadAnim.SetBool("Firing", false);
        }

        // If player presses 'R' and does not have max ammo, Reload
        if(Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();

        // Check ammo amount for UI ellements
        CheckAmmo();
   }

   private void Shoot()
   {
        // Set firing to true, display muzzleFlash effect, remove one bullet from the magazine, and play SFX
        muzzleFlash.Play();
        bulletsLeft--;
        reloadAnim.SetBool("Firing", true);
        audioSource.PlayOneShot(fireSFX, volume);

        // If bullet hits a target with a Target script, cause it to take damage
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

            // Create and bullet impact effect at hit point, then destroy it after a delay
            GameObject imapactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(imapactGO, 2f);
        }
   }
   
   // Begin reload animation, SFX, and invoke reloadFinished after the designated reload time
   private void Reload()
   {
        reloading = true;
        reloadAnim.SetBool("Reloading", true);
        audioSource.PlayOneShot(reloadSFX, volume);
        Invoke("ReloadFinished", reloadTime);
   } 

   // Reset bullets and UI elements - Complete reload
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
