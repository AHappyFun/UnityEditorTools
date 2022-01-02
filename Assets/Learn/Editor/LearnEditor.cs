using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// 正常一个类写编辑器，测试各个回调方法时机
/// </summary>
public class LearnEditor : EditorWindow
{
    [MenuItem("Tools/LearnEditorWindow")]
    static void CreateLearnEditor()
    {
        Rect rect = new Rect(0, 0, 500, 500);
        EditorWindow.GetWindowWithRect<LearnEditor>(rect, true, "LearnEditor", true);
    }

    //--------API测试-------
    private void Awake()
    {
        
    }

    private string txt;
    private Animation anim;
    private Texture texture;

    Rect drag;
    private void OnGUI()
    {
        txt = EditorGUILayout.TextField("输入文字：", txt);

        if(GUILayout.Button("通知", GUILayout.Width(200)))
        {
            this.ShowNotification(new GUIContent("This is a Notifcation"));
        }
        if(GUILayout.Button("关闭通知", GUILayout.Width(200)))
        {
            this.RemoveNotification();
        }

        EditorGUILayout.LabelField("Mouse Pos", Event.current.mousePosition.ToString());

        texture = EditorGUILayout.ObjectField("选择图片", texture, typeof(Texture), true) as Texture;

        if(GUILayout.Button("关闭窗口", GUILayout.Width(200)))
        {
            this.Close();
        }

        EditorGUILayout.BeginVertical("GroupBox");
        {
            drag = EditorGUILayout.GetControlRect(GUILayout.Height(60));
            EditorGUI.LabelField(drag, "拖动到这里");

            if (drag.Contains(Event.current.mousePosition))
            {
                if (Event.current.type == EventType.DragUpdated)
                {
                    //Debug.Log("正在拖拽");
                    DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                }
                else if (Event.current.type == EventType.DragPerform)
                {
                    Debug.Log("拖拽放下");
                    foreach (var item in DragAndDrop.paths)
                    {
                        Debug.Log(item);
                    }
                    Object[] objs = DragAndDrop.objectReferences;
                    foreach (var item in objs)
                    {
                        Debug.Log(item.name);
                    }
                }
            }
        }

    }

    private void Update()
    {
        
    }

    private void OnFocus()
    {
        Debug.Log("聚焦到当前板子");
    }

    private void OnLostFocus()
    {
        Debug.Log("板子失焦");
    }

    private void OnHierarchyChange()
    {
        Debug.Log("Hierarchy里任何物体发生变化");
    }

    private void OnInspectorUpdate()
    {
        //Debug.Log("InspectorUpdate");
        this.Repaint(); //这里重绘可以刷新MousePos信息
    }

    private void OnSelectionChange()
    {
        foreach (Transform t in Selection.transforms)
        {
            Debug.Log( "SelectionChange： " +  t.name);
        }
    }

    private void OnDestroy()
    {
        Debug.Log("窗口关闭");
    }
}
