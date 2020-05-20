using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorGenerator
{
    ColorSettings settings;
    const int textureSize = 512;
	const TextureFormat textureFormat = TextureFormat.RGB565;


    public ColorGenerator(ColorSettings colorSettings)
    {
        this.settings = colorSettings;
    }

    public void UpdateElevation(MinMax elevationMinMax, MeshFilter[] filters){
        settings.mainMaterial.SetFloat("minHeight", elevationMinMax.Min);
        settings.mainMaterial.SetFloat("maxHeight",  elevationMinMax.Max);
        settings.mainMaterial.SetInt ("layerCount", settings.layers.Count);
        settings.mainMaterial.SetColorArray ("baseColours", settings.layers.ConvertAll(x => x.tint).ToArray());
        settings.mainMaterial.SetFloatArray ("baseStartHeights", settings.layers.ConvertAll(x => x.startHeight).ToArray());
        settings.mainMaterial.SetFloatArray ("baseBlends", settings.layers.ConvertAll(x => x.blendStrength).ToArray());
        settings.mainMaterial.SetFloatArray ("baseColourStrength", settings.layers.ConvertAll(x => x.tintStrength).ToArray());
        settings.mainMaterial.SetFloatArray ("baseTextureScales", settings.layers.ConvertAll(x => x.textureScale).ToArray());
        Texture2DArray texturesArray = GenerateTextureArray (settings.layers.ConvertAll (x => x.texture).ToArray ());
		settings.mainMaterial.SetTexture ("baseTextures", texturesArray);
        // Debug.Log(filters.Length);
        // foreach(MeshFilter filter in filters){
        //     Mesh mesh = filter.sharedMesh;
        //     Vector3[] vertices = mesh.vertices;
        //     Color[] colors = new Color[vertices.Length];
        //     for(int i = 0; i < vertices.Length; i++){
        //         colors[i] = settings.gradient.Evaluate(Mathf.Abs(vertices[i].y) / Mathf.Abs(elevationMinMax.Max));
        //     }
        //     mesh.colors = colors;
        // }
    }

    Texture2DArray GenerateTextureArray(Texture2D[] textures) {
		Texture2DArray textureArray = new Texture2DArray (textureSize, textureSize, textures.Length, textureFormat, true);
		for (int i = 0; i < textures.Length; i++) {
			textureArray.SetPixels (textures [i].GetPixels (), i);
		}
		textureArray.Apply ();
		return textureArray;
	}
}
