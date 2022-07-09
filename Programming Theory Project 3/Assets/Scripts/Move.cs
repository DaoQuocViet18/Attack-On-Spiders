using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CompareTag("cube"))
        {
            transform.Translate(Vector3.forward * 10 * Time.deltaTime);
        } 

        if (CompareTag("capsule"))
        {
            int speed = Random.Range(-20, 21);
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
    }
}
