using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 8.267f;
    private bool isReloading = false;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public Animator weaponAnimator;
    public AmmoManager ammoManager;

    void Start()
    {
        currentAmmo = maxAmmo;
        weaponAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isReloading)
            return;

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    public void AddAmmo(int ammoAmount)
    {
        currentAmmo = Mathf.Clamp(currentAmmo + ammoAmount, 0, maxAmmo);
        Debug.Log("Picked up ammo: " + ammoAmount + ". Current ammo: " + currentAmmo);
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");

        weaponAnimator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime);

        weaponAnimator.SetBool("Reloading", false);

        currentAmmo = maxAmmo;
        ammoManager.ReloadAmmo(maxAmmo); // Update ammo in AmmoManager
        isReloading = false;
    }

    void Shoot()
    {
        if (isReloading)
            return;

        if (currentAmmo > 0)
        {
            muzzleFlash.Play();
            weaponAnimator.SetTrigger("recoil");
            currentAmmo--;

            if (ammoManager != null)
            {
                ammoManager.UseAmmo(1); // Update ammo in AmmoManager
            }

            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name);
                Enemy target = hit.transform.GetComponent<Enemy>();
                if (target != null)
                {
                    target.takeDamage(damage);
                }
            }
        }
        else
        {
            StartCoroutine(Reload());
        }
    }
}
