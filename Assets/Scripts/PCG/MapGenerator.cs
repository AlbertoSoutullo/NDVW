using System;
using UnityEngine;
using System.Collections;
using UnityEngine.AI;


public class MapGenerator : MonoBehaviour {

    public int mapWidth;
    public int mapHeight;

    public TerrainData terrainData;
    public NoiseData noiseData;
    public TextureData textureData;
    public PrefabsData prefabsData;

    public NavMeshSurface surface;

    public Material terrainMaterial;

    public void Start()
    {
        MeshData mesh = GenerateMap();
        GeneratePrefabs(mesh);
        GenerateNavMesh();
    }

    public MeshData GenerateMap() {
        float[,] noiseMap = Noise.GenerateNoiseMap (mapWidth, mapHeight, noiseData.seed, noiseData.noiseScale, noiseData.octaves, noiseData.persistance, noiseData.lacunarity, noiseData.offset);
        MapDisplay display = FindObjectOfType<MapDisplay> ();
        textureData.UpdateMeshHeights (terrainMaterial, terrainData.minHeight, terrainData.maxHeight);
        textureData.ApplyToMaterial(terrainMaterial);
        MeshData meshData = MeshGenerator.GenerateTerrainMesh(noiseMap, terrainData.meshHeightMultiplier, terrainData.meshHeightCurve);
        display.DrawMesh (meshData);
        return meshData;
    }

    public void GeneratePrefabs(MeshData mesh)
    {
        // loop through all triangles and select the positions to where instantiate prefabs
        int[] triangles = mesh.triangles;
        Vector3[] positions = new Vector3[triangles.Length / 3];
        int count = 0;
        for (int i = 0; i < triangles.Length; i += 3)
        {
            // only selecting the first vertex for each triangle (greedy approach)
            positions[count] = mesh.vertices[triangles[i]];
            ++count;
        }
        
        // determine where to instantiate the prefabs
        PrefabsInternalData prefabsInternalData = PrefabsGenerator.DeterminePrefabsPositions(mapWidth, mapHeight, terrainData.meshHeightMultiplier, positions, prefabsData);
        
        // instantiate the prefabs
        MapDisplay display = FindObjectOfType<MapDisplay> ();
        display.InstantiatePrefabs(prefabsInternalData);
    }

    public void GenerateNavMesh()
    {
        surface.BuildNavMesh();
    }

    private void OnValidate() {
        if (mapWidth < 1) 
        {
            mapWidth = 1;
        }
        if (mapHeight < 1) 
        {
            mapHeight = 1;
        }
    }
}
