using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Camera Inspector 编辑器扩展
/// 在已有存在的Inspector上扩展
/// </summary>
[CanEditMultipleObjects()]
[CustomEditor(typeof(Camera))]
public class CameraInspectorEdit : Editor
{
    public override void OnInspectorGUI()
    {
        Camera cam = target as Camera;
        if (!cam)
            return;

        if(GUILayout.Button("Camera Inspector 扩展, FOV设置为30"))
        {
            cam.fieldOfView = 30f;
            Debug.Log("扩展Camera功能, FOV设置30");
        }

        base.OnInspectorGUI();
    }
}


