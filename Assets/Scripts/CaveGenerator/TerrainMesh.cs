using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMesh : MonoBehaviour
{
    Mesh mesh;
    int resolution;
    Vector3 position;


    public void ConstructMesh(){
        var vertices = new Vector3[resolution * resolution];
        var triangles = new int[(resolution - 1) * (resolution - 1) * 6];

        for (int y = 0; y < resolution; y++){
            for (int x = 0; x < resolution; x++){
                int i = x + y * resolution;
                Mathf.PerlinNoise(x + position.x, y + position.z);
                vertices[i] = 
            }
        }
    }
}
