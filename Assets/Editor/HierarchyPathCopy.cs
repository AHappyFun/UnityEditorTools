using UnityEditor;
using UnityEngine;

public class HierarchyPathCopy : Editor
{

    [MenuItem("GameObject/拷贝路径 %&#C")]
    static void GetRelativePath()
    {
        string path = string.Empty;
        if (Selection.gameObjects.Length == 1)
        {
            GameObject current = Selection.gameObjects[0];
            path = GetGameObjectPath(current);
            GUIUtility.systemCopyBuffer = path;
            Debug.Log($"已拷贝完整路径“{path}”到剪切板");
            return;
        }
        else if (Selection.gameObjects.Length == 2)
        {
            string a = GetGameObjectPath(Selection.gameObjects[0]);
            string b = GetGameObjectPath(Selection.gameObjects[1]);

            if (a.StartsWith(b))
            {
                path = a.Remove(0, b.Length - Selection.gameObjects[1].name.Length);
            }
            else if (b.StartsWith(a))
            {
                path = b.Remove(0, a.Length - Selection.gameObjects[1].name.Length);
            }
            else
            {
                Debug.Log("两个Gameobject没有父子关系");
                return;
            }

            GUIUtility.systemCopyBuffer = path;
            Debug.Log($"已拷贝“{path}”到剪切板");
        }
        else if (Selection.gameObjects.Length > 2)
        {
            Debug.Log("已选择的Gameobject大于两个，无法查找");
        }

    }

    static string GetGameObjectPath(GameObject go)
    {
        string path = go.name;

        while (go.transform.parent)
        {
            go = go.transform.parent.gameObject;
            path = go.transform.name + "/" + path;
        }
        return path;
    }
}
