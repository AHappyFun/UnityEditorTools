using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CustomValueScript))]
public class CustomValueScriptInspector : Editor
{
    #region 绘制方法1
    /*
    private CustomValueScript m_target => target as CustomValueScript;

    public override void OnInspectorGUI()
    {
        m_target.intValue = EditorGUILayout.IntSlider(new GUIContent("Int Silder"), m_target.intValue, 0, 10);
        //m_target.intValue = EditorGUILayout.IntField("int value", m_target.intValue);

        m_target.stringValue = EditorGUILayout.TextField("string value", m_target.stringValue);

        m_target.floatValue = EditorGUILayout.Slider("float silder", m_target.floatValue, 0, 10f);
        //m_target.floatValue = EditorGUILayout.FloatField("float value", m_target.floatValue);

        m_target.boolValue = EditorGUILayout.Toggle("bool value", m_target.boolValue);

        m_target.vec3Value = EditorGUILayout.Vector3Field("vec3 value", m_target.vec3Value);

        m_target.textureValue = (Texture)EditorGUILayout.ObjectField("texture value", m_target.textureValue, typeof(Texture), true);

        m_target.colValue = EditorGUILayout.ColorField("col value", m_target.colValue);


    }
    */
    #endregion

    #region 绘制方法2
    //序列化属性
    private SerializedProperty intValue;
    private SerializedProperty floatValue;
    private SerializedProperty stringValue;
    private SerializedProperty boolValue;
    private SerializedProperty vec3Value;
    private SerializedProperty textureValue;
    private SerializedProperty colValue;

    private void OnEnable()
    {
        intValue = serializedObject.FindProperty("intValue");
        floatValue = serializedObject.FindProperty("floatValue");
        stringValue = serializedObject.FindProperty("stringValue");
        boolValue = serializedObject.FindProperty("boolValue");
        vec3Value = serializedObject.FindProperty("vec3Value");
        textureValue = serializedObject.FindProperty("textureValue");
        colValue = serializedObject.FindProperty("colValue");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(intValue);
        EditorGUILayout.PropertyField(floatValue);
        EditorGUILayout.PropertyField(stringValue);
        EditorGUILayout.PropertyField(boolValue);
        EditorGUILayout.PropertyField(vec3Value);
        EditorGUILayout.PropertyField(textureValue);
        EditorGUILayout.PropertyField(colValue);
        serializedObject.ApplyModifiedProperties();
    }

    #endregion
}
