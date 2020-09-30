using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CustomTextureImporter : AssetPostprocessor
{
    /// <summary>
    /// 导入Texture之前，设置平台压缩格式
    /// </summary>
    void OnPreprocessTexture()
    {
        TextureImporter textureImporter = assetImporter as TextureImporter;

        if (textureImporter.assetPath.Contains("Textures"))
        {
            //设置plan1
            /*
            textureImporter.textureType = TextureImporterType.Default;
            textureImporter.mipmapEnabled = true;
            
            textureImporter.SetPlatformTextureSettings("Standalone", 2048, TextureImporterFormat.DXT5);
            textureImporter.SetPlatformTextureSettings("iPhone", 2048, TextureImporterFormat.ASTC_RGBA_4x4, 100, true);
            textureImporter.SetPlatformTextureSettings("Android", 2048, TextureImporterFormat.ETC_RGB4,true);
            */

            //设置plan2
            TextureImporterPlatformSettings settings = textureImporter.GetPlatformTextureSettings("Android");
            if(settings.format != TextureImporterFormat.RGBA32 && settings.format != TextureImporterFormat.ETC2_RGBA8)
            {
                textureImporter.SetPlatformTextureSettings("Android", 2048, TextureImporterFormat.ETC2_RGBA8, true);
            }
        }
    }

    [MenuItem("Assets/手动设置贴图格式", false, -1)]
    static void SetTextureFormat()
    {
        if(Selection.assetGUIDs.Length > 0)
        {
            AssetImporter importer = AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(Selection.activeObject));

            if(importer is TextureImporter)
            {
                (importer as TextureImporter).SetPlatformTextureSettings("Standalone", 2048, TextureImporterFormat.RGBA32, true);
                importer.SaveAndReimport();
            }
        }
    }
}
