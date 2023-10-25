using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] [Range (0.1f, 2f)] private float fImpactRadius = 1;
    private ShipStats ship;

    public void SetShip(ShipStats playerShip)
    {
        ship = playerShip;
    }

    void Update()
    {
        transform.position += transform.forward * Time.deltaTime;
        
        CheckShipRange();
    }

    private void CheckShipRange()
    {
        Vector3 distanceVector = ship.transform.position - transform.position;
        if (distanceVector.magnitude <= fImpactRadius)
        {
            ship.TakeDamage();
            Destroy(this.gameObject);
        }
    }
}
