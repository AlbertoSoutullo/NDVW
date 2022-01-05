using UnityEngine;
using System.Collections;

public class MapDisplay : MonoBehaviour {

    public Renderer textureRender;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    public MeshCollider meshCollider;

    public void DrawMesh(MeshData meshData) {
        Mesh mesh = meshData.CreateMesh ();
        meshFilter.sharedMesh = mesh;
        meshCollider.sharedMesh = mesh;
    }

}