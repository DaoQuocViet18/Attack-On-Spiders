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
    float deadlineShoot = 2.5f;
    float TimeShootGun;
    public float reloatTime = 1.5f;

    [Header("Throwing")]
    public KeyCode throwKey = KeyCode.Mouse0;
    public float throwForce;
    public float ThrowUpwardForce;
    private int FirstShoot = 0;
    [SerializeField] LayerMask layermask;
    PlayerMovement PM;

    [Header("Particle")]
    WeaponsSwitching WS;
    public Animator animator;
    public ParticleSystem effect;

    bool readyToThrow;

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
        if (isReloading)
            return;

        if (WS.P_Changing)
            return;

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reloat());
            return;   // stop here and not continue
        }

        inputShoot();
    }

    void inputShoot()
    {
        if (FirstShoot == 0)
        {
            TimeShootGun = Time.time + deadlineShoot;
        }


        if (Input.GetKeyDown(throwKey) && readyToThrow && maxAmmo > 0)
        {
            FirstShoot++;
            if (FirstShoot == 1)
            {
                animator.SetBool("Shooting", true);
                StartCoroutine(PrepapeShoot());
            }
            else
                Shoot();
        }
        else if (Input.GetKeyUp(throwKey))
        {
            TimeShootGun = Time.time + deadlineShoot;
        }

        if (Time.time >= TimeShootGun)
        {
            animator.SetBool("Shooting", false);
            FirstShoot = 0;
        }
    }

    void Shoot()
    {
        readyToThrow = false;

        currentAmmo--;

        effect.Play();

        // imstantiate object to throw
        GameObject projectite = Instantiate(objectToThrow,new Vector3(0,-100,0), cam.rotation);

        // get rigidbody component
        Rigidbody projectleRb = projectite.GetComponent<Rigidbody>();

        // calculate direction
        Vector3 forceDirection = cam.transform.forward;

        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 500f , layermask))
        {
       
            forceDirection = (hit.point - attackpoint.position).normalized;
        }


        // add Force
        Vector3 forceToAdd = forceDirection * throwForce + transform.up * ThrowUpwardForce;

        projectite.transform.position = attackpoint.position;
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

    IEnumerator PrepapeShoot()
    {
        yield return new WaitForSeconds(0.18f);
        Shoot();
    }

    void Test()
    {
        RaycastHit hit;

        Vector3 forceDirection = cam.transform.forward;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 500f))
        {
            Debug.DrawRay(cam.transform.position, cam.transform.forward * hit.distance, Color.red);
            forceDirection = (hit.point - attackpoint.position).normalized;
        }

        Vector3 forceToAdd = forceDirection * throwForce;

        Debug.DrawRay(attackpoint.transform.position, forceToAdd * hit.distance, Color.green);
    }
}