using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidManager : MonoBehaviour
{
    [SerializeField] private ShipStats playerShip;
    [SerializeField] private Asteroid asteroidPrefab;
    [SerializeField] [Range (10, 100)] private int iSpawnRadius = 10;
    [SerializeField] [Range (100, 100000)] private int iPoolSize = 10000;
    
    
    [SerializeField] private int iSpawnInterval = 2;
    private float fSpawnTimer = 0;
    private int iSpawnCount = 0;

    private List<Asteroid> asteroidPool;

    private void Awake()
    {
        for (int i = 0; i < iPoolSize; i++)
        {
            Asteroid spawned = Instantiate(asteroidPrefab, transform.position, Quaternion.identity);
            spawned.Setup(playerShip, this);
            spawned.DespawnAsteroid();
            asteroidPool.Add(spawned);
        }
    }

    void Update()
    {
        fSpawnTimer += Time.deltaTime;
        if (fSpawnTimer >= iSpawnInterval)
        {
            iSpawnCount++;
            
            for (int i = 0; i < iSpawnCount; i++)
            {
                SpawnAtRandomLocationOnCircle();
            }

            fSpawnTimer = 0;
        }
    }

    void SpawnAtRandomLocationOnCircle()
    {
        Asteroid spawned;
        float posAngle = Random.Range(0f, 6.28f);

        Vector3 spawnPos = new Vector3(Mathf.Sin(posAngle) * iSpawnRadius, 0, Mathf.Cos(posAngle) * iSpawnRadius);

        if (asteroidPool.Count <= 0)
        {
            spawned = Instantiate(asteroidPrefab, spawnPos, Quaternion.identity);
            spawned.Setup(playerShip, this);
        }
        else
        {
            spawned = asteroidPool[0];
            spawned.transform.position = spawnPos;
            spawned.SpawnAsteroid();
            asteroidPool.Remove(spawned);
        }
        spawned.transform.LookAt(playerShip.transform.position);
    }

    public void RePool(Asteroid asteroid)
    {
        asteroidPool.Add(asteroid);
    }
}
