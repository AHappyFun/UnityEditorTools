using System.IO;
using UnityEditor;
using UnityEngine;

//用于创建项目模块
//打开一个对话框，输入模块Name，生成一个包含Shader、Scripts、Materials、Textures、Model的文件夹

public class CreateMoudleFloder : EditorWindow
{

    private new string name;

    private void OnGUI()
    {
        name = EditorGUILayout.TextField(name, GUILayout.Width(170));
        if (GUILayout.Button("创建"))
        {
            CreateMoudleFloderWithName(name);
        }
    }

    [MenuItem("Assets/Create/创建一个工作模块", false, 1)]
    private static void CreateMoudle()
    {
        GetWindow<CreateMoudleFloder>("创建工作模块");
    }

    private static void CreateMoudleFloderWithName(string name)
    {
        string moduleString = "Assets/" + name;
        if (!Directory.Exists(moduleString))
        {
            Directory.CreateDirectory(moduleString);

            Directory.CreateDirectory(Path.Combine(moduleString, "Shader"));
            Directory.CreateDirectory(Path.Combine(moduleString, "Scripts"));
            Directory.CreateDirectory(Path.Combine(moduleString, "Textures"));
            Directory.CreateDirectory(Path.Combine(moduleString, "Model"));
            Directory.CreateDirectory(Path.Combine(moduleString, "Materials"));
        }
        AssetDatabase.Refresh();
        Debug.Log("Create Module：" + name + " Successed..");
    }

}
