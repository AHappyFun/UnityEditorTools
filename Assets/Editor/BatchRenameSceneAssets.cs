using System.Linq;
using UnityEditor;
using UnityEngine;

public class BatchRenameSceneAssets : EditorWindow
{
    public string assetPrefix, assetName, assetSuffix, startNum = "0", difits = "1";

    [MenuItem("Tools/场景资源批量重命名", priority = 11)]
    static void Open()
    {
        BatchRenameSceneAssets window = GetWindow<BatchRenameSceneAssets>();
        window.titleContent = new GUIContent("场景内资源批量重命名工具");
        window.Show();
    }
    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("资产前缀：");
        assetPrefix = GUILayout.TextField(assetPrefix);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("资产名称：");
        assetName = GUILayout.TextField(assetName);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("起始数字/数字长度：");
        startNum = GUILayout.TextField(startNum);
        difits = GUILayout.TextField(difits);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("资产后缀：");
        assetSuffix = GUILayout.TextField(assetSuffix);
        EditorGUILayout.EndHorizontal();

        EditorGUI.BeginDisabledGroup(Selection.gameObjects.Length == 0);
        if (GUILayout.Button("重命名"))
        {

            if (!int.TryParse(startNum, out int m_StartNum))
            {
                ShowNotification(new GUIContent("起始数字不是数字，以0开始"));
            }

            if (!int.TryParse(difits, out int m_difits))
            {
                ShowNotification(new GUIContent("起始数字不是数字，以0开始"));
            }

            foreach (GameObject go in Selection.gameObjects.OrderBy(_ => _.transform.GetSiblingIndex()))
            {
                go.name = (assetPrefix != string.Empty ? assetName + '_' : string.Empty) + (assetName != string.Empty ? assetName + '_' : string.Empty) + m_StartNum.ToString().PadLeft(m_difits, '0') + (assetSuffix != string.Empty ? '_' + assetSuffix : string.Empty);
                m_StartNum++;
            }
        }
        EditorGUI.EndDisabledGroup();

    }
}

