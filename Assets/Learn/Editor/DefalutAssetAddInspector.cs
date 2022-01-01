using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// unity默认资源的Inspector扩展
/// </summary>
/// 
[CustomEditor(typeof(UnityEditor.SceneAsset))]
//[CustomEditor(typeof(UnityEditor.MonoScript))]
public class DefalutAssetAddInspector : Editor
{
    public override void OnInspectorGUI()
    {


        string path = AssetDatabase.GetAssetPath(target);
        GUI.enabled = true;

        if (path.EndsWith(".unity"))
        {
            GUILayout.Button("我是场景");
        }else if (path.EndsWith(".cs"))
        {
            GUILayout.Button("我是代码");
        }

        base.OnInspectorGUI();
    }
}
