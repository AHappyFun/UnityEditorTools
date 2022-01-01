using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//Scene的拓展基于对象，也就是必须在Hierarchy中选择一个对象。
//选择不同的对象，显示不同的Scene拓展。
//也可以有常驻的拓展

/// <summary>
/// Camera辅助UI
/// </summary>
[CustomEditor(typeof(Camera))]
public class CameraSceneEditor : Editor
{
    private void OnSceneGUI()
    {
        Camera cam = target as Camera;
        if (cam)
        {
        }
    }

    //常驻场景UI
    [InitializeOnLoadMethod]
    static void AlwaysSceneUI()
    {
        SceneView.onSceneGUIDelegate += (sceneView) =>
        {
            Handles.BeginGUI();
            Handles.color = Color.blue;
            GUI.Label(new Rect(0f,0f,40f,40f), "Scene常驻UI");
            GUI.DrawTexture(new Rect(0f, 50f, 150, 100), AssetDatabase.LoadAssetAtPath<Texture>("Assets/Resources/Textures/zed.jpg"));

            Handles.EndGUI();
        };
    }
}


[CustomEditor(typeof(Transform))]
public class TransformSceneEditor : Editor
{

    private void OnSceneGUI()
    {
        Transform trans = target as Transform;
        if (trans)
        {
            Handles.color = Color.blue;
            Handles.Label(trans.position, "Pos: " + trans.position.ToString());

            Handles.BeginGUI();

            GUILayout.BeginArea(new Rect(0, Screen.height - 200, 200, 200));
            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("我是Scene拓展按钮", GUILayout.Width(100)))
            {
                Debug.Log("！！！我是Scene拓展按钮！！！");
            }
            GUILayout.Label("我是Scene拓展Label");
            GUILayout.EndArea();

            Handles.EndGUI();
        }
    }


}
