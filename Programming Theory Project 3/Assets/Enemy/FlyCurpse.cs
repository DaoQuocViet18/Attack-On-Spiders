using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyCurpse : MonoBehaviour
{
    Rigidbody CurpseRb;
    private float TimeDeadline;

    float FlyX;
    float FlyZ;

    // Start is called before the first frame update
    void Start()
    {
        CurpseRb = gameObject.GetComponent<Rigidbody>();
        TimeDeadline = Time.time + 5f;
        FlyX = Random.Range(-3, 4);
        FlyZ = Random.Range(-3, 4);
        CurpseRb.AddForce(new Vector3(FlyX, 1, FlyZ) * 10, ForceMode.Impulse);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Time.time >= TimeDeadline)
        {
            Destroy(gameObject);
        }
    }
}
