using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueChangeScript : MonoBehaviour
{
    public string name = "123";
    public int num = 10;

    //脚本上Reset按钮触发
    private void Reset()
    {
        name = "123";
        num = 10;
    }

    //脚本中序列化值在Inspector上发生改变
    private void OnValidate()
    {
        Debug.Log("值发生变化");
    }
}
