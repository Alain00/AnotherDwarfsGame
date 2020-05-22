using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class WaterMeshGenerator : MonoBehaviour
{
    public int VertexCount;
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
        for(int x = 0 ; x < VertexCount ; x++){
            for(int z = 0 ; z < VertexCount ; z++){
                Vertices.Add(new Vector3(-CellSize * .5f + CellSize * (x/((float)VertexCount)) ,0,-CellSize/2 + CellSize * (z/((float)VertexCount)) ));
                Normals.Add(Vector3.up);
                Uvs.Add(new Vector2( (x/((float)VertexCount)) , (float)(z/((float)VertexCount)) ) );
            }
        }
        var triangles = new List<int>();
        //Generate triangles
        for(int i = 0 ; i < VertexCount * VertexCount - VertexCount ; i++){
            if( (i + 1) % VertexCount  == 0 )
                continue;
            triangles.AddRange(new List<int>{
                i+1+VertexCount,i+VertexCount,i,
                i,i+1,i+1+VertexCount
            });    
        }

        _mesh.SetVertices(Vertices);
        _mesh.SetNormals(Normals);
        _mesh.SetUVs(0,Uvs);
        _mesh.SetTriangles(triangles,0);
        return _mesh;
    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(transform.position, new Vector3(CellSize, 1, CellSize));
    }
}
