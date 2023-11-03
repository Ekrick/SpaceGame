using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    [SerializeField] [Range (1, 100)] private int iProjectileSpeed;
    [SerializeField] [Range (1, 10)] private int iLifeTime;
    [SerializeField] private LaserPool pool;
    private float fdespawnTimer = 0;
    
    void Update()
    {
        transform.position += transform.forward * iProjectileSpeed * Time.deltaTime;
        DespawnTimer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Asteroid asteroid))
        {
            asteroid.DespawnAsteroid();
        }
    }
    
    public void SpawnLaser()
    {
        fdespawnTimer = 0;
        this.gameObject.SetActive(true);
    }

    public void DespawnLaser()
    {
        pool.RePool(this);
        this.gameObject.SetActive(false);
    }

    public void Setup(LaserPool laserPool)
    {
        pool = laserPool;
        DespawnLaser();
    }

    private void DespawnTimer()
    {
        fdespawnTimer += Time.deltaTime;
        if (fdespawnTimer >= iLifeTime)
        {
            DespawnLaser();
        }
    }
}
