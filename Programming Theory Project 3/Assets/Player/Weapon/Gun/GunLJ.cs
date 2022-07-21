using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLJ : MonoBehaviour
{
    [Header("References")]
    public Transform cam;
    public Transform attackpoint;
    public GameObject objectToThrow;

    [Header("settings")]
    public int maxAmmo;
    public float throwCooldown;
    public int currentAmmo;
    public bool isReloading = false;
    float TimeShootGun;
    public float reloatTime = 1.5f;

    [Header("Throwing")]
    public KeyCode throwKey = KeyCode.Mouse0;
    public float throwForce;
    public float ThrowUpwardForce;
    [SerializeField] LayerMask layermask;
    PlayerMovement PM;
    public Vector3 targetPosition;

    [Header("Particle")]
    WeaponsSwitching WS;
    public Animator animator;
    public ParticleSystem effect;

    public bool readyToThrow;

    

    private void Awake()
    {
        WS = GameObject.Find("WeaponsHolder").GetComponent<WeaponsSwitching>();
        PM = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }
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
        Test();
    }

    public void Shoot()
    {
        readyToThrow = false;

        currentAmmo--;

        effect.Play();

        
        // calculate direction
        Vector3 forceDirection = cam.transform.forward;

        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 500f, layermask))
        {
            
            forceDirection = (hit.point - attackpoint.position).normalized;
        }

        
        // add Force
        Vector3 forceToAdd = forceDirection * throwForce + transform.up * ThrowUpwardForce;

        // imstantiate object to throw
        GameObject projectite = Instantiate(objectToThrow, attackpoint.position, cam.rotation);

        // get rigidbody component
        Rigidbody projectleRb = projectite.GetComponent<Rigidbody>();

        projectleRb.AddForce(forceToAdd, ForceMode.Impulse);

        // implement throwCoolDown
        Invoke(nameof(ResetThrow), throwCooldown);
    }

    void ResetThrow()
    {
        readyToThrow = true;
    }

    public IEnumerator Reloat()
    {
        isReloading = true;
        animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloatTime - 0.25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(0.25f);
        currentAmmo = maxAmmo;
        isReloading = false;
    }

    void Test()
    {
        RaycastHit hit;

        Vector3 forceDirection = cam.transform.forward;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 500f))
        {
            Debug.DrawRay(cam.transform.position, cam.transform.forward * hit.distance, Color.red);
            targetPosition = hit.point;
            forceDirection = (hit.point - attackpoint.position).normalized;
        }

        Vector3 forceToAdd = forceDirection * throwForce;

        Debug.DrawRay(attackpoint.transform.position, forceToAdd * hit.distance, Color.green);
    }
}