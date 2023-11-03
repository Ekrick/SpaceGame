using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private Image hpBar;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private ShipStats playerShip;
    private float fScore = 0;

    private void Update()
    {
        fScore += Time.deltaTime;
        UpdateScore();
        
        hpBar.fillAmount = playerShip.GetHealthFloat();
    }

    public void UpdateScore()
    {
       String scoreString = "" + Mathf.RoundToInt(fScore);
       scoreText.text = scoreString;
    }
}
