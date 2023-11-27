using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;


public class LevelPassDatabase : Database<LevelPassRow>
{
    public static string JsonFilePath;

    public LevelPassDatabase()
    {
        this.DataNameFromResourses = "LevelPass";
    }

    public override LevelPassRow FetchFromID(int level)
    {
        for (int i = 0; i < m_database.Count; i++)
        {
            if (m_database[i].Level == level)
            {
                return m_database[i];
            }
        }
        return null;
    }

    public override LevelPassRow FetchFromString_ID(string string_id)
    {
        throw new System.NotImplementedException();
    }

    protected override void ConstructDatabase()
    {

        foreach (JsonData jsonitem in m_jsondata)
        {
            m_database.Add(new LevelPassRow(int.Parse(jsonitem["Level"].ToString()),bool.Parse(jsonitem["IsPass"].ToString()), int.Parse(jsonitem["Score"].ToString())));
        }
    }

    public override void SetupDatabase()
    {
        saveAndLoad = new SaveAndLoad();
        // m_jsondata = saveAndLoad.LoadDataFormResources(DataNameFromResourses);
        //開始前有資料的話清除
        //if (m_database.Count != 0) { m_database.Clear(); }

#if UNITY_EDITOR
        //載入與儲存路徑
        JsonFilePath = Application.dataPath + "/Resources/" + DataNameFromResourses + ".json";
        m_jsondata = saveAndLoad.LoadData(JsonFilePath);


#elif UNITY_ANDROID

        JsonFilePath = Application.persistentDataPath + "/"+ DataNameFromResourses + ".json";


        m_jsondata = saveAndLoad.LoadData(JsonFilePath);


        //有無抓到新另存的檔案
        if (m_jsondata == null)
        {//沒有抓到新另存的檔案

            //載入初始路徑
            m_jsondata = saveAndLoad.LoadDataFormResources(DataNameFromResourses);
            Debug.Log("載入初始路徑");
        }
#endif

        ConstructDatabase();

    }



    /// <summary>
    /// </summary>
    /// <param name="ID">要被重新設定值的ID</param>
    /// <param name="lotedbool">設定值的true或是false</param>
    public void LevelPassToJson(int level, bool isPass)
    {
        //設定要存的欄位數量
        int amount = m_database.Count;

        List<LevelPassRowToSave> savedatabase = new List<LevelPassRowToSave>();

        //實例化
        for (int i = 1; i <= amount; i++)
        {
            savedatabase.Add(new LevelPassRowToSave(-1,"0",-1));
        }
        //Debug.Log(FetchAnimalFromID(0).Kind);

        //先根據上一次讀取到的database全部欄位重載
        for (int i = 0; i < amount; i++)
        {
            savedatabase[i].Level = DatabaseManager.Instance.FetchFromID_LevelPassRow(i+1).Level;
            savedatabase[i].IsPass = DatabaseManager.Instance.FetchFromID_LevelPassRow(i+1).IsPass.ToString();
            savedatabase[i].Score = DatabaseManager.Instance.FetchFromID_LevelPassRow(i + 1).Score;
        }

        //再修改預計要改的欄位
        savedatabase[level-1].IsPass = isPass.ToString();

        //最後存儲savedatabase到指定路徑，更新json的內容  
        saveAndLoad.SaveData(savedatabase, JsonFilePath);

    }  
}


public class LevelPassRow
{
    public int Level { get; set; }
    public bool IsPass { get; set; }
    public int Score { get; set; }

    public LevelPassRow(int level, bool isPass,int score)
    {
        this.Level = level;
        this.IsPass = isPass;
        this.Score = score;
    }
}


public class LevelPassRowToSave
{
    public int Level { get; set; }
    public string IsPass { get; set; }
    public int Score { get; set; }

    public LevelPassRowToSave(int level, string isPass, int score)
    {
        level = -1;
    }
}
