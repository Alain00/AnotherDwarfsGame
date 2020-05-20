using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoiseSettings
{
   public float scale = 20;
   public float strength = 11;
   [Range(0f,1f)]
   public float minY = 0.2f;
   [Range(0f,1f)]
   public float maxY = 0.5f;
   public float maxHeigth = 50;
   [Range(1f, 50f)]
   public float cutoffStrength = 2f;
   public float cutoffHeigth = 40;
}
