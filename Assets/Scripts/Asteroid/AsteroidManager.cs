using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    [SerializeField] private ShipStats playerShip;
    [SerializeField] private Asteroid asteroidPrefab;
    [SerializeField] [Range (10, 100)] private int iSpawnRadius = 10;

    [SerializeField] private int iSpawnInterval = 2;
    private float fSpawnTimer = 0;
    private int iSpawnCount = 0;

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
        float posAngle = Random.Range(0f, 6.28f);

        Vector3 spawnPos = new Vector3(Mathf.Sin(posAngle) * iSpawnRadius, 0, Mathf.Cos(posAngle) * iSpawnRadius);

        Asteroid spawned = Instantiate(asteroidPrefab, spawnPos, Quaternion.identity);
        spawned.SetShip(playerShip);
        spawned.transform.LookAt(playerShip.transform.position);
    }
}
