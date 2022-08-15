using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    SpringJoint joint;
    Vector3 grapplePoint;
    [SerializeField] LayerMask whatIsGrappleable;
    public Transform guntip, cam, player;
    float maxDistance = 300f;

    private void Awake()
    {

    }

    private void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 500f, whatIsGrappleable))
        {
            Debug.DrawRay(cam.transform.position, cam.transform.forward * hit.distance, Color.red);
        }
    }


    public  void StartGrappleSingle()
    {
        RaycastHit hit;

        if (Physics.Raycast(cam.position, cam.forward, out hit, maxDistance, whatIsGrappleable))
        {
            grapplePoint = hit.point;

            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            // the distance grapple will try to keep from grapple poinr
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            // Change there value to fix your game
            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;
        }
    }


    //public void StartGrappleDual()
    //{

    //}

    public void StopGrapple()
    {
        Destroy(joint);
    }

    public bool IsGrapping()
    {
        return joint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }
}
