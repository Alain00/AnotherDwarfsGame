using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarProgress : MonoBehaviour
{
    PlayerStats playerStats;
    RectTransform BarSize;
    void Start()
    {
        playerStats = GameObject.FindObjectOfType<PlayerStats>();
        BarSize = GetComponent<RectTransform>();
    }

    
    void Update()
    {
        int CurSize = Mathf.RoundToInt(playerStats.CurHealth / playerStats.MaxHealth * 100);
        CurSize = 100 - CurSize;
        BarSize.sizeDelta = new Vector2 (BarSize.sizeDelta.x , CurSize);
    }
}
