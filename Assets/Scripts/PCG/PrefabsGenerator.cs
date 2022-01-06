using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class PrefabsGenerator {

    public static PrefabsInternalData DeterminePrefabsPositions(int mapWidth, int mapHeight, float heightMultiplier, Vector3[] positions, PrefabsData prefabsData) {
        System.Random random = new System.Random();
        
        AnimationCurve noiseImportance = prefabsData.noiseImportance;
        PrefabsData.Prefab[] prefabs = prefabsData.prefabs;
        
        // create the noise maps for all prefabs
        List<float[,]> noiseMaps = new List<float[,]>();
        List<Transform>[] prefabsTransforms = new List<Transform>[prefabs.Length];
        String[] transformsNames = new String[prefabs.Length];
        for (int i = 0; i < prefabs.Length; ++i)
        {
            float[,] noiseMap = Noise.GenerateNoiseMap (
                mapWidth, 
                mapHeight, 
                prefabs[i].noise.seed, 
                prefabs[i].noise.noiseScale, 
                prefabs[i].noise.octaves, 
                prefabs[i].noise.persistance, 
                prefabs[i].noise.lacunarity, 
                prefabs[i].noise.offset);
            noiseMaps.Add(noiseMap);
            prefabsTransforms[i] = prefabs[i].transforms;
            transformsNames[i] = prefabs[i].name;
        }
        
        // create the internal data object for display everything later on
        PrefabsInternalData prefabsInternalData = new PrefabsInternalData(positions, prefabsTransforms, transformsNames);

        // loop all positions and determine whether to instantiate the prefabs
        for (int i = 0; i < positions.Length; ++i)
        {
            for (int j = 0; j < prefabs.Length; ++j)
            {
                Vector3 position = positions[i];
                PrefabsData.Prefab prefab = prefabs[j];
                float[,] noiseMap = noiseMaps[j];
                
                // weighted sum between the position height and the noise of the prefab
                int positionInNoiseMapX = (int) Math.Round(position[0] - (noiseMap.GetLength (0) - 1) / -2f);
                int positionInNoiseMapZ = (int) Math.Round((noiseMap.GetLength (1) - 1) / 2f - position[2]);
                float positionY = position[1] / heightMultiplier;

                float heightProbability = prefab.heightImportance.Evaluate(positionY);
                float noiseProbability = noiseImportance.Evaluate(noiseMap[positionInNoiseMapX, positionInNoiseMapZ]);

                // sample true or false with the given probability
                double randomNumber = random.NextDouble();
                if (randomNumber < heightProbability & randomNumber < noiseProbability)
                {
                    prefabsInternalData.AddPositionPrefabs(i, j);
                }
            }
        }

        return prefabsInternalData;
    }
}

public class PrefabsInternalData
{
    public Vector3[] positions;
    public List<Transform>[] transforms;
    public String[] transformsNames;
    public List<Tuple<int, int>> prefabsPositions;

    public PrefabsInternalData(Vector3[] positions, List<Transform>[] transforms, String[] transformsNames)
    {
        this.positions = positions;
        this.transforms = transforms;
        this.transformsNames = transformsNames;
        this.prefabsPositions = new List<Tuple<int, int>>();
    }

    public void AddPositionPrefabs(int positionIndex, int prefabsIndex)
    {
        this.prefabsPositions.Add(new Tuple<int, int>(positionIndex, prefabsIndex));
    }

}