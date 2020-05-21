using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailsGenerator
{
    DetailsSettings settings;
    List<GameObject[]> detailsPlaced;
    float regionSize;
    Vector2 origin;

    public DetailsGenerator(DetailsSettings settings, float regionSize, Vector2 origin)
    {
        this.settings = settings;
        this.regionSize = regionSize;
        this.origin = origin;
        detailsPlaced = new List<GameObject[]>();
    }

    public Vector2[][] ComputedPoints(){
        if (settings.layers.Length == 0) return null;
        Vector2[][] points = new Vector2[settings.layers.Length][];
        int i = 0;
        foreach (DetailsSettings.Layer layer in settings.layers){
            points[i] = GeneratePoints(layer.radius, layer.numSamplesBeforeRejection);
            i++;
        }
        return points;
    }

    public int FindLayer(float height){
        for (int i = 0; i < settings.layers.Length; i++){
            if (height >= settings.layers[i].minHeight && height <= settings.layers[i].maxHeight){
                return i;
            }
        }
        return -1;
    }

    public Vector2[] GeneratePoints(float radius, float numSamplesBeforeRejection){
        Vector2 sampleRegionSize = new Vector2(regionSize * 2, regionSize * 2);

        float cellSize = radius/Mathf.Sqrt(2);

		int[,] grid = new int[Mathf.CeilToInt(sampleRegionSize.x/cellSize), Mathf.CeilToInt(sampleRegionSize.y/cellSize)];
		List<Vector2> points = new List<Vector2>();
		List<Vector2> spawnPoints = new List<Vector2>();

		spawnPoints.Add(origin + sampleRegionSize/2);
		while (spawnPoints.Count > 0) {
			int spawnIndex = Random.Range(0,spawnPoints.Count);
			Vector2 spawnCentre = spawnPoints[spawnIndex];
			bool candidateAccepted = false;

			for (int i = 0; i < numSamplesBeforeRejection; i++)
			{
				float angle = Random.value * Mathf.PI * 2;
				Vector2 dir = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
				Vector2 candidate = spawnCentre + dir * Random.Range(radius, 2*radius);
				if (IsValid(candidate, sampleRegionSize, cellSize, radius, points, grid)) {
					points.Add(candidate);
					spawnPoints.Add(candidate);
					grid[(int)((candidate.x - origin.x)/cellSize),(int)((candidate.y - origin.y)/cellSize)] = points.Count;
					candidateAccepted = true;
					break;
				}
			}
			if (!candidateAccepted) {
				spawnPoints.RemoveAt(spawnIndex);
			}

		}
        return points.ToArray();
    }

    public void GenerateDetails(Vector2[][] points){
        if (!Application.isPlaying) return;
        for (int i = 0; i < points.Length; i++){
            if (settings.layers[i].prefabs.Length == 0) continue;
            foreach (Vector2 point in points[i]){
                Vector3 rayOrigin = new Vector3(point.x, settings.maxHeight, point.y);
                RaycastHit raycastHit;
                if (Physics.Raycast(rayOrigin, Vector3.down, out raycastHit, settings.maxHeight*2)){
                    Vector3 candidate = raycastHit.point;
                    if (candidate.y >= settings.layers[i].minHeight && candidate.y <= settings.layers[i].maxHeight){
                        DetailsSettings.Layer layer = settings.layers[i];
                        GameObject toPlace = layer.prefabs[Random.Range(0, layer.prefabs.Length)];
                        GameObject.Instantiate(toPlace, candidate, Quaternion.identity);
                    }
                }
            }
        }
    }

    bool IsValid(Vector2 candidate, Vector2 sampleRegionSize, float cellSize, float radius, List<Vector2> points, int[,] grid) {
		if (candidate.x >= origin.x && candidate.x < origin.x+sampleRegionSize.x && candidate.y >= origin.y && candidate.y < origin.y + sampleRegionSize.y) {
			int cellX = (int)((candidate.x - origin.x)/cellSize);
			int cellY = (int)((candidate.y - origin.y)/cellSize);
			int searchStartX = Mathf.Max(0,cellX -2);
			int searchEndX = Mathf.Min(cellX+2,grid.GetLength(0)-1);
			int searchStartY = Mathf.Max(0,cellY -2);
			int searchEndY = Mathf.Min(cellY+2,grid.GetLength(1)-1);

			for (int x = searchStartX; x <= searchEndX; x++) {
				for (int y = searchStartY; y <= searchEndY; y++) {
					int pointIndex = grid[x,y]-1;
					if (pointIndex != -1) {
						float sqrDst = (candidate - points[pointIndex]).sqrMagnitude;
						if (sqrDst < radius*radius) {
							return false;
						}
					}
				}
			}
			return true;
		}
		return false;
	}
}
