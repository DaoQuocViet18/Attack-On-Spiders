using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    Transform Cam;

    // Start is called before the first frame update
    void Start()
    {
        Cam = GameObject.Find("Main Camera").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + Cam.position);
    }
}
