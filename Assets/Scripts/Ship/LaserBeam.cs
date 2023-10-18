using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    [SerializeField] [Range (1, 100)] private int iProjectileSpeed;
    void Start()
    {
        
    }

    void Update()
    {
        transform.position += transform.forward * iProjectileSpeed * Time.deltaTime;
    }
}
