using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [Range(2,256)]
    public int resolution = 10;
    public float wallHeigth = 10;
    public int subdivisions = 1;
    public NoiseSettings noiseSettings;
    public ColorSettings colorSettings;
    ColorGenerator colorGenerator;
    TerrainMesh[] terrainMeshes;
    [SerializeField]
    public MeshFilter[] meshFilters;
    MeshFilter walls;
    Vector2[] faceDirections;
    NoiseFilter noiseFilter;
    
    void Start(){
        Initialize();
        GenerateMesh();
    }

    void Initialize(){
        Vector2[] faceDirections = new Vector2[4];
        faceDirections[0] = new Vector2(0,0);
        faceDirections[1] = new Vector2(1,0);
        faceDirections[2] = new Vector2(0,1);
        faceDirections[3] = new Vector2(1,1);

        noiseFilter = new NoiseFilter(noiseSettings);
        colorGenerator = new ColorGenerator(colorSettings);
        
        var facesResolution = resolution / subdivisions;
        var facesCount = subdivisions;
        terrainMeshes = new TerrainMesh[facesCount];
        if (meshFilters == null || meshFilters.Length == 0 || meshFilters.Length != facesCount)
            meshFilters = new MeshFilter[facesCount];
        Vector3 facePosition = transform.position;
        
        
        for (int i = 0; i < facesCount; i++){
            if (meshFilters[i] == null || walls == null){
                GameObject meshObj = new GameObject("mesh");
                GameObject wallObj = new GameObject("wall");
                meshObj.transform.parent = transform;
                wallObj.transform.parent = transform;
                meshObj.AddComponent<MeshRenderer>();
                wallObj.AddComponent<MeshRenderer>();
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                walls = wallObj.AddComponent<MeshFilter>();
                walls.sharedMesh = new Mesh();
                meshFilters[i].sharedMesh = new Mesh();
            }
            meshFilters[i].GetComponent<MeshRenderer>().sharedMaterial = colorSettings.mainMaterial;
            walls.GetComponent<MeshRenderer>().sharedMaterial = colorSettings.mainMaterial;

            Vector2 currentDirection = faceDirections[i%4];
            facePosition = new Vector3(facesResolution * currentDirection.x, 0, facesResolution * currentDirection.y);
            terrainMeshes[i] = new TerrainMesh(meshFilters[i].sharedMesh, walls.sharedMesh, wallHeigth, facesResolution, facePosition, noiseFilter);
        }
    }

    void GenerateMesh(){
        foreach(TerrainMesh face in terrainMeshes){
            face.ConstructMesh();
        }
        if (meshFilters != null && meshFilters.Length > 0)
        colorGenerator.UpdateElevation(noiseFilter.elevationMinMax, meshFilters);
    }
}
