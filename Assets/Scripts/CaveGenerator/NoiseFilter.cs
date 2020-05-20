using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseFilter
{
    public NoiseSettings settings;
    public MinMax elevationMinMax;

    public NoiseFilter(NoiseSettings settings)
    {
        this.settings = settings;
        elevationMinMax = new MinMax();
    }

    public bool ShouldIncludePoint(Vector3 point){
        return point.y <= settings.cutoffHeigth;
    }

    public float CalculateElevation(float x, float y){
        float perlin = Mathf.PerlinNoise(x, y);
        
        float elevation = perlin * settings.strength;
        if (perlin > settings.maxY) elevation = settings.maxHeigth;
        if (perlin < settings.minY) elevation = 0;
        elevationMinMax.AddValue(elevation);
        return elevation;
    }

    public Color CalculateColor(float x, float y)
    {
        float perlin = Mathf.PerlinNoise(x, y);
        if (perlin <= settings.minY) return Color.grey;
        return Color.green;
    }
}
