using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGunLJ : MonoBehaviour
{
    GunLJ gunlj;
    [SerializeField] private Transform[] Guntip;
    [SerializeField] private GameObject[] LJB;
    int numberGrapplingGun = 1;
    WeaponsSwitching WS;
    void Awake()
    {
        gunlj = GameObject.Find("LJ").GetComponent<GunLJ>();
        WS = GameObject.Find("WeaponsHolder").GetComponent<WeaponsSwitching>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gunlj.isReloading)
            return;

        if (WS.P_Changing)
            return;

        if (gunlj.currentAmmo <= 0)
        {
            StartCoroutine(gunlj.Reloat());
            return;   // stop here and not continue
        }

        switch (numberGrapplingGun)
        {
            case 1:
                {
                    if (Input.GetMouseButtonDown(0) && gunlj.readyToThrow && gunlj.maxAmmo > 0)
                    {
                        LJB[0].SetActive(false);
                        gunlj.attackpoint = Guntip[0];
                        gunlj.Shoot();
                        numberGrapplingGun++;
                    }
                    else if (!gunlj.isReloading)
                    {
                        LJB[1].SetActive(true);
                    }
                    break;
                }

            case 2:
                {
                    if (Input.GetMouseButtonDown(0) && gunlj.readyToThrow && gunlj.maxAmmo > 0)
                    {
                        LJB[1].SetActive(false);
                        gunlj.attackpoint = Guntip[1];
                        gunlj.Shoot();
                        numberGrapplingGun = 1;
                    }
                    else if (!gunlj.isReloading)
                    {
                        LJB[0].SetActive(true);
                    }
                    break;
                }
        }
    }
}
