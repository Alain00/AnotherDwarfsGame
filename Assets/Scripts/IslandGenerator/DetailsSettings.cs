using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DetailsSettings
{
    [System.Serializable]
    public class Layer{
        public GameObject[] prefabs;
        public float minHeight;
        public float maxHeight;
    }

    public Layer[] layers;
    

}
