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
        if (Time.time >= TimeDeadline)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Spider_Current(Clone)")
        {
            SpawnScript.Change = 1;
            Destroy(collision.gameObject);

            GameObject ObjectCurpse = Instantiate(Curpse);
            ObjectCurpse.transform.position = collision.transform.position;
            ObjectCurpse.transform.rotation = collision.transform.rotation;
            

            Destroy(gameObject);
        }
    }
}
