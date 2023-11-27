using System.Collections;
using System.Collections.Generic;
using System.IO;
//using UnityEditor;
using UnityEngine;

public class LoadAssetFile : MonoSingleton<LoadAssetFile>{


    //public List<GameObject> LoadPrefabInAsset(string path)
    //{
    //    List<GameObject> go = new List<GameObject>();

    //    //Application.dataPath + "/_Prefab/MapGrid/"
    //    string[] aMapFiles = Directory.GetFiles(path, "*.prefab", SearchOption.TopDirectoryOnly);

    //    foreach (string mapFile in aMapFiles)
    //    {
    //        string assetPath = "Assets" + mapFile.Replace(Application.dataPath, "").Replace('\\', '/');
    //        Debug.Log(assetPath);
    //        Object prefab = AssetDatabase.LoadAssetAtPath(assetPath, typeof(GameObject));
    //        go.Add((GameObject)prefab);
    //    }

    //    return go;
    //}


    List<GameObject> mapsInResouces;
    public List<GameObject> MapsInResouces
    {
        get { return mapsInResouces;}
    }

    private void Start()
    {
        mapsInResouces = LoadPrefabInAssetFromResource("Maps");
    }


    public List<GameObject> LoadPrefabInAssetFromResource(string path)
    {
        List<GameObject> go = new List<GameObject>();

        GameObject[] maps = Resources.LoadAll<GameObject>(path);
        foreach (var item in maps)
        {
            go.Add(item);
        }
        return go;
    }
}
