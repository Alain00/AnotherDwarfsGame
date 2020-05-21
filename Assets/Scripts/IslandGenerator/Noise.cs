using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise : Filter
{
    NoiseSettings settings;

    public Noise(NoiseSettings settings)
    {
        this.settings = settings;
    }

    public override float Calculate(float x, float y){
        x *= settings.scale;
        y *= settings.scale;
        return Mathf.PerlinNoise(x,y) * settings.rougness;
    }
}
