using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    [SerializeField] [Range (1, 100)] private int iProjectileSpeed;
    private LaserPool pool;
    void Update()
    {
        transform.position += transform.forward * iProjectileSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Asteroid asteroid;
        Debug.Log("Trigger!");
        // if (other.CompareTag("Asteroid"))
        // {
        //     Destroy(other.gameObject);
        //     Destroy(this.gameObject);
        // }
        if (other.TryGetComponent(out asteroid))
        {
            asteroid.DespawnAsteroid();
        }
    }
    
    public void SpawnLaser()
    {
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
    }
}
