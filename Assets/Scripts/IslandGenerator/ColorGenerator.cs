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
        return settings.gradient.Evaluate(yNormalized);
    }
}
