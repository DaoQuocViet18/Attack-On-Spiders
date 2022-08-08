using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] ParticleSystem bisExplosion;
    [SerializeField] GameObject Damage;
    [SerializeField] float TimeofAmmo = 5f;
    [SerializeField] float TimeofParticle = 2f;

    void Start()
    {
        StartCoroutine(Wait());      
    }

    private void OnCollisionEnter(Collision other)
    {
        if (gameObject.CompareTag("Destroy"))
        {
            ParticleSystem ObjectBisExplosion =  Instantiate(bisExplosion);
            ObjectBisExplosion.transform.position = transform.position;
            //ObjectBisExplosion.transform.Rotate(-transform.rotation.x, -transform.rotation.y, -transform.rotation.z, Space.Self);
            ObjectBisExplosion.Play();

            GameObject ObjectDamege = Instantiate(Damage);
            ObjectDamege.transform.position = transform.position;

            Destroy(gameObject);
        }
        
    }

    IEnumerator Wait()
    {
        if (CompareTag("Bullet"))
        {
            yield return new WaitForSeconds(TimeofAmmo);
            Destroy(gameObject);
        }
        else if(CompareTag("Particle"))
        {
            yield return new WaitForSeconds(TimeofParticle);
            Destroy(gameObject);  
        }
    }
}
