using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBound : MonoBehaviour
{
    private Spawn SpawnScript;

    private float Xbound = 74.5f;
    private float Zbound = 74.5f;
    private float Ybound = -0.2f;

    void Start()
    {
        SpawnScript = GameObject.Find("SpawnManager").GetComponent<Spawn>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > Xbound || transform.position.x < -Xbound)
        {
            SpawnScript.Change = 1;
            Destroy(gameObject);
        }
        else if (transform.position.z > Zbound || transform.position.z < -Zbound)
        {
            SpawnScript.Change = 1;
            Destroy(gameObject);
        }
        else if (transform.position.y < Ybound)
        {
            SpawnScript.Change = 1;
            Destroy(gameObject);
        }
    }
}
