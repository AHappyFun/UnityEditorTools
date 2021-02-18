using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
//using Miscellaneous;
//using Sirenix.Utilities;
using UnityEditor;
using UnityEditor.Experimental.AssetImporters;
using UnityEngine;
using Object = UnityEngine.Object;

/// <summary>
/// 自动修改特定路径下Mat的Shader
/// </summary>
public class AutoChangeMatShader : AssetPostprocessor
{

    //static readonly AssetProcessor[] assetProcessors =
    //    {
    //        new DefaultProcessor(),
    //        new EffectProcessor(),
    //        new ModelProcessor(),
    //        new SceneProcessor(),
    //        new ShaderProcessor(),
    //        new AnimatorProcessor(),
    //        new UIImageProcessor(),
    //        new UILayoutProcessor(),
    //        new ConfigAssetProcessor(),
    //        new PlayableProcessor(),
    //    };

    //public void ResetAllAssetBundleName()
    //{
    //    foreach (var assetPath in AssetDatabase.GetAllAssetPaths())
    //    {
    //        ResetAssetBundleName(assetPath);
    //    }
    //}

    private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        //var iterAssets = importedAssets.Union(movedAssets);
        //foreach (var assetPath in iterAssets)
        //{
        //    ResetAssetBundleName(assetPath);
        //    foreach (var processor in assetProcessors)
        //    {
        //        if (processor.Filter(assetPath))
        //        {
        //            processor.DoProcessor(assetPath);
        //            break;
        //        }
        //    }
        //}
        // trick (lixiaolu)
        string path = "Resources/Materials"; //需要替换的路径
        foreach (string asset in importedAssets)
        {
            
            if (asset.EndsWith(".mat") && asset.Contains(path))
            {
                OnPostprocessMaterial(AssetDatabase.LoadAssetAtPath<Material>(asset));
            }
           // asset => asset.EndsWith(".mat")).ForEach(material => OnPostprocessMaterial(AssetDatabase.LoadAssetAtPath<Material>(material))

        }
       // importedAssets.Where(asset => asset.EndsWith(".mat")).ForEach(material => OnPostprocessMaterial(AssetDatabase.LoadAssetAtPath<Material>(material)));
    }
    //private static void ResetAssetBundleName(string assetPath)
    //{
    //    if (!assetPath.StartsWith("Assets/GameAssets/"))
    //    {
    //        if (!assetPath.EndsWith(".cs"))
    //        {
    //            var importer = AssetImporter.GetAtPath(assetPath);
    //            importer.assetBundleName = null;
    //            importer.userData = null;
    //        }
    //        return;
    //    }
    //    foreach (var processor in assetProcessors)
    //    {
    //        if (processor.Filter(assetPath))
    //        {
    //            AssetImporter.GetAtPath(assetPath).assetBundleName = processor.GenerateAssetBundleName(assetPath);
    //            return;
    //        }
    //    }
    //    AssetImporter.GetAtPath(assetPath).assetBundleName = null;
    //}

    static void OnPostprocessMaterial(Material material)
    {
        var shader = Shader.Find("FX/Flare");     //要替换成的Shader
        var discards = new List<string>     //如果监测到有这些shader，都会替换成上面的shader
        {
            "Standard",
            "Standard (Specular setup)",
            "Standard (Roughness setup)",
            "Custom/SceneObject",
            "Custom/SceneObject (Specular setup)"
        };
        if (discards.Any(s => s.Equals(material.shader.name)))
        {
            Debug.Log(string.Format("replace shader {0} to 我的SHADER", material.shader.name));
            material.shader = shader;
        }
        if (material.shader.name.Contains("InternalErrorShader"))
        {
            Debug.Log(string.Format("material [{0}] shader error: InternalErrorShader", material.name));
        }
    }

    //void OnPostprocessTexture(Texture2D texture)
    //{
    //    string configPath = "Assets/Editor/Miscellaneous/AssetProcessor/ResourcesPostProcessingConfig.asset";
    //    ResourcesPostProcessingConfig rppConfig =
    //        AssetDatabase.LoadAssetAtPath<ResourcesPostProcessingConfig>(configPath);

    //    List<string> specialPaths = new List<string>();
    //    if (rppConfig != null)
    //    {
    //        for (int i = 0; i < rppConfig.SpecialPaths.Length; i++)
    //        {
    //            specialPaths.Add(rppConfig.SpecialPaths[i].DirPath.Replace("\\", "/"));
    //        }
    //    }

    //    bool isInSpecialDir = false;
    //    for (int i = 0; i < specialPaths.Count; i++)
    //    {
    //        if (assetPath.Contains(specialPaths[i]))
    //        {
    //            isInSpecialDir = true;
    //            break;
    //        }
    //    }

    //    var inSceneDir = assetPath.Contains("Assets/GameAssets/Scenes");
    //    var inModelDir = assetPath.Contains("Assets/GameAssets/Models");
    //    var inPlayerDir = assetPath.Contains("Assets/GameAssets/Models/Player");
    //    var isTerrainControl = inSceneDir && assetPath.Contains("/Terrains/");
    //    var inGroundDir = assetPath.Contains("Assets/GameAssets/Scenes/Sce_Models/Common/Groudmap");
    //    var inBossDir = assetPath.Contains("Assets/GameAssets/Models/Monster/BOSS");
    //    var importer = assetImporter as TextureImporter;
    //    if (!isInSpecialDir && !isTerrainControl && (inSceneDir || inModelDir))
    //    {
    //        if (importer != null)
    //        {
    //            importer.isReadable = false;
    //            var lightmapDir = new Regex("Lightmap-(\\d+)_comp_dir.png");
    //            var isLigthmapDir = lightmapDir.IsMatch(Path.GetFileName(assetPath));
    //            var lightmapBase = new Regex("Lightmap-(\\d+)_comp_light.exr");
    //            var isLightmapColor = lightmapBase.IsMatch(Path.GetFileName(assetPath));
    //            var lightmapShadowMask = new Regex("Lightmap-(\\d+)_comp_shadowmask.png");
    //            var isLightmapShadowMask = lightmapShadowMask.IsMatch(Path.GetFileName(assetPath));

    //            var isLightmap = isLigthmapDir || isLightmapColor || isLightmapShadowMask;
    //            var medium = inPlayerDir || isLightmapColor || inGroundDir || inBossDir;
    //            importer.mipmapEnabled = (inSceneDir && !isLightmap) || inPlayerDir || inBossDir;
    //            var platforms = new List<string> { "iPhone", "Android", "Standalone" };
    //            platforms.ForEach(platform =>
    //            {
    //                var setting = new TextureImporterPlatformSettings
    //                {
    //                    name = platform,
    //                    overridden = true,
    //                    maxTextureSize = isLightmap ? 1024 : (medium ? 512 : 256),
    //                    crunchedCompression = true,
    //                    textureCompression = TextureImporterCompression.Compressed
    //                };
    //                importer.SetPlatformTextureSettings(setting);
    //            });
    //        }
    //    }

    //    if (isTerrainControl)
    //    {
    //        importer.isReadable = false;
    //        var platforms = new List<string> { "iPhone", "Standalone" };
    //        platforms.ForEach(platform =>
    //        {
    //            var settings = new TextureImporterPlatformSettings
    //            {
    //                name = platform,
    //                overridden = true,
    //                maxTextureSize = 512,
    //                crunchedCompression = true,
    //                textureCompression = TextureImporterCompression.Compressed
    //            };
    //            importer.SetPlatformTextureSettings(settings);
    //        });

    //        var androidSetting = new TextureImporterPlatformSettings
    //        {
    //            name = "Android",
    //            overridden = true,
    //            maxTextureSize = 512,
    //            crunchedCompression = false,
    //            textureCompression = TextureImporterCompression.Uncompressed
    //        };
    //        importer.SetPlatformTextureSettings(androidSetting);
    //    }

    //    var ui = "Assets/GameAssets/UI/SpriteSheet";
    //    if (assetPath.Contains(ui))
    //    {
    //        importer.isReadable = false;
    //        var platforms = new List<string> { "iPhone", "Android", "Standalone" };
    //        platforms.ForEach(platform =>
    //        {
    //            var setting = new TextureImporterPlatformSettings
    //            {
    //                name = platform,
    //                overridden = true,
    //                textureCompression = TextureImporterCompression.Compressed
    //            };
    //            importer.SetPlatformTextureSettings(setting);
    //        });
    //    }
    //}

    //void OnPostprocessModel(GameObject o)
    //{
    //    var importer = assetImporter as ModelImporter;
    //    importer.importMaterials = true;
    //    importer.materialLocation = ModelImporterMaterialLocation.External;
    //    var path = "Assets/GameAssets/Materials/default.mat";
    //    var mat = new[] { AssetDatabase.LoadAssetAtPath<Material>(path) };
    //    Renderer[] renders = o.GetComponentsInChildren<Renderer>();
    //    for (int i = 0; i < renders.Length; i++)
    //    {
    //        for (int j = 0; j < renders[i].sharedMaterials.Length; j++)
    //        {
    //            renders[i].sharedMaterials[j] = mat[0];
    //        }
    //    }
    //}

    //[MenuItem("Build/AssetBundle/CheckNamesForSelection")]
    //public static void CheckNamesForSelection()
    //{
    //    var selectedAsset = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.DeepAssets);

    //    for (var i = 0; i < selectedAsset.Length; i++)
    //    {
    //        var item = selectedAsset[i];
    //        var path = AssetDatabase.GetAssetPath(item.GetInstanceID());
    //        EditorUtility.DisplayProgressBar("Check Asset Bundle Names", path, (float)i / selectedAsset.Length);

    //        if (Directory.Exists(path))
    //            continue;

    //        if (path.StartsWith("Assets/GameAssets/")
    //            && !path.Contains("Main.unity")
    //            && !path.Contains("DlgFactory.unity"))
    //        {
    //            var importer = AssetImporter.GetAtPath(path);
    //            if (string.IsNullOrEmpty(importer.assetBundleName))
    //            {
    //                Debug.LogError(path + " is not set AssetBundle Name. You must reimport the asset.");
    //            }
    //        }
    //    }
    //    EditorUtility.ClearProgressBar();
    //    Debug.Log("CheckNamesForSelection Finished.");
    //}

    //[MenuItem("Build/AssetBundle/ClearBundleNameForSelection")]
    //public static void ClearBundleNameForSelection()
    //{
    //    var selectedAsset = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.DeepAssets);
    //    for (var i = 0; i < selectedAsset.Length; i++)
    //    {
    //        var item = selectedAsset[i];
    //        var path = AssetDatabase.GetAssetPath(item.GetInstanceID());
    //        EditorUtility.DisplayProgressBar("Clear Bundle Names", path, (float)i / selectedAsset.Length);
    //        var importer = AssetImporter.GetAtPath(path);
    //        if (importer)
    //        {
    //            try
    //            {
    //                importer.assetBundleName = string.Empty;
    //            }
    //            catch (Exception ex)
    //            {
    //            }
    //        }
    //    }
    //    EditorUtility.ClearProgressBar();
    //    Debug.Log("ClearBundleNameForSelection Finished.");
    //    AssetDatabase.RemoveUnusedAssetBundleNames();
    //}

    //[MenuItem("Build/Textures/InitDefaultConfig")]
    //public static void InitDefaultTextureConfig()
    //{
    //    var selectedAsset = Selection.GetFiltered(typeof(UnityEngine.Texture), SelectionMode.DeepAssets);
    //    for (int i = 0; i < selectedAsset.Length; i++)
    //    {
    //        Texture2D tex = selectedAsset[i] as Texture2D;
    //        TextureImporter ti = (TextureImporter)TextureImporter.GetAtPath(AssetDatabase.GetAssetPath(tex));
    //        var scene = "Assets/GameAssets/Scenes";
    //        var model = "Assets/GameAssets/Models";
    //        var path = ti.assetPath;
    //        if (path.Contains(scene) || path.Contains(model))
    //        {
    //            if (ti != null)
    //            {
    //                var lightmapDir = new Regex("Lightmap-(\\d+)_comp_dir.png");
    //                var lightmapBase = new Regex("Lightmap-(\\d+)_comp_light.exr");
    //                var lightmapShadowMask = new Regex("Lightmap-(\\d+)_comp_shadowmask.png");

    //                var usedForLightmap = lightmapDir.IsMatch(Path.GetFileName(path)) ||
    //                                      lightmapBase.IsMatch(Path.GetFileName(path)) ||
    //                                      lightmapShadowMask.IsMatch(Path.GetFileName(path));
    //                var uncompress = lightmapDir.IsMatch(Path.GetFileName(path));
    //                var platforms = new List<string> { "iPhone", "Android", "Standalone" };
    //                platforms.ForEach(platform =>
    //                {
    //                    var setting = new TextureImporterPlatformSettings
    //                    {
    //                        name = platform,
    //                        overridden = true,
    //                        maxTextureSize = usedForLightmap ? 1024 : 512,
    //                        crunchedCompression = true,
    //                        textureCompression = uncompress
    //                            ? TextureImporterCompression.Uncompressed
    //                            : TextureImporterCompression.Compressed
    //                    };
    //                    ti.SetPlatformTextureSettings(setting);
    //                });
    //                AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(tex));
    //            }

    //        }
    //    }

    //}

}