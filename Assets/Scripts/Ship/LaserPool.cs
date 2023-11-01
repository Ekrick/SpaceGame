using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPool : MonoBehaviour
{
    [SerializeField] [Range (100, 1000)] private int iPoolSize = 1000;
    [SerializeField] private LaserBeam laserPrefab;
    
    private List<LaserBeam> laserPool;
    
    private void Awake()
    {
        for (int i = 0; i < iPoolSize; i++)
        {
            LaserBeam spawned = Instantiate(laserPrefab, transform.position, Quaternion.identity);
            spawned.Setup(this);
            spawned.DespawnLaser();
            laserPool.Add(spawned);
        }
    }

    public void FireLaser(Vector3 position, Quaternion rotation)
    {
        LaserBeam spawned;
        
        if (laserPool.Count <= 0)
        {
            spawned = Instantiate(laserPrefab, position, rotation);
            spawned.Setup(this);
        }
        else
        {
            spawned = laserPool[0];
            laserPool.Remove(spawned);
            spawned.transform.position = position;
            spawned.transform.rotation = rotation;
            spawned.SpawnLaser();
        }
    }
    
    public void RePool(LaserBeam laser)
    {
        laserPool.Add(laser);
    }
}
