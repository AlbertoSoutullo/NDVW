using System;
using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {

    public int mapWidth;
    public int mapHeight;

    public TerrainData terrainData;
    public NoiseData noiseData;
    public TextureData textureData;

    public Material terrainMaterial;

    public void Start()
    {
        GenerateMap();
    }

    public void GenerateMap() {
        float[,] noiseMap = Noise.GenerateNoiseMap (mapWidth, mapHeight, noiseData.seed, noiseData.noiseScale, noiseData.octaves, noiseData.persistance, noiseData.lacunarity, noiseData.offset);
        MapDisplay display = FindObjectOfType<MapDisplay> ();
        textureData.UpdateMeshHeights (terrainMaterial, terrainData.minHeight, terrainData.maxHeight);
        textureData.ApplyToMaterial(terrainMaterial);
        display.DrawMesh (MeshGenerator.GenerateTerrainMesh (noiseMap, terrainData.meshHeightMultiplier, terrainData.meshHeightCurve));
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
