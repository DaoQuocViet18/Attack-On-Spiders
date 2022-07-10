using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed;
    public Transform orientation;

    public float groundDrag;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    private bool grounded;

    float HorizontalInput;
    float forwardInput;

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

        CheskGround();

        SpeedControl();

        
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void CheskGround()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        if (grounded)
        {
            PlayerRb.drag = groundDrag;
        }
        else
        {
            PlayerRb.drag = 0;
        }
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

    void SpeedControl()
    {
        Vector3 flaVel = new Vector3(PlayerRb.velocity.x, 0f, PlayerRb.velocity.z);

        // limit velocity if needed 
        if (flaVel.magnitude > speed)
        {
            Vector3 limitedVel = flaVel.normalized * speed;
            PlayerRb.velocity = new Vector3(limitedVel.x, PlayerRb.velocity.y, limitedVel.z);
        }
    }
    
}
