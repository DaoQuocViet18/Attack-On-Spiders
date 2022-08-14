using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] KeyCode shield = KeyCode.Tab;
    [SerializeField] GameObject objectShield;

    // Start is called before the first frame update
    void Start()
    {
        objectShield.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        MoveInput();
    }

    void MoveInput()
    {
        if (Input.GetKey(shield))
        {
            objectShield.SetActive(true);
        }
        else
        {
            objectShield.SetActive(false);
        }
    }
}
