using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;

public class AutoCreateSkinPlugin : EditorWindow
{
    [MenuItem("Skin代码自动生成工具/打开工具")]
	public static void ShowWindow()
    {
        GetWindow<AutoCreateSkinPlugin>("Skin代码自动生成工具");
    }
    public List<DataStruct> data = new List<DataStruct>();
    List<string> pathStrs = new List<string>();
    string dialogName;
    Vector2 scrollVer;
    Vector2 scrollHor;
    public GameObject prefab;
    GameObject pf;

    List<Rect> rects = new List<Rect>();
    Dictionary<Transform, int> dic = new Dictionary<Transform, int>();
    
    private void OnGUI()
    {      
        EditorGUILayout.LabelField("使用说明：");
        EditorGUILayout.LabelField("1.将界面预制体拖到此工具里");
        EditorGUILayout.LabelField("2.点击“绘制节点”，会显示预制体所有节点路径");
        EditorGUILayout.LabelField("3.勾选“是否生成”，并选择组件类型，(可不输入)输入节点在skin代码里的命名");
        EditorGUILayout.LabelField("3.(可不输入)输入Dialog的命名，点击生成代码，即可在Console里看到");

        scrollVer = EditorGUILayout.BeginScrollView(scrollVer);
        scrollHor = EditorGUILayout.BeginScrollView(scrollHor);

        GUILayout.BeginHorizontal("Box"); 
        prefab = EditorGUILayout.ObjectField(prefab, typeof(Object), true, GUILayout.Width(170)) as GameObject;
        dialogName = EditorGUILayout.TextField(dialogName,GUILayout.Width(170));
        if (GUILayout.Button("绘制节点"))
        {
            data.Clear();
            pathStrs.Clear();
            rects.Clear();
           // dic.Clear();
            FindTransforms();
        }      
        Draw();
        EditorGUILayout.EndScrollView();
        
        GUILayout.BeginHorizontal();
        for (int i = 0; i < 300; i++)
        {
            EditorGUILayout.Space();
        }
        GUILayout.EndHorizontal();

        EditorGUILayout.EndScrollView();
    }
    void Draw()
    {
        if (data.Count == 0)
            return;
        if (GUILayout.Button("生成代码"))
        {
            CreateSkinCode();
        }
        GUILayout.EndHorizontal();     
        for (int i = 0; i < 600; i++)
        {
            EditorGUILayout.Space();
        }
        //开始绘制节点
        GUILayout.BeginVertical();
        BeginWindows();
        for (int i = 0; i < data.Count; i++)
        {
            if (!data[i].IsDraw)
            {
                continue;
            }
            Vector2 pos = Vector2.zero;
            if (i != 0)
            {
                int index = GetIndexOfInParent(data[data[i].parentID].childNodes, data[i]);
                if (index != -1)
                {
                    pos = data[data[i].parentID].pos + new Vector2(130, index * 130);
                }
            }
            else
                pos = new Vector2(20, 50);
            data[i].pos = pos;
            Rect re = new Rect(data[i].pos.x, data[i].pos.y, 100, 120);
            re.Set(data[i].pos.x, data[i].pos.y, 100, 120);
            rects.Add(re);
            rects[i] = GUI.Window(data[i].windowID, rects[i], DrawNodeWindow, data[i].nodeName);
        }
        EndWindows();
        GUILayout.EndVertical();

        //开始绘制连线
        for (int i = 0; i < data.Count; i++)
        {
            if (!data[i].IsDraw)
            {
                continue;
            }
            Rect start;
            Rect end = rects[data[i].windowID];
            if (data[i].parentID == -1)
            {
                continue;
            }
            else
            {
                start = rects[data[i].parentID];
            }
            DrawNodeCurve(start, end);
        }
    }
    //自己在父节点的第几位
    int GetIndexOfInParent(List<DataStruct> list, DataStruct trans)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] == trans)
            {
                return i;
            }
        }
        return -1;
    }
    //绘制节点
    void DrawNodeWindow(int id)
    { 
        EditorGUILayout.BeginHorizontal();    
        data[id].create = EditorGUILayout.Toggle(data[id].create);
        EditorGUILayout.LabelField("是否生成");
        EditorGUILayout.EndHorizontal();
        data[id].name = EditorGUILayout.TextField(data[id].name, GUILayout.Width(70));
        data[id].comType = (ComponentType)EditorGUILayout.EnumPopup(data[id].comType);
        if (GUILayout.Button("展开"))
        {
            SetDrawChild(data[id].childNodes, true);
            Repaint();
        }
        if (GUILayout.Button("隐藏"))
        {
            SetDrawChild(data[id].childNodes, false);
            Repaint();
        }
        //设置窗口可以拖动
        GUI.DragWindow();
    }

    void SetDrawChild(List<DataStruct> daSt, bool draw)
    {
        for (int i = 0; i < daSt.Count; i++)
        {
            daSt[i].IsDraw = draw;
            SetDrawChild(daSt[i].childNodes, draw);
        }
    }

    void FindTransforms()
    {
        pf = Instantiate(prefab);
        if (prefab == null)
        {
            Debug.Log("找不到Prefab ");
            return;
        }
        Transform[] trans = pf.GetComponentsInChildren<Transform>();
        for (int i = 0; i < trans.Length; i++)
        {
            string tmp = GenPathStr(trans[i]);
            pathStrs.Add(tmp);
            DataStruct da = new DataStruct();
            da.nodeName = trans[i].name;
            da.trans = trans[i];
            da.path = tmp;
            da.windowID = i;
            if (trans[i].name == pf.name)
            {
                da.parentID = -1;
            }
            else
            { 
                da.parentID = FindParentID(da.trans);
                data[da.parentID].childNodes.Add(da);
            }
            data.Add(da);
            dic.Add(da.trans, da.windowID);     
        }
        Repaint();
        DestroyImmediate(pf);
    }

    //根据自身WindowID获取父ID
    int FindParentID(Transform curTrans)
    {
        return dic[curTrans.parent];
    }

    string GenPathStr(Transform tran)
    {
        string path = "";
        if (tran.name == pf.name)
        {
            return path;
        }      
        Transform[] tmpTrans = tran.gameObject.GetComponentsInParent<Transform>();
        for (int i = 0; i < tmpTrans.Length; i++)
        {
            if (tmpTrans[i].name != pf.name)
            {
                path = tmpTrans[i].name + path;
                path = "/" + path;             
            }
        }
        path = path.Substring(1);
        return path;
    }

    void CreateSkinCode()
    {
        StringBuilder code = new StringBuilder();
        string tmpStr = dialogName+"Skin";
        code.Append("local UISkin = require \"UI.Base.UISkin\";\n" + "local " + tmpStr + " = Class(\"" + tmpStr + "\", UISkin);\n");
        code.Append("local M = " + tmpStr + "; \n");
        code.Append("function M:OnCreate(transform)\nself:GenChildPathMap(transform);\n\n");

        //create code       
        for (int i = 0; i < data.Count; i++)
        {
            if (!data[i].create)
                continue;
            string tmp = "";
            if (data[i].comType == ComponentType.Child)  //child
            {
                tmp = CodeString.self + data[i].name + CodeString.equal + CodeString.getChild + data[i].path + CodeString.Yinhao + CodeString.last;
            }
            else if (data[i].comType == ComponentType.GameObject)  //gameobject
            {
                tmp = CodeString.self + data[i].name + CodeString.equal + CodeString.getGameobj + data[i].path + CodeString.Yinhao + CodeString.last;
            }
            else if (data[i].comType == ComponentType.Component)  //Component
            {
                tmp = CodeString.self + data[i].name + CodeString.equal + CodeString.GetComp + data[i].path + CodeString.CompNex + data[i].component + CodeString.Yinhao + CodeString.last;
            }
            else if (data[i].comType == ComponentType.Button)  //Button
            {
                tmp = CodeString.self + data[i].name + CodeString.equal + CodeString.BtnNex + CodeString.getChild + data[i].path + CodeString.Yinhao + ")" + CodeString.last;
            }
            else if (data[i].comType == ComponentType.GroupUnit)  //GroupUnit
            {
                tmp = CodeString.self + data[i].name + CodeString.equal + CodeString.GroupUnitNex + CodeString.getChild + data[i].path + CodeString.Yinhao + ")" + CodeString.last;               
            }
            else //扩展
            {

            }
            code.Append(tmp);
            code.Append("\n");
        }

        code.Append("\nend \n return M;\n ");

        Debug.Log(code);   
    }

    //连接节点曲线
    void DrawNodeCurve(Rect start, Rect end)
    {
        Vector3 startPos = new Vector3(start.x + start.width, start.y + start.height / 2, 0);
        Vector3 endPos = new Vector3(end.x, end.y + end.height / 2, 0);
        Vector3 startTan = startPos + Vector3.right * 50;
        Vector3 endTan = endPos + Vector3.left * 50;
        Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.blue, null, 4);
    }
}
public enum ComponentType
{
    Child,
    GameObject,
    Component,
    Button,
    GroupUnit
}
public class DataStruct
{
    public bool create;
    public string path;
    public string name;
    public int type;
    public string component;
    public ComponentType comType;

    //节点数据
    public int windowID;
    public int parentID;
    public bool IsDraw = true;
    public Vector2 pos;
    public string nodeName;
    public Transform trans;
    public List<DataStruct> childNodes = new List<DataStruct>();
    public void ShowData()
    {
        Debug.Log(nodeName+" "+windowID+" "+parentID+" "+pos.x+" "+pos.y);
    }
}

public class CodeString
{
    public static string self = "self.";
    public static string equal = " = ";
    public static string getChild = "self:GetChildByPath(\"";
    public static string getGameobj = "self:GetGameObjectByPath(\"";
    public static string GetComp = "self:GetComponentByPath(\"";
    public static string CompNex = "\",\"";
    public static string BtnNex = "ButtonUIComponent.New(";
    public static string GroupUnitNex = "GroupUnit.New(";
    public static string last = ");";
    public static string Yinhao = "\"";
}