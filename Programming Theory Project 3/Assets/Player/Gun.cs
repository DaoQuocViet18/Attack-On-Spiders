using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("References")]
    public Transform cam;
    public Transform attackpoint;
    public GameObject objectToThrow;

    [Header("settings")]
    public int maxAmmo;
    public float throwCooldown;
    [SerializeField] int currentAmmo;
    [SerializeField] bool isReloading = false;
    public float reloatTime = 1f;

    [Header("Throwing")]
    public KeyCode throwKey = KeyCode.Mouse0;
    public float throwForce;
    public float ThrowUpwardForce;

    [Header("Particle")]
    public Animator animator;
    public ParticleSystem effect;

    bool readyToThrow;

    private void Start()
    {
        readyToThrow = true;

        currentAmmo = maxAmmo;
    }

    private void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }

    private void Update()
    {
        if (isReloading)
            return;

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reloat());
            return;   // stop here and not continue
        }  

        if (Input.GetKeyDown(throwKey) && readyToThrow && maxAmmo > 0)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        readyToThrow = false;

        currentAmmo--;

        effect.Play();

        // imstantiate object to throw
        GameObject projectite = Instantiate(objectToThrow, attackpoint.position, cam.rotation);

        // get rigidbody component
        Rigidbody projectleRb = projectite.GetComponent<Rigidbody>();

        // calculate direction
        Vector3 forceDirection = cam.transform.forward;

        RaycastHit hit;

        if (Physics.Raycast(cam.position, cam.forward, out hit, 500f))
        {
            forceDirection = (hit.point - attackpoint.position).normalized;
        }

        // add Force
        Vector3 forceToAdd = forceDirection * throwForce + transform.up * ThrowUpwardForce;

        projectleRb.AddForce(forceToAdd, ForceMode.Impulse);

        // implement throwCoolDown
        Invoke(nameof(ResetThrow), throwCooldown);
    }
    
    void ResetThrow()
    {
        readyToThrow = true;
    }  
    
    IEnumerator Reloat()
    {
        isReloading = true;
        animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloatTime - 0.25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(0.25f);
        currentAmmo = maxAmmo;
        isReloading = false;
    }   
}