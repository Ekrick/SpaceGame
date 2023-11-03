using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPool : MonoBehaviour
{
    [SerializeField] [Range (10, 200)] private int iPoolSize = 1000;
    [SerializeField] private LaserBeam laserPrefab;
    
    private List<LaserBeam> laserPool;
    
    private void Awake()
    {
        laserPool = new List<LaserBeam>();

    }

    private void Start()
    {
        FillPool();
    }

    public void FireLaser(Vector3 position, Quaternion rotation)
    {
        if (laserPool.Count <= 0)
        {
            LaserBeam spawned = Instantiate(laserPrefab, position, rotation);
            spawned.Setup(this);
        }
        else
        {
            LaserBeam beam = laserPool[laserPool.Count - 1];
            laserPool.RemoveAt(laserPool.Count - 1);
            if (beam != null)
            {
                beam.transform.position = position;
                beam.transform.rotation = rotation;
                beam.SpawnLaser();
            }
        }
    }
    
    public void RePool(LaserBeam laser)
    {
        laserPool.Add(laser);
    }

    private void FillPool()
    {
        for (int i = 0; i < iPoolSize; i++)
        {
            LaserBeam spawned = Instantiate(laserPrefab, transform.position, Quaternion.identity);
            spawned.Setup(this);
            laserPool.Add(spawned);
        }
    }
}
