using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingRope : MonoBehaviour
{
    Spring spring;
    LineRenderer lr;
    public GrapplingGun grapplingGun;
    public int quality;
    public float damper;
    public float strength;
    public float velocity;
    public float waveCount;
    public float waveHeght;
    public AnimationCurve affectCurn;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        spring = new Spring();
        spring.SetTarget(0);
    }

    private void LateUpdate()
    {
        DrawRope();
    }

    private Vector3 currentGrapplePosition;

    void DrawRope()
    {
        // if not grappling, don't draw
        if (!grapplingGun.IsGrapping())
        {
            currentGrapplePosition = grapplingGun.guntip.position;
            spring.Reset();

            if (lr.positionCount > 0)
                lr.positionCount = 0;
            return;
        }

        if (lr.positionCount == 0)
        {
            spring.SetVelocity(velocity);
            lr.positionCount = quality + 1;
        }

        spring.SetDamper(damper);
        spring.SetStrength(strength);
        spring.Update(Time.deltaTime);

        var grapplePoint = grapplingGun.GetGrapplePoint();
        var gunTipPosition = grapplingGun.guntip.position;
        var up = Quaternion.LookRotation((grapplePoint - gunTipPosition).normalized) * Vector3.up;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);

        for (int i = 0; i < quality + 1; i++)
        {
            var delta = i / (float)quality;
            
            var offest = up * waveCount * Mathf.Sin(delta * waveCount * Mathf.PI) * spring.Value * affectCurn.Evaluate(delta);

            lr.SetPosition(i, Vector3.Lerp(gunTipPosition, currentGrapplePosition, delta) + offest);
        }

    }
}
