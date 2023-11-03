using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Jobs;
using UnityEngine;
using Unity.Collections;

[BurstCompile]
public struct AsteroidJob : IJob
{
    private Vector3 asteroidPosition;
    private Vector3 targetDestination;
    private float fSpeed;
    private float fDeltaTime;
    private float fDistance;
    private Vector3 shipPosition;

    private NativeArray<Vector3> positionArray;
    private NativeArray<float> distanceArray;
    
    public AsteroidJob(Vector3 position, Vector3 destination, float speed, float deltaTime, Vector3 targetPosition, float distance, NativeArray<Vector3> positions, NativeArray<float> distances)
    {
        asteroidPosition = position;
        targetDestination = destination;
        fSpeed = speed;
        fDeltaTime = deltaTime;
        shipPosition = targetPosition;
        fDistance = distance;
        positionArray = positions;
        distanceArray = distances;
    }
    
    public void Execute()
    {
        MoveAsteroid();
        CheckCollision();
    }

    private void MoveAsteroid()
    {
        Vector3 positionChange = (targetDestination - asteroidPosition).normalized * fSpeed * fDeltaTime;
        asteroidPosition += positionChange;

        positionArray[0] = asteroidPosition;
    }

    private void CheckCollision()
    {
        fDistance = (shipPosition - asteroidPosition).magnitude;
        distanceArray[0] = fDistance;
    }
}
