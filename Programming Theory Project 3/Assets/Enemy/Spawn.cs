using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject[] enemy;
    private float spawnDelay = 2f;
    private float spawnInterval = 1.5f;
    public int NumberEnemy = 0;
    public int NumberPoint = 0;
    Vector3 spawnLocation;

    public int Change
    {
        set { NumberEnemy -= value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", spawnDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    void SpawnEnemy()
    {


        if (NumberEnemy <= 20)
        {
            NumberPoint = Random.Range(1, 5);
            if (NumberPoint == 1)
            {
                spawnLocation = new Vector3(Random.Range(40, 71), 0, Random.Range(-70, 71));
            }
            else if (NumberPoint == 2)
            {
                spawnLocation = new Vector3(Random.Range(-70, 71), 0, Random.Range(40, 71));
            }
            else if (NumberPoint == 3)
            {
                spawnLocation = new Vector3(Random.Range(-40, -71), 0, Random.Range(-70, 71));
            }
            else if (NumberPoint == 4)
            {
                spawnLocation = new Vector3(Random.Range(-70, 71), 0, Random.Range(-40, -71));
            }

            Instantiate(enemy[0], spawnLocation, enemy[0].transform.rotation);
            NumberEnemy++;
        }
    }
}
