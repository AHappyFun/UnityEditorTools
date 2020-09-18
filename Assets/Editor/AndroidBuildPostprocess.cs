using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Callbacks;
using System.IO;
using System.Threading;
#endif

/// <summary>
/// Obb 自动重命名
/// </summary>
public static class AndroidBuildPostprocess
{
#if UNITY_EDITOR
    [PostProcessBuild(999)]
    public static void OnPostProcessBuild(BuildTarget target, string pathToBuiltProject)
    {
        if(target != BuildTarget.Android || PlayerSettings.Android.useAPKExpansionFiles == false)
        {
            return;
        }

        WaitBuildSuccess(Directory.GetParent(pathToBuiltProject).FullName);
    }

    static void WaitBuildSuccess(string pathToBuiltProject)
    {
        if (Directory.Exists(pathToBuiltProject))
        {
            DirectoryInfo direction = new DirectoryInfo(pathToBuiltProject);
            FileInfo[] files = direction.GetFiles("*", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Name.EndsWith(".obb"))
                {
                    string sourceName = files[i].FullName;
                    string obbName = "main." + PlayerSettings.Android.bundleVersionCode +"."+ Application.identifier + ".obb";
                    string destName = Path.Combine(pathToBuiltProject, obbName);
                    Directory.Move(sourceName, destName);
                    Debug.Log("Obb Change Name:" + sourceName +" To " + destName);
                }
            }
        }
    }
#endif
}
