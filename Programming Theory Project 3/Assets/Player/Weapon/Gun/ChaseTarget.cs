using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseTarget : MonoBehaviour
{
    [SerializeField] private Vector3 target;
    private float throwForce = 100f;
    private float TimeFonce = 0;
    private Rigidbody projectleRb;

    GunLJ gunlj;

    // Start is called before the first frame update
    void Awake()
    {
        projectleRb = GetComponent<Rigidbody>();
        gunlj = GameObject.Find("LJ").GetComponent<GunLJ>();
    }

    private void Start()
    {
        target = gunlj.targetPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time == TimeFonce)
        {
            // calculate direction
            Vector3 forceDirection = (target - transform.position).normalized;

            // add Force
            Vector3 forceToAdd = forceDirection * throwForce;

            projectleRb.AddForce(forceToAdd, ForceMode.Impulse);

            TimeFonce += 1f;
        }

    }
}
