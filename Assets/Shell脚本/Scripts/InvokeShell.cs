using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

/// <summary>
/// C# 调用Shell
/// </summary>
public class InvokeShell
{

    [MenuItem("Tools/调用Copy Shell")]
    static void CopyShell()
    {
        string shell = Path.Combine(Application.dataPath, "Shell脚本", "Shell", "copy.sh");
        string arg1 = Path.Combine(Application.dataPath, "Resources", "Textures", "zed.jpg");
        string arg2 = Path.Combine(Application.dataPath, "Resources", "Textures", "zed2.jpg");

        string args = shell + " " + arg1 + " " + arg2;

        System.Diagnostics.Process.Start("/bin/bash", args);
    }
}
