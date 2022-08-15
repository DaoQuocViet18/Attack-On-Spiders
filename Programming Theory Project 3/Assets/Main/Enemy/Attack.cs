using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack : MonoBehaviour
{
    [SerializeField] GameObject[] Weapon;
    Transform Player;
    Rigidbody WeaponRb;
    Vector3 direction;
    private float timeShoot = 0;
    public Vector3 dir
    {
        get { return direction; }
    }

    // Start is called before the first frame update
    void Start()
    {
        WeaponRb = GetComponent<Rigidbody>();
        Player = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = Player.transform.position - transform.position;

        if (direction.magnitude <= 15f)
        {
            if (Time.time >= timeShoot)
            {
                Vector3 direction = Player.transform.position - transform.position;

                Instantiate(Weapon[0], transform.position + transform.up * 5f, transform.rotation);
                timeShoot = Time.time + 3f;
            }
        }
    }
}
