using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ColorSettings
{
    public Color mainColor;
    public Material mainMaterial;
    public Gradient gradient;

    public List<Layer> layers;
}
