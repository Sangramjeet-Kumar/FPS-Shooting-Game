
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    private Animator animator;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animator = GetComponent<Animator>();
            Shoot();
        }
    }
    void Shoot()
    {
        muzzleFlash.Play();
        animator.SetTrigger("recoil");
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
           Enemy target = hit.transform.GetComponent<Enemy>();
            if (target != null) 
            {
                target.takeDamage(damage);
            }
        }

    }
}
