using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMove : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float _speed = 3f;
    [SerializeField] float smoothness = 5f;
    [SerializeField] int raysNb = 8;
    [SerializeField] float halfRange = 0.5f;
    [SerializeField] float raysEccentricity = 0.2f;
    [SerializeField] float outerRaysOffset = 2f;
    [SerializeField] float innerRaysOffset = 25f;

    private Vector3 velocity;
    private Vector3 lastVelocity;
    private Vector3 lastPosition;
    private Vector3 forward;
    private Vector3 upward;
    private Quaternion lastRot;
    private Vector3[] pn;

    [Header("RandomRotation")]
    public float RotationSpeed = 3f;
    float valueY = 1;
    float valueX = 0.5f;

    private bool IsWandering = false;
    private bool IsRotationRight = false;
    private bool IsRotationLeft = false;
    private bool IsWalking = false;

    [Header("Attack")]
    PlayerMovement Player;
    [SerializeField] float playerHeight;

    private void Awake()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    void Start()
    {
        velocity = new Vector3();
        forward = transform.forward;
        upward = transform.up;
        lastRot = transform.rotation;
    }


    void FixedUpdate()
    {
        velocity = (smoothness * velocity + (transform.position - lastPosition)) / (1f + smoothness);

        if (velocity.magnitude < 0.00025f)
            velocity = lastVelocity;

        lastPosition = transform.position;
        lastVelocity = velocity;


        Vector3 direction = Player.transform.position - transform.position;

        if (direction.magnitude < 15 && transform.position.y < 2)
        {
            transform.position += direction.normalized * 1f * _speed * Time.fixedDeltaTime;
        }
        else if (direction.magnitude > 15)
        {
            if (IsWandering == false)
                StartCoroutine(Wander());
            if (IsRotationRight == true)
                transform.position += Vector3.Cross(transform.up, transform.forward) * valueX * RotationSpeed * Time.fixedDeltaTime;
            if (IsRotationLeft == true)
                transform.position += Vector3.Cross(transform.up, transform.forward) * valueX * RotationSpeed * Time.fixedDeltaTime;
            if (IsWalking == true)
                transform.position += transform.forward * valueY * _speed * Time.fixedDeltaTime;
        }
        

        //valueY = Input.GetAxis("Vertical");
        //if (valueY != 0)
        //    transform.position += transform.forward * valueY * _speed * Time.fixedDeltaTime;

        //valueX = Input.GetAxis("Horizontal");
        //if (valueX != 0)
        //    transform.position += Vector3.Cross(transform.up, transform.forward) * valueX * _speed * Time.fixedDeltaTime;


        if (valueX != 0 || valueY != 0)
        {
            pn = GetClosestPoint(transform.position, transform.forward, transform.up, halfRange, 0.1f, 30, -30, 4);

            upward = pn[1];


            Vector3[] pos = GetClosestPoint(transform.position, transform.forward, transform.up, halfRange, raysEccentricity, innerRaysOffset, outerRaysOffset, raysNb);
            transform.position = Vector3.Lerp(lastPosition, pos[0], 1f / (1f + smoothness));

            forward = velocity.normalized;
            Quaternion q = Quaternion.LookRotation(forward, upward);
            transform.rotation = Quaternion.Lerp(lastRot, q, 1f / (1f + smoothness));
        }

        lastRot = transform.rotation;
    }



    static Vector3[] GetClosestPoint(Vector3 point, Vector3 forward, Vector3 up, float halfRange, float eccentricity, float offset1, float offset2, int rayAmount)
    {
        Vector3[] res = new Vector3[2] { point, up };
        Vector3 right = Vector3.Cross(up, forward);
        float normalAmount = 1f;
        float positionAmount = 1f;

        // huong cua tia
        Vector3[] dirs = new Vector3[rayAmount];
        float angularStep = 2f * Mathf.PI / (float)rayAmount;
        float currentAngle = angularStep / 2f;


        for (int i = 0; i < rayAmount; ++i)
        {
            dirs[i] = -up + (right * Mathf.Cos(currentAngle) + forward * Mathf.Sin(currentAngle)) * eccentricity;
            currentAngle += angularStep;
        }

        // chieu tung tia va xac dinh vi tri 
        foreach (Vector3 dir in dirs)
        {
            RaycastHit hit;
            Vector3 largener = Vector3.ProjectOnPlane(dir, up);

            // diem nam trong
            Ray ray = new Ray(point - (dir + largener) * halfRange + largener.normalized * offset1 / 100f, dir);
            //Debug.DrawRay(ray.origin, ray.direction, Color.red);
            if (Physics.SphereCast(ray, 0.01f, out hit, 2f * halfRange))
            {
                res[0] += hit.point;
                res[1] += hit.normal;
                normalAmount += 1;
                positionAmount += 1;
            }

            // diem nam ngoai
            ray = new Ray(point - (dir + largener) * halfRange + largener.normalized * offset2 / 100f, dir);
            //Debug.DrawRay(ray.origin, ray.direction, Color.green);
            if (Physics.SphereCast(ray, 0.01f, out hit, 2f * halfRange))
            {
                res[0] += hit.point;
                res[1] += hit.normal;
                normalAmount += 1;
                positionAmount += 1;
            }
        }

        res[0] /= positionAmount;
        res[1] /= normalAmount;

        // cong lai chia TB  --> 1 diem nam giua: cac diem nam trong va ngoai
        return res;
    }

    IEnumerator Wander()
    {
        float rotationTine = Random.Range(0, 2);
        float rotateWait = Random.Range(0, 2);
        int rotationDirection = Random.Range(1, 3);
        int walkWait = Random.Range(0, 2);
        int WalkTime = Random.Range(5, 10);

        IsWandering = true;
        yield return new WaitForSeconds(walkWait);

        IsWalking = true;
        yield return new WaitForSeconds(WalkTime);

        IsWalking = false;
        yield return new WaitForSeconds(rotateWait);

        if (rotationDirection == 1)
        {
            IsRotationLeft = true;
            yield return new WaitForSeconds(rotationTine/2);
            IsRotationLeft = false;
        }
        else if (rotationDirection == 2)
        {
            IsRotationRight = true;
            yield return new WaitForSeconds(rotationTine/2);
            IsRotationRight = false;
        }

        IsWandering = false;
    }
}
