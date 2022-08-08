using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("hoat dong duoc");
    }

    //private Spawn SpawnScript;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    SpawnScript = GameObject.Find("SpawnManager").GetComponent<Spawn>();      
    //}

    //private void Update()
    //{
    //    if (Time.time >= 10)
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    //if (collision.gameObject.name == "Spider_Current")
    //    //{
    //    //    SpawnScript.Change = 1;
    //    //    Destroy(collision.gameObject);
    //    //    Destroy(gameObject);
    //    //}
    //    Debug.Log("hoat dong duoc");

    //    if (CompareTag("Enemy"))
    //    {
    //        SpawnScript.Change = 1;
    //        Destroy(collision.gameObject);
    //        Destroy(gameObject);
    //    }
    //}



    //private void OnTriggerEnter(Collider other)
    //{
    //    //SpawnScript.Change = 1;

    //    Destroy(other.gameObject);
    //    Destroy(gameObject);

    //    //if (other.gameObject.name == "Damage")
    //    //{
    //    //    SpawnScript.Change = 1;
    //    //    Destroy(other.gameObject);
    //    //    Destroy(gameObject);
    //    //}
    //}
}
