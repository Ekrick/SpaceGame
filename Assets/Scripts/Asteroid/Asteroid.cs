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
    
    private NativeArray<Vector3> positionArray;
    private NativeArray<float> distanceArray;

    private JobHandle jobHandle;
    
    private ShipStats ship;
    private AsteroidManager asteroidManager;

    private void OnEnable()
    {
        fDistance = 1000;
        positionArray = new NativeArray<Vector3>(1, Allocator.Persistent);
        positionArray[0] = transform.position;
        distanceArray = new NativeArray<float>(1, Allocator.Persistent);
        distanceArray[0] = fDistance;
    }

    void Update()
    {
        // transform.position += transform.forward * Time.deltaTime;
        //
        // CheckShipRange();

        AsteroidJob job = new AsteroidJob(transform.position, transform.rotation, fSpeed, Time.deltaTime, ship.transform.position, fDistance, positionArray, distanceArray);

        jobHandle = job.Schedule();
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
    }

    public void LogDistance()
    {
        Debug.Log(fDistance);
    }

    public void SpawnAsteroid()
    {
        this.gameObject.SetActive(true);
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
