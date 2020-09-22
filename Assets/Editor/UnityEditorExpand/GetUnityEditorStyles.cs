using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

/// <summary>
/// 通过反射展示所有GUI Styles
/// </summary>
public class GetUnityEditorStyles : EditorWindow
{
    static List<GUIStyle> styles = null;

    [MenuItem("Tools/ShowAllGUIStyles")]
    public static void ShowAllStyles()
    {
        EditorWindow.GetWindow<GetUnityEditorStyles>("所有GUI Styles");

        styles = new List<GUIStyle>();
        foreach (PropertyInfo item in typeof(EditorStyles).GetProperties(BindingFlags.Static|BindingFlags.Public|BindingFlags.NonPublic))
        {
            object o = item.GetValue(null, null);
            if(o.GetType() == typeof(GUIStyle))
            {
                styles.Add(o as GUIStyle);
            }
        }
    }

    public Vector2 scrollPos = Vector2.zero;
    private void OnGUI()
    {
        scrollPos = GUILayout.BeginScrollView(scrollPos);
        for (int i = 0; i < styles.Count; i++)
        {
            GUILayout.Label("EditorStyles." + styles[i].name, styles[i]);
        }
        GUILayout.EndScrollView();
    }
}
