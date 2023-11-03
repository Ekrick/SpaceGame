using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Unity.Jobs;

public class Asteroid : MonoBehaviour
{
    [SerializeField] [Range (0.1f, 2f)] private float fImpactRadius = 1;
    [SerializeField] [Range (0.1f, 2f)] private float fSpeed = 2;
    [SerializeField] private float fDistance;

    private Vector3 destination;
    
    private NativeArray<Vector3> positionArray;
    private NativeArray<float> distanceArray;

    private JobHandle jobHandle;
    
    [SerializeField] private ShipStats ship;
    private AsteroidManager asteroidManager;

    private void OnEnable()
    {
        fDistance = 1000;
        if (ship != null)
        {
            destination = ship.transform.position;
        }
        positionArray = new NativeArray<Vector3>(1, Allocator.Persistent);
        positionArray[0] = transform.position;
        distanceArray = new NativeArray<float>(1, Allocator.Persistent);
        distanceArray[0] = fDistance;
    }

    void Update()
    {
        if (ship != null)
        {
            AsteroidJob job = new AsteroidJob(transform.position, destination, fSpeed, Time.deltaTime, ship.transform.position, fDistance, positionArray, distanceArray);

            jobHandle = job.Schedule();
        }
    }

    private void LateUpdate()
    {
        jobHandle.Complete();

        transform.position = positionArray[0];
        fDistance = distanceArray[0];
        
        if (fDistance <= fImpactRadius)
        {
            ship.TakeDamage();
            DespawnAsteroid();
        }
    }

    private void OnDisable()
    {
        positionArray.Dispose();
        distanceArray.Dispose();
    }
    
    // private void OnDestroy()
    // {
    //     positionArray.Dispose();
    //     distanceArray.Dispose();
    // }

    public void Setup(ShipStats playerShip, AsteroidManager manager)
    {
        ship = playerShip;
        asteroidManager = manager;
        destination = playerShip.transform.position;
        DespawnAsteroid();
    }


    public void SpawnAsteroid()
    {
        this.gameObject.SetActive(true);
        transform.LookAt(ship.transform.position);
    }

    public void DespawnAsteroid()
    {
        asteroidManager.RePool(this);
        this.gameObject.SetActive(false);
    }
    
    
    // private void CheckShipRange()
    // {
    //     Vector3 distanceVector = ship.transform.position - transform.position;
    //     if (distanceVector.magnitude <= fImpactRadius)
    //     {
    //         ship.TakeDamage();
    //         Destroy(this.gameObject);
    //     }
    // }
}
