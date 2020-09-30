using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// 导入模型，自动进行资源配置
/// </summary>
public class CustomModelImporter : AssetPostprocessor
{
    void OnPreprocessModel()
    {
        ModelImporter modelImporter = assetImporter as ModelImporter;

        modelImporter.animationType = ModelImporterAnimationType.Generic;

        modelImporter.importMaterials = false;
    }

    void OnPostprocessModel(GameObject g)
    {
        // PrefabUtility.CreatePrefab
        Debug.Log(g.name);
    }
}
