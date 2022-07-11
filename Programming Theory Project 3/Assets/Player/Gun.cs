using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("References")]
    public Transform cam;
    public Transform attackpoint;
    public GameObject objectToThrow;

    [Header("Settings")]
    public int totalThrow;
    public float throwCooldown;

    [Header("Throwing")]
    public KeyCode throwKey = KeyCode.Mouse0;
    public float throwForce;
    public float ThrowUpwardForce;

    [Header("Particle")]
    public ParticleSystem effect;

    bool readyToThrow;

    private void Start()
    {
        readyToThrow = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(throwKey) && readyToThrow && totalThrow > 0)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        readyToThrow = false;

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
        Vector3 forceToAdd = cam.transform.forward * throwForce + transform.up * ThrowUpwardForce;

        projectleRb.AddForce(forceToAdd, ForceMode.Impulse);

        totalThrow--;

        // implement throwCoolDown
        Invoke(nameof(ResetThrow), throwCooldown);
    }

    void ResetThrow()
    {
        readyToThrow = true;
    }
}