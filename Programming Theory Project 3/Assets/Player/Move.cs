using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [Header("Movement")]
    private float speed = 7.0f;
    public float HorizontalInput;
    public float forwardInput;
    public Transform orientation;
    Vector3 moveDirection;
    private Rigidbody PlayerRb;

    // Start is called before the first frame update
    void Start()
    {
        PlayerRb = GetComponent<Rigidbody>();
        PlayerRb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        MoveInput();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MoveInput()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");
    }

    void MovePlayer()
    {
        moveDirection = (orientation.transform.forward * forwardInput + orientation.right * HorizontalInput).normalized;

        PlayerRb.AddForce(moveDirection * speed * 3.0f, ForceMode.Force);
    }
}
