using UnityEngine;
using System.IO;
using LitJson;
using System;
 using System.Text.RegularExpressions;

public class SaveAndLoad  {

    /// <summary>
    /// 儲存json
    /// </summary>
    /// <typeparam name="T">不確定會放什麼資料進來</typeparam>
    /// <param name="saveData">要儲存的資料</param>
    /// <param name="jsonFilePath">要存的路徑</param>
    public void SaveData<T>(T saveData, string jsonFilePath)
    {
        //排列json
        JsonWriter jsonWriter = new JsonWriter();
        jsonWriter.PrettyPrint = true;
        jsonWriter.IndentValue = 5;

        //把JsonData轉成JsonWriter
        JsonMapper.ToJson(saveData, jsonWriter);

        File.WriteAllText(jsonFilePath, String_ChineseConvert(jsonWriter.ToString()));

        //為轉換中文前的寫法
        //File.WriteAllText(jsonFilePath, jsonWriter.ToString());
    }


    /// <summary>
    /// 載入json
    /// </summary>
    /// <param name="jsonFilePath">要讀取的路徑</param>
    public JsonData LoadData(string jsonFilePath)
    {
        //判斷路徑是否存在
        if (!File.Exists(jsonFilePath))
        {
            return null;
        }

        //讀取指定位置的json檔
        JsonData jsonData = JsonMapper.ToObject(File.ReadAllText(jsonFilePath));
        //Debug.Log(" jsonData.Count:" + jsonData.Count);
        return jsonData;
    }

    /// <summary>
    /// 載入初始檔案用(手機端)
    /// </summary>
    /// <param name="fileName">第一次載入的檔案名稱</param>
    /// <returns></returns>
    public JsonData LoadDataFormResources(string fileName)
    {
        //**********TextAsset要讀取中文，需要將文件存成UTF-8格式*****************************
        //讀取指定位置的json檔
        TextAsset textasset = Resources.Load(fileName) as TextAsset;
        JsonData jsonData = JsonMapper.ToObject(textasset.text);
    
       // Debug.Log(" jsonData.Count:" + jsonData.Count);
        return jsonData;
    }

    /// <summary>
    /// 可以將中文的unicode轉成能識別的GBK編碼
    /// </summary>
    /// <param name="ToJsondata">要轉化的字串</param>
    /// <returns></returns>
    string String_ChineseConvert(string ToJsondata ) {
        string jsonStr = ToJsondata;

        Regex reg = new Regex(@"(?i)\\[uU]([0-9a-f]{4})");
        string ss = reg.Replace(jsonStr, delegate (Match m) { return ((char)Convert.ToInt32(m.Groups[1].Value, 16)).ToString(); });

        return ss;
    }
}
