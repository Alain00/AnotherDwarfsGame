using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorGenerator
{
    ColorSettings settings;

    public ColorGenerator(ColorSettings settings)
    {
        this.settings = settings;
    }

    public Color Calculate(float yNormalized){
        Color color = settings.gradient.Evaluate(yNormalized);
        //color += new Color(Random.Range(0.0f,0.1f), Random.Range(0.0f,0.1f), Random.Range(0.0f,0.1f));
        return color;
    }
}
