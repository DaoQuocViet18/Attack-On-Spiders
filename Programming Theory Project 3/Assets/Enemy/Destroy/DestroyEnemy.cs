using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    [SerializeField] GameObject Curpse;

    private Spawn SpawnScript;
    private float TimeDeadline;

    // Start is called before the first frame update
    void Start()
    {
        SpawnScript = GameObject.Find("SpawnManager").GetComponent<Spawn>();
        TimeDeadline = Time.time + 0.2f;
    }

    private void Update()
    {
        Damage();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Damage(Clone)" || collision.gameObject.name == "Player")
        {
            SpawnScript.Change = 1;

            GameObject ObjectCurpse = Instantiate(Curpse);
            ObjectCurpse.transform.position = collision.transform.position;
            ObjectCurpse.transform.rotation = collision.transform.rotation;
            

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
}
