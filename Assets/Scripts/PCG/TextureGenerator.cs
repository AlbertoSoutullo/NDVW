using UnityEngine;
using System.Collections;

public static class TextureGenerator {

    public static Texture2D TextureFromColourMap(Color[] colourMap, int width, int height) {
        Texture2D texture = new Texture2D (width, height);
        texture.filterMode = FilterMode.Bilinear;  // or Point for better defined
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.SetPixels (colourMap);
        texture.Apply ();
        return texture;
    }

}