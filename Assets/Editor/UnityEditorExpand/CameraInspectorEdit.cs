using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Inspector 编辑器扩展
/// </summary>
[CustomEditor(typeof(Camera))]
public class CameraInspectorEdit : Editor
{
    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("Camera Inspector 扩展"))
        {
            Debug.Log("可以在我这里做一些事情，扩展Camera功能");
        }

        base.OnInspectorGUI();
    }
}
