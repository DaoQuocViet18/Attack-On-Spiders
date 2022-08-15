using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGrapplingGun : MonoBehaviour
{
    GrapplingGun GG;
    public Transform[] Guntip;
    int numberGrapplingGun = 1;
    void Awake()
    {
        GG = GameObject.Find("Cylinder.004").GetComponent<GrapplingGun>();
    }

    // Update is called once per frame
    void Update()
    {     
        switch (numberGrapplingGun)
        {
            case 1:
                {
                    if (Input.GetMouseButtonDown(1))
                    {
                        GG.guntip = Guntip[0];
                        GG.StartGrappleSingle();
                    }
                    else if (Input.GetMouseButtonUp(1))
                    {
                        GG.StopGrapple();
                        numberGrapplingGun++;
                        //GG2.StopGrapple();
                    }
                    break;
                }

            case 2:
                {
                    if (Input.GetMouseButtonDown(1))
                    {
                        GG.guntip = Guntip[1];
                        GG.StartGrappleSingle();
                    }
                    else if (Input.GetMouseButtonUp(1))
                    {
                        //GG1.StopGrapple();
                        GG.StopGrapple();
                        numberGrapplingGun = 1;
                    }                  
                    break;
                }
        }    
    }
}
