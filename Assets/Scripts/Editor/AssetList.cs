using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using System.IO;

public class AssetListBuilder : AssetPostprocessor,IPreprocessBuildWithReport
{
    public int callbackOrder => 0;

    void Start()
    {
        
    }

    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        Debug.Log($"Rebuilding resource database for {importedAssets.Length} new files");
        RebuidDatabase();
    }
    public static void RebuidDatabase()
    {
        string objectList = "";
        
        foreach (var item in AssetDatabase.GetAllAssetPaths())
        {
            if (Regex.IsMatch(item, "\\/?[A,a]ssets\\/[R,r]esources\\/(?!AssetList)"))
            {
                var path = item.Substring(17);
                if (!File.Exists(Application.dataPath+"/Resources/"+ path))
                {
                    continue;
                }
                objectList += $"{path.Substring(0,path.LastIndexOf('.'))};";
            }
        }
        File.WriteAllText(Application.dataPath+"/Resources/AssetList.txt",objectList);
        AssetDatabase.Refresh();
    }
    public void OnPreprocessBuild(BuildReport report)
    {
        Debug.Log($"Rebuilding resource database before build");
        //RebuidDatabase();
    }
}
