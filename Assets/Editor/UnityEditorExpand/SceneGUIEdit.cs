using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// 场景里的辅助UI
/// </summary>
[CustomEditor(typeof(Camera))]
public class SceneGUIEdit : Editor
{
    private void OnSceneGUI()
    {
        Camera cam = target as Camera;
        if (cam)
        {
            Handles.color = Color.red;
            Handles.Label(cam.transform.position, cam.transform.position.ToString());
            Handles.BeginGUI();
            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("click"))
            {
                Debug.Log("SSS");
            }
            GUILayout.Label("Test Lable");
            Handles.EndGUI();
        }
    }

    //常驻场景UI
    [InitializeOnLoadMethod]
    static void AlwaysSceneUI()
    {
        SceneView.onSceneGUIDelegate += (sceneView) =>
        {
            Handles.BeginGUI();

            GUI.Label(new Rect(0f,0f,40f,40f), "Scene常驻UI");
            GUI.DrawTexture(new Rect(0f, 50f, 150, 100), AssetDatabase.LoadAssetAtPath<Texture>("Assets/Resources/Textures/zed.jpg"));

            Handles.EndGUI();
        };
    }
}
