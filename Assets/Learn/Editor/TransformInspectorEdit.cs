using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;


/// <summary>
/// 拓展现有Transform Inspector的编辑器，需要使用反射调用Editor内部的DLL方法
/// </summary>
[CustomEditor(typeof(Transform))]
public class TransformInspectorEdit : Editor
{
    private Editor _edit;
    private void OnEnable()
    {
        _edit = Editor.CreateEditor(target, Assembly.GetAssembly(typeof(Editor)).GetType("UnityEditor.TransformInspector" , true));
    }

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("拓展Transform"))
        {
            Debug.Log("这里可以拓展Transform的Inspector方法哦");
        }
        //base.OnInspectorGUI();
        _edit.OnInspectorGUI();  //使用DLL里的原始方法
    }
}
