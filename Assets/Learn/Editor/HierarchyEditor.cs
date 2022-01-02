using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Hierarchy的编辑器扩展
/// </summary>
public class HierarchyEditor
{
    [InitializeOnLoadMethod]
    static void InitializeOnLoadMethod()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowOnGUI;
            //delegate (int instanceID, Rect selectionRect)
        //{
        //    //if (Selection.activeObject && instanceID == Selection.activeObject.GetInstanceID())
        //    //{
        //    //    float width = 15f;
        //    //    float height = 15f;
        //    //    selectionRect.x += (selectionRect.width - width);
        //    //    selectionRect.width = width;
        //    //    selectionRect.height = height;
        //    //
        //    //    if (GUI.Button(selectionRect, AssetDatabase.LoadAssetAtPath<Texture>("Assets/unity.png")))
        //    //    {
        //    //        Debug.Log("click.." + Selection.activeObject.name);
        //    //    }
        //    //}
        //
        //    if(!Selection.activeObject)
        //    {
        //        float width = 15f;
        //        float height = 15f;
        //        selectionRect.x += (selectionRect.width - width);
        //        selectionRect.width = width;
        //        selectionRect.height = height;
        //
        //        if (GUI.Button(selectionRect, AssetDatabase.LoadAssetAtPath<Texture>("Assets/unity.png")))
        //        {
        //            Selection.activeGameObject.SetActive(true);
        //        }
        //    }
        //    else
        //    {
        //        float width = 15f;
        //        float height = 15f;
        //        selectionRect.x += (selectionRect.width - width);
        //        selectionRect.width = width;
        //        selectionRect.height = height;
        //        if (Selection.activeGameObject.activeSelf)
        //        {
        //            if (GUI.Button(selectionRect, AssetDatabase.LoadAssetAtPath<Texture>("Assets/unity.png")))
        //            {
        //                Selection.activeGameObject.SetActive(false);
        //            }
        //        }
        //        else
        //        {
        //            if (GUI.Button(selectionRect, AssetDatabase.LoadAssetAtPath<Texture>("Checkmark.png")))
        //            {
        //                Selection.activeGameObject.SetActive(true);
        //            }
        //        }
        //    }
        //
        //};
    }

    static void HierarchyWindowOnGUI(int instanceID, Rect selectionRect)
    {
        //Toggle控制Hierarchy上的对象显隐
        Rect rect = new Rect(selectionRect);
        rect.x += (selectionRect.width - 15f);
        rect.width = 15f;

        GameObject go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
        if (go == null) return;
        go.SetActive(GUI.Toggle(rect, go.activeSelf, string.Empty));
    }
}
