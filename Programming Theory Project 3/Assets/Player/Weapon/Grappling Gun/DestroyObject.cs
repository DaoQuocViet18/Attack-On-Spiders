using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] float TimeofAmmo = 5f;

    void Start()
    {
        StartCoroutine(Wait());      
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(TimeofAmmo);
        Destroy(gameObject);
    }
}
