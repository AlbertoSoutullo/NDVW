using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu()]
public class PrefabsData : ScriptableObject
{
    public AnimationCurve noiseImportance;
    public Prefab[] prefabs;

    [System.Serializable]
    public class Prefab
    {
        public String name;
        public List<Transform> transforms;
        public NoiseData noise;
        public AnimationCurve heightImportance;
    }
}
