using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private float speed = 10.0f;
    private float turnSpeed = 25.0f;
    public float HorizontalInput;
    public float forwardInput;

    private Rigidbody PlayerRb;

    // Start is called before the first frame update
    void Start()
    {
        PlayerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    void move()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");


        PlayerRb.AddForce(Vector3.forward * speed * forwardInput);                                                         
        transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime * HorizontalInput);
    }
}
