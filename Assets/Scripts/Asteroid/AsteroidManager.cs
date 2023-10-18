using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    
    [SerializeField] private Asteroid asteroidPrefab;
    [SerializeField] private int count = 0;
    void Start()
    {
        
    }

    void Update()
    {
        SpawnAtRandomLocation();
    }

    void SpawnAtRandomLocation()
    {
        float x = Random.Range(-100, 100);
        float z = Random.Range(-100, 100);

        Vector3 spawnPos = new Vector3(x, 0, z);

        Asteroid spawned = Instantiate(asteroidPrefab, spawnPos, Quaternion.identity);

        spawned.transform.LookAt(Vector3.zero);
        count++;
    }
}
