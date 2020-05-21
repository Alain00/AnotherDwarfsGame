using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularGradient : Filter
{
    Vector3 center;
    CircularGradientSettings settings;

    public CircularGradient(Vector3 center, CircularGradientSettings settings)
    {
        this.center = center;
        this.settings = settings;
    }

    public override float Calculate(float x, float z){
        Vector3 pos = new Vector3(x,center.y,z);
        Vector3 direction = pos - center;
        float distance = direction.magnitude;
        float normalized = distance / settings.radius;
        normalized *= normalized;
        return Mathf.Max(0, 1 - normalized) * settings.rougness;
    }
}
