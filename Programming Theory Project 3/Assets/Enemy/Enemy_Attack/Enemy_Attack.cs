using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] GameObject[] Weapon;
    PlayerMovement Player;
    Rigidbody WeaponRb;
    Vector3 direction;
    [SerializeField] float timeShoot = 0;
    [SerializeField] float time = 0;
    public Vector3 dir
    {
        get { return direction; }
    }

    [SerializeField] int NumberWeapon = 0;

    // Start is called before the first frame update
    void Start()
    {
        WeaponRb = GetComponent<Rigidbody>();
        Player = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = Player.transform.position - transform.position;

        if (direction.magnitude <= 15f)
        {
            if (time < 1)
            {
                timeShoot = Time.time + 2f;
                time++;
            }

            if (NumberWeapon < 1 && Time.time >= timeShoot)
            {
                Instantiate(Weapon[0], transform.up , transform.rotation);

                NumberWeapon++;
                time = 0;
            }
        }
    }
}
