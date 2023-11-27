using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class PageIO  {

    //public static List<T> GetAsset<T>()
    //{
    //    string[] aSDFiles = Directory.GetFiles(Application.dataPath + "/ToturialPagePath/", "*.asset", SearchOption.AllDirectories);
    //    List<T> objs = new List<T>();
    //    foreach (string sdFile in aSDFiles)
    //    {
    //        string assetPath = "Assets" + sdFile.Replace(Application.dataPath, "").Replace('\\', '/');
    //        var t = AssetDatabase.LoadAssetAtPath(assetPath,typeof(T));
    //        objs.Add(t);
    //    }

    //}

    public static T[] GetAtPath<T>(string path)
    {

        ArrayList al = new ArrayList();
        string[] fileEntries = Directory.GetFiles(Application.dataPath + "/" + path);
        
        foreach (string fileName in fileEntries)
        {
            
            string assetPath = "Assets" + fileName.Replace(Application.dataPath, "").Replace('\\', '/');
            
            Object t = AssetDatabase.LoadAssetAtPath(assetPath, typeof(T));

            if (t != null)
                al.Add(t);
        }
        T[] result = new T[al.Count];
        for (int i = 0; i < al.Count; i++)
            result[i] = (T)al[i];


        return result;
    }
}
