using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMesh
{
    Mesh mesh;
    Mesh walls;
    int resolution;
    Vector3 position;
    float variationY;
    Vector3 axisA;
    Vector3 axisB;
    NoiseFilter noiseFilter;
    float wallHeigth;
    
    public TerrainMesh(Mesh mesh, Mesh walls, float wallHeigth, int resolution, Vector3 position, NoiseFilter noiseFilter)
    {
        this.mesh = mesh;
        this.walls = walls;
        this.resolution = resolution;
        this.position = position;
        this.noiseFilter = noiseFilter;
        this.wallHeigth = wallHeigth;

        axisA = new Vector3(Vector3.up.y, Vector3.up.z, Vector3.up.x);
        axisB = Vector3.Cross(Vector3.up, axisA);
    }

    public void ConstructMesh(){
        var vertices = new Vector3[resolution * resolution];
        var triangles = new List<int>();
        var outlineVertices = new List<int>();

        var wallVertices = new List<Vector3>();
        var wallTriangles = new List<int>();
        
        int triIndex = 0;
        for (int y = 0; y < resolution; y++){
            for (int x = 0; x < resolution; x++){
                int i = x + y * resolution;
                Vector2 persent = new Vector2(x, y);
                float xCoord = (float) x / (resolution-1) * noiseFilter.settings.scale;
                float yCoord = (float) y / (resolution-1) * noiseFilter.settings.scale;
                

                //Debug.Log(perlin);
                Vector3 pointOnTerrain = Vector3.up + (persent.x - 0.5f) * 2 * axisA + (persent.y - 0.5f) * 2 * axisB;
                var elevation = noiseFilter.CalculateElevation(xCoord, yCoord);
                pointOnTerrain.y = elevation;
                if (!noiseFilter.ShouldIncludePoint(pointOnTerrain)) continue;
                    vertices[i] = pointOnTerrain;
                if (x != resolution - 1 && y != resolution - 1){
                    
                    // if (vertices[i].y == vertices[i+resolution+1].y && vertices[i].y == vertices[i+resolution].y){
                        triangles.Add(i);
                        triangles.Add(i+resolution+1);
                        triangles.Add(i+resolution);
                    // }
                    
                    // if (vertices[i].y == vertices[i+1].y && vertices[i].y == vertices[i+resolution+1].y){
                        triangles.Add(i);
                        triangles.Add(i+1);
                        triangles.Add(i+resolution+1);
                    // }
                    //triIndex += 6;
                }
            }
        }
        // for (int y = 0; y < resolution; y++){
        //     for (int x = 0; x < resolution; x++){
        //         int i = x + y * resolution;
                
        //         // if (vertices[i].y != 0){
        //         //     continue;
        //         // };

            
        //     }
        // }


        // for(int i = 0; i < outlineVertices.Count; i++){
        //     if (i+1 >= outlineVertices.Count) continue;
        //     if (outlineVertices[i+1] >= vertices.Length) continue;
        //     int startIndex = wallVertices.Count;
        //     wallVertices.Add(vertices[outlineVertices[i]]); //left
        //     wallVertices.Add(vertices[outlineVertices[i+1]]); //right
        //     wallVertices.Add(vertices[outlineVertices[i]] - Vector3.up * wallHeigth); //bottom left
        //     wallVertices.Add(vertices[outlineVertices[i+1]] - Vector3.up * wallHeigth); //bottom right
        //     // Debug.Log(wallVertices.Count);
        //     wallTriangles.Add(startIndex + 0);
        //     wallTriangles.Add(startIndex + 2);
        //     wallTriangles.Add(startIndex + 3);

        //     wallTriangles.Add(startIndex + 3);
        //     wallTriangles.Add(startIndex + 1);
        //     wallTriangles.Add(startIndex + 0);
        //}

        walls.Clear();
        walls.vertices = wallVertices.ToArray();
        walls.triangles = wallTriangles.ToArray();
        walls.RecalculateNormals();

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
    }

    
    
}

