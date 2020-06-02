using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterNoise : MonoBehaviour
{
    private MeshFilter Mfilter;
    public float Xoffset;
    public float Zoffset;
    public float power;
    public float waveSpeed;
    public float Scale;
    void Start()
    {
        Mfilter = GetComponent<MeshFilter>();
        
    }
    void Update()
    {
        MakeNoise();
        Xoffset += Time.deltaTime * waveSpeed;
        Zoffset += Time.deltaTime * waveSpeed;
    }
    void MakeNoise(){
        Vector3[] Vertices = Mfilter.mesh.vertices;
        for(int i = 0 ; i < Vertices.Length ; i++){
            Vertices[i].y = CalculateHeight(Vertices[i].x , Vertices[i].z) * power;
        }
        Mfilter.mesh.SetVertices(Vertices);
    }

    float CalculateHeight(float xPos , float zPos){
        xPos = xPos * Scale + Xoffset; 
        zPos = zPos * Scale + Zoffset; 
        
        return Mathf.PerlinNoise(xPos,zPos);
    }
}
