using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject[] enemy;
    private float spawnDelay = 2f;
    private float spawnInterval = 1.5f;
    private int EnemyLimit = 25;
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
        for (int i = 0; i < EnemyLimit; i++)
        {
            Instantiate(enemy[0], RandomSpawn(), enemy[0].transform.rotation);
            NumberEnemy++;
        }
        InvokeRepeating("SpawnEnemy", spawnDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnEnemy()
    {
        if (NumberEnemy < EnemyLimit)
        {
            
            Instantiate(enemy[0], RandomSpawn(), enemy[0].transform.rotation);
            NumberEnemy++;
        }
    }

    Vector3 RandomSpawn()
    {
        NumberPoint = Random.Range(1, 6);
        if (NumberPoint == 1)
        {
            spawnLocation = new Vector3(Random.Range(10, 30), 0, Random.Range(-70, 71));
        }
        else if (NumberPoint == 2)
        {
            spawnLocation = new Vector3(Random.Range(-70, 71), 0, Random.Range(10, 30));
        }
        else if (NumberPoint == 3)
        {
            spawnLocation = new Vector3(Random.Range(-30, -10), 0, Random.Range(-70, 71));
        }
        else if (NumberPoint == 4)
        {
            spawnLocation = new Vector3(Random.Range(-70, 71), 0, Random.Range(-30, -10));
        }

        return spawnLocation;
    }
}
