using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// 脚本上下文扩展  
/// </summary>
public class ScriptContextEdit 
{
    [MenuItem("CONTEXT/Component/打印Name")]
    public static void PrintName(MenuCommand command)
    {
        Debug.Log("Target Name:  "+ command.context.name);
    }
}
