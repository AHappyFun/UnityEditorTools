using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

public class ClearConsoleLog 
{
    [MenuItem("Tools/使用反射Clean日志", false, 2)]
    public static void ClearLog()
    {
        Assembly assembly = Assembly.GetAssembly(typeof(Editor));

        MethodInfo method = assembly.GetType("UnityEditor.LogEntries").GetMethod("Clear");
        method.Invoke(null,null);
    }
}
