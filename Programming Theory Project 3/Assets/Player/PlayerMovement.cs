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
    public float airGGMultiplier;
    public KeyCode jumbKey = KeyCode.Space;
    bool readyToJumb = true;

    [Header("Slope Handling")]
    [SerializeField] float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    private float HorizontalInput;
    private float forwardInput;

    public Vector3 moveDirection;
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
        OnSlope();

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
        moveDirection = orientation.transform.forward * forwardInput + orientation.right * HorizontalInput;

        if (OnSlope() && !exitingSlope)
        {
            PlayerRb.AddForce(GetSlopeMoveDirection() * speed * 10.0f, ForceMode.Force);

            if (PlayerRb.velocity.y > 0)
                PlayerRb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        if (grounded)
            PlayerRb.AddForce(moveDirection.normalized * speed * 10.0f, ForceMode.Force);
        else if (!grounded && Input.GetMouseButtonUp(1))
            PlayerRb.AddForce(moveDirection.normalized * speed * 10.0f * airMultiplier, ForceMode.Force);
        else if (!grounded && Input.GetMouseButton(1))
            PlayerRb.AddForce(moveDirection.normalized * speed * 10.0f * airGGMultiplier, ForceMode.Force);
            
        PlayerRb.useGravity = !OnSlope();
    }

    void Jumb()
    {
        exitingSlope = true;

        // reset a vellocity
        PlayerRb.velocity = new Vector3(PlayerRb.velocity.x, 0f, PlayerRb.velocity.z);

        PlayerRb.AddForce(Vector3.up * jumbFouce, ForceMode.Impulse);
    }

    void ResetJumb()
    {
        readyToJumb = true;

        exitingSlope = false;
    }

    void SpeedControl()
    {
        // limiting speed on slope
        if (OnSlope() && !exitingSlope)
        {
            if (PlayerRb.velocity.magnitude > speed)
                PlayerRb.velocity = PlayerRb.velocity.normalized * speed;
        }

        // limiting speed on ground or on air
        else
        {
            Vector3 flatVel = new Vector3(PlayerRb.velocity.x, 0, PlayerRb.velocity.z);

            // limiting velocity if need
            if (flatVel.magnitude > speed)
            {
                Vector3 limitedVel = flatVel.normalized * speed;

                PlayerRb.velocity = new Vector3(limitedVel.x, PlayerRb.velocity.y, limitedVel.z);
            }
        }
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            Debug.DrawRay(slopeHit.point, slopeHit.normal * 50f, Color.red);
            return angle < maxSlopeAngle && angle != 0;
        }
        else
            return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }
}

