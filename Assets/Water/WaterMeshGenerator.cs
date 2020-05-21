using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class WaterMeshGenerator : MonoBehaviour
{
    public int GridSize;
    public float CellSize;
    private MeshFilter Mfilter;
    void Start()
    {
        Mfilter = GetComponent<MeshFilter>();
        Mfilter.mesh = GenerateMesh();
    }

    public Mesh GenerateMesh(){
        Mesh _mesh = new Mesh();
        List<Vector3>Vertices = new List<Vector3>();
        List<Vector3>Normals = new List<Vector3>();
        List<Vector2>Uvs = new List<Vector2>();
        for(int x = 0 ; x < GridSize ; x++){
            for(int z = 0 ; z < GridSize ; z++){
                Vertices.Add(new Vector3(-CellSize * .5f + CellSize * (x/((float)GridSize)) ,0,-CellSize/2 + CellSize * (z/((float)GridSize)) ));
                Normals.Add(Vector3.up);
                Uvs.Add(new Vector2( (x/((float)GridSize)) , (float)(z/((float)GridSize)) ) );
            }
        }
        var triangles = new List<int>();
        //Generate triangles
        for(int i = 0 ; i < GridSize * GridSize - GridSize ; i++){
            if( (i + 1) % GridSize  == 0 )
                continue;
            triangles.AddRange(new List<int>{
                i+1+GridSize,i+GridSize,i,
                i,i+1,i+1+GridSize
            });    
        }

        _mesh.SetVertices(Vertices);
        _mesh.SetNormals(Normals);
        _mesh.SetUVs(0,Uvs);
        _mesh.SetTriangles(triangles,0);
        return _mesh;
    }
}
