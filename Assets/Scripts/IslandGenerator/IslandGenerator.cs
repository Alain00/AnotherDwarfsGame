using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandGenerator : MonoBehaviour
{
    public int size = 100;
    public int resolution = 1;
    public int seed = 123456;
    public bool randomSeed = false;
    public float lowPolyEffect = 1;
    public IslandMesh islandMesh;
    public IslandMesh islandMesh2;
    public MeshCollider meshCollider;
    public CircularGradientSettings circularGradientSettings;
    public NoiseSettings noiseSettings;
    public ColorSettings colorSettings;
    public DetailsSettings detailsSettings;

    
    DetailsGenerator detailsGenerator;
    Noise noise;
    CircularGradient circularGradient;
    ColorGenerator colorGenerator;
    Vector2[][] detailPoints;

    void Awake(){
        if (randomSeed){
            seed = Random.Range(1, 123456);
        }
        GenerateMesh();
    }

    void Start(){
        detailsGenerator.GenerateDetails(detailPoints);
    }

    void OnValidate(){
        GenerateMesh();
    }

    void GenerateMesh(){
        if (resolution == 0) return;
        
        //var verticesTwo = new List<Vector3>();
        var colors = new List<Color>();
        var triangles = new List<int>();
        var trianglesTwo = new List<int>();
        var trianglesAll = new List<int>();
        var vertices = new List<Vector3>();
        var axisA = new Vector3(Vector3.up.y, Vector3.up.z, Vector3.up.x);
        var axisB = Vector3.Cross(Vector3.up, axisA);

        noise = new Noise(noiseSettings);
        circularGradient = new CircularGradient(new Vector3(size/2, 0, size/2), circularGradientSettings);
        colorGenerator = new ColorGenerator(colorSettings);
        detailsGenerator = new DetailsGenerator(detailsSettings, size, new Vector2(0,-(size*2)));
        detailPoints = detailsGenerator.ComputedPoints();
        //Debug.Log(detailPoints[0].Length);

        int i = 0;
        for (int z = 0; z < size; z++){
            for (int x = 0; x < size; x++){
                Vector3 sercent = new Vector3((float)x/size-1, 0, (float)z/size-1);
                float elevation = noise.Calculate(sercent.x + seed, sercent.z + seed) * circularGradient.Calculate(x,z);  
                float elevationNormalized = Mathf.Clamp01(elevation / (noiseSettings.rougness + circularGradientSettings.rougness));
                
                colors.Add(colorGenerator.Calculate(elevationNormalized));
                
                Vector3 point = Vector3.up + (x - .5f) * 2 * axisA + (z - .5f) * 2 * axisB;
                point.y = elevation + Random.Range(-1f, 1f) * lowPolyEffect;
                point.x += Random.Range(-1f, 1f) * lowPolyEffect;
                point.z += Random.Range(-1f, 1f) * lowPolyEffect;
                vertices.Add(point);
                //verticesTwo.Add(point);
                i++;
            }
        }

        i = 0;
        for (int z = 0; z < size; z++){
            for (int x = 0; x < size; x++){
                if (x != size - 1 && z != size - 1){
                    triangles.Add(i);
                    triangles.Add(i+size+1);
                    triangles.Add(i+size);
                   
                    // triangles.Add(i);
                    // triangles.Add(i+1);
                    // triangles.Add(i+size+1);
                    
                    trianglesTwo.Add(i);
                    trianglesTwo.Add(i+1);
                    trianglesTwo.Add(i+size+1);
                }

                i++;
            }
        }
        islandMesh.GenerateMesh(vertices.ToArray(), triangles.ToArray(), colors.ToArray());
        islandMesh2.GenerateMesh(vertices.ToArray(), trianglesTwo.ToArray(), colors.ToArray());
        
        trianglesAll.AddRange(triangles);
        trianglesAll.AddRange(trianglesTwo);
        if (!meshCollider.sharedMesh){
            meshCollider.sharedMesh = new Mesh();
        }
        var colliderMesh = meshCollider.sharedMesh;
        colliderMesh.vertices = vertices.ToArray();
        
        colliderMesh.triangles = trianglesAll.ToArray();
        colliderMesh.RecalculateNormals();
        meshCollider.sharedMesh = colliderMesh;
        
    }

    void OnDrawGizmos(){
        if (detailPoints == null || detailPoints.Length == 0) return;
        if (!detailsSettings.debug) return;

        for (int i = 0; i < detailPoints.Length; i++){
            float colorValue = 0.1f +  ((float) i / detailPoints.Length);
            Gizmos.color = new Color(colorValue, colorValue, colorValue, 1);
            foreach(Vector2 point in detailPoints[i]){
                Vector3 pos = new Vector3(point.x, detailsSettings.maxHeight + (5*i), point.y);
                Gizmos.DrawSphere(pos, 1);
                Gizmos.DrawRay(pos, Vector3.down);
            }
        }
    }
}
