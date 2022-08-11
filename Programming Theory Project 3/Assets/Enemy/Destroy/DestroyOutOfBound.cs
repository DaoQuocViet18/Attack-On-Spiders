using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBound : MonoBehaviour
{
    private float Xbound = 75;
    private float Zbound = 75;
    private float Ybound = -0.5f;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x >= Xbound || transform.position.x <= -Xbound)
        {
            Destroy(gameObject);
        }
        else if (transform.position.z >= Zbound || transform.position.z <= -Zbound)
        {
            Destroy(gameObject);
        }
        else if (transform.position.y <= Ybound)
        {
            Destroy(gameObject);
        }
    }
}
