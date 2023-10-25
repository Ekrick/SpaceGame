using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private Image hpBar;
    [SerializeField] private ShipStats playerShip;

    private void Update()
    {
        hpBar.fillAmount = playerShip.GetHealthFloat();
    }
}
