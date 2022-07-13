using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGrapplingGun : MonoBehaviour
{
    GrapplingGun GG1;
    GrapplingGun GG2;
    void Awake()
    {
        GG1 = GameObject.Find("GearLeft").GetComponent<GrapplingGun>();
        GG2 = GameObject.Find("GearRight").GetComponent<GrapplingGun>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
