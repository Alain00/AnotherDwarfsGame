using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandGenerator : MonoBehaviour
{
    public int size = 100;
    public int resolution = 1;
    public float lowPolyEffect = 1;
    public MeshFilter meshFilter;
    public CircularGradientSettings circularGradientSettings;
    public NoiseSettings noiseSettings;
    public ColorSettings colorSettings;
    
    
    Noise noise;
    CircularGradient circularGradient;
    ColorGenerator colorGenerator;


    void OnValidate(){
        GenerateMesh();
    }

    void GenerateMesh(){
        if (resolution == 0) return;
        var vertices = new List<Vector3>();
        var colors = new List<Color>();
        var triangles = new List<int>();

        var axisA = new Vector3(Vector3.up.y, Vector3.up.z, Vector3.up.x);
        var axisB = Vector3.Cross(Vector3.up, axisA);

        noise = new Noise(noiseSettings);
        circularGradient = new CircularGradient(new Vector3(size/2, 0, size/2), circularGradientSettings);
        colorGenerator = new ColorGenerator(colorSettings);

        for (int z = 0; z < size; z++){
            for (int x = 0; x < size; x++){
                Vector3 sercent = new Vector3((float)x/size-1, 0, (float)z/size-1);
                float elevation = noise.Calculate(sercent.x, sercent.z) * circularGradient.Calculate(x,z);  
                float elevationNormalized = Mathf.Clamp01(elevation / (noiseSettings.rougness + circularGradientSettings.rougness));
                colors.Add(colorGenerator.Calculate(elevationNormalized));
                Vector3 point = Vector3.up + (x - .5f) * 2 * axisA + (z - .5f) * 2 * axisB;
                point.y = elevation + Random.Range(-1f, 1f) * lowPolyEffect;
                vertices.Add(point);
            }
        }

        int i = 0;
        for (int z = 0; z < size; z++){
            for (int x = 0; x < size; x++){
                if (x != size - 1 && z != size - 1){
                    triangles.Add(i);
                    triangles.Add(i+size+1);
                    triangles.Add(i+size);
                    triangles.Add(i);
                    triangles.Add(i+1);
                    triangles.Add(i+size+1);
                }

                i++;
            }
        }
        Mesh mesh;
        if (!meshFilter.sharedMesh){
            mesh = new Mesh();
            meshFilter.sharedMesh = mesh;
        }
        mesh = meshFilter.sharedMesh;
        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.colors = colors.ToArray();
        mesh.RecalculateNormals();
    }
}
