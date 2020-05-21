using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailsGenerator
{
    DetailsSettings settings;
    List<GameObject[]> detailsPlaced;

    public DetailsGenerator(DetailsSettings settings)
    {
        this.settings = settings;
        detailsPlaced = new List<GameObject[]>();
    }

    public int FindLayer(float height){
        for (int i = 0; i < settings.layers.Length; i++){
            if (height >= settings.layers[i].minHeight && height <= settings.layers[i].maxHeight){
                return i;
            }
        }
        return -1;
    }

    public void Generate(Vector3 point){
        int layerIndex = FindLayer(point.y);
        if (layerIndex == -1) return;
        DetailsSettings.Layer layer = settings.layers[layerIndex];
        // layer
    }
}
