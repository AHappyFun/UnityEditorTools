using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Data;
using System.IO;

public class MusicFilesSelectTools : EditorWindow
{    
    public static string[] OpenCSV(string filepath)
    {  
        return File.ReadAllLines(filepath);
    }

    static string dir = @"F:\Project\auto_client";
    static string outputDir = @"Assets\MusicsNewPlot";
    static string resDir = @"Assets\Resources\Music\Plot";
    static string createFolder = "Assets/MusicsNewPlot";

    [MenuItem("Tools/SelectMusicPlot")]
    public static void SelectMusic()
    {
        if (!Directory.Exists(createFolder))
        {
            Directory.CreateDirectory(createFolder);
        }

        string[] data = OpenCSV(Path.Combine(Application.dataPath, "plot.csv"));
        string[] forders = new string[data.Length];
        for (int i = 0; i < data.Length; i++)
        {         
            string name = data[i].Split(',')[2];
            if(!Directory.Exists(Path.Combine(createFolder, name)))
            {
                Directory.CreateDirectory(Path.Combine(createFolder, name));
            }
            
            string resName = data[i].Split(',')[4] + ".ogg";
            
            string fullResPath = Path.Combine(dir,resDir, resName);
            string newResPath = Path.Combine(dir,outputDir, name, resName);

            File.Copy(fullResPath, newResPath, true);

        }
        AssetDatabase.Refresh();
    } 
}
