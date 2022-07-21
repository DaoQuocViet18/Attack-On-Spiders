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

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.CompareTag("Destroy"))
        {
            Destroy(gameObject);
        }
        
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(TimeofAmmo);
        Destroy(gameObject);
    }
}
