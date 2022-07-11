using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed;
    public Transform orientation;

    public float groundDrag;

    [Header("Jumb")]
    public float jumbFouce;
    public float JumbCooldown;
    public float airMultiplier;
    public KeyCode jumbKey = KeyCode.Space;
    bool readyToJumb = true;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    private float HorizontalInput;
    private float forwardInput;

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
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MoveInput();
        SpeedControl();

        // handle Drag
        if (grounded)
            PlayerRb.drag = groundDrag;
        else
            PlayerRb.drag = 0;
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MoveInput()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");


        // when to jumb
        if (Input.GetKeyDown(jumbKey) && readyToJumb && grounded)
        {
            readyToJumb = false;
            Jumb();
            Invoke(nameof(ResetJumb), JumbCooldown);
        }
    }

    void MovePlayer()
    {
        moveDirection = (orientation.transform.forward * forwardInput + orientation.right * HorizontalInput).normalized;

        PlayerRb.AddForce(moveDirection * speed * 3.0f, ForceMode.Force);

        if (grounded)
            PlayerRb.AddForce(moveDirection * speed * 3.0f, ForceMode.Force);
        else if (!grounded)
            PlayerRb.AddForce(moveDirection * speed * 3.0f * airMultiplier, ForceMode.Force);
    }

    void Jumb()
    {
        // reset a vellocity
        PlayerRb.velocity = new Vector3(PlayerRb.velocity.x, 0f, PlayerRb.velocity.z);

        PlayerRb.AddForce(Vector3.up * jumbFouce, ForceMode.Impulse);
    }

    void ResetJumb()
    {
        readyToJumb = true;
    }

    void SpeedControl()
    {
        Vector3 flatVel = new Vector3(PlayerRb.velocity.x, 0, PlayerRb.velocity.z);

        if (flatVel.magnitude > speed)
        {
            Vector3 limitedVel = flatVel.normalized * speed;

            PlayerRb.velocity = new Vector3(limitedVel.x, PlayerRb.velocity.y, limitedVel.z);
        }
    }
}

