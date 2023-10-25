using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ShipStats : MonoBehaviour
{
    [SerializeField] [Range (1, 10)] private int iMaxHealth;
    private int iCurrentHealth = 0;
    void Start()
    {
        iCurrentHealth = iMaxHealth;
    }

    public void TakeDamage()
    {
        iCurrentHealth--;
        Debug.Log("You Got Hit");
        if (iCurrentHealth <= 0)
        {
            SceneManager.LoadScene(2);
        }
    }

    public float GetHealthFloat()
    {
        return (float)iCurrentHealth / iMaxHealth;
    }
}
