using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Sword : MonoBehaviour
{
    Transform Player;
    Rigidbody PlayerRb;
    private float timeShoot;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<Transform>();
        PlayerRb = GetComponent<Rigidbody>();
        timeShoot = Time.time + 2f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Player.transform.position - transform.position;
        if (Time.time >= timeShoot)
        {
            PlayerRb.AddForce(direction.normalized * 10f, ForceMode.Impulse);
        }
        else
        {
            transform.LookAt(Player);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player" || collision.gameObject.CompareTag("Untagged") || collision.gameObject.gameObject.name == "Shield_Player")
        {
            Destroy(gameObject);
        }
    }
}
