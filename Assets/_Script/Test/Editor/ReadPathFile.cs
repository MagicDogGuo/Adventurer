using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class ReadPathFile : MonoBehaviour {

    [SerializeField]
    List<GameObject> go = new List<GameObject>() ;

    void Start()
    {
        string[] aMapFiles = Directory.GetFiles(Application.dataPath+ "/_Prefab/MapGrid/", "*.prefab", SearchOption.TopDirectoryOnly);
        Debug.Log("aMapFiles.Length:" + aMapFiles.Length);
        foreach (string mapFile in aMapFiles)
        {
            string assetPath = "Assets" + mapFile.Replace(Application.dataPath, "").Replace('\\', '/');
            Debug.Log(assetPath);
            Object prefab = AssetDatabase.LoadAssetAtPath(assetPath, typeof(GameObject));
            go.Add((GameObject)prefab);

        }
        Debug.Log("go.Count:"+ go.Count);

        foreach (var item in go)
        {
            Instantiate(item);
        }
    }
}
