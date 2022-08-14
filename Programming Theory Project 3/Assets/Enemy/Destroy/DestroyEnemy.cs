using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    [SerializeField] GameObject Curpse;
    [SerializeField] ParticleSystem bisExplosion_02;
    [SerializeField] GameObject DamageShpere;
    [SerializeField] Transform Player;

    private Spawn SpawnScript;
    private float TimeDeadline;

    // Start is called before the first frame update
    void Start()
    {
        SpawnScript = GameObject.Find("SpawnManager").GetComponent<Spawn>();
        TimeDeadline = Time.time + 0.2f;
        Player = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        Damage();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Damage(Clone)")
        {
            SpawnScript.Change = 1;

            GameObject ObjectCurpse = Instantiate(Curpse);
            ObjectCurpse.transform.position = collision.transform.position;
            ObjectCurpse.transform.rotation = collision.transform.rotation;
            

            Destroy(gameObject);
        }

        if (collision.gameObject.name == "Player" && gameObject.name == "Spider_Current(Clone)")
        {
            ParticleSystem ObjectBisExplosion = Instantiate(bisExplosion_02);
            ObjectBisExplosion.transform.position = transform.position;
            ObjectBisExplosion.Play();

            SpawnScript.Change = 1;

            GameObject ObjectCurpse = Instantiate(Curpse);
            ObjectCurpse.transform.position = collision.transform.position;
            ObjectCurpse.transform.rotation = collision.transform.rotation;

            GameObject ObjectDamege = Instantiate(DamageShpere);
            ObjectDamege.transform.position = transform.position;

            Destroy(gameObject);
        }
    }

    void Damage()
    {
        if (gameObject.name == "Damage(Clone)")
        {
            if (Time.time >= TimeDeadline)
            {
                Destroy(gameObject);
            }
        }
    }

    void DestroyBound()
    {
        float distance = (Player.transform.position - transform.position).magnitude;

        //if (distance > )
        //{

        //}
    }
}
