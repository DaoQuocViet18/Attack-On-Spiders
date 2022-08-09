using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject[] enemy;
    private float spawnDelay = 2f;
    private float spawnInterval = 1.5f;
    private int NumberEnemy = 0;

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
        Vector3 spawnLocation = new Vector3(Random.Range(-71, 71), 0 , Random.Range(-71, 71));
        if (NumberEnemy <= 15)
        {
            Instantiate(enemy[0], spawnLocation, enemy[0].transform.rotation);
            NumberEnemy++;
        }
    }
}
