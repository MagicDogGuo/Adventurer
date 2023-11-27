using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;


public class GameContentTTSDatabase : Database<GameContentTTSRow>
{

    public GameContentTTSDatabase()
    {
        this.DataNameFromResourses = "GameContentTTS";
    }


    public override GameContentTTSRow FetchFromID(int id)
    {
        for (int i = 0; i < m_database.Count; i++)
        {
            if (m_database[i].ID == id)
            {
                return m_database[i];
            }
        }
        return null;
    }

    public override GameContentTTSRow FetchFromString_ID(string string_id)
    {
        for (int i = 0; i < m_database.Count; i++)
        {
            if (m_database[i].String_ID == string_id)
            {
                return m_database[i];
            }
        }
        return null;
    }

    protected override void ConstructDatabase()
    {
        foreach (JsonData jsonitem in m_jsondata)
        {
            switch (CurrLanguage.currLanguage)
            {
                case SystemLanguage.Chinese:
                case SystemLanguage.ChineseSimplified:
                    m_database.Add(new GameContentTTSRow(int.Parse(jsonitem["ID"].ToString()), jsonitem["String_ID"].ToString(), jsonitem["Description"].ToString(), jsonitem["CN"].ToString()));
                    break;
                case SystemLanguage.ChineseTraditional:
                    m_database.Add(new GameContentTTSRow(int.Parse(jsonitem["ID"].ToString()), jsonitem["String_ID"].ToString(), jsonitem["Description"].ToString(), jsonitem["TW"].ToString()));
                    break;

                case SystemLanguage.English:
                    m_database.Add(new GameContentTTSRow(int.Parse(jsonitem["ID"].ToString()), jsonitem["String_ID"].ToString(), jsonitem["Description"].ToString(), jsonitem["EN"].ToString()));
                    break;

                case SystemLanguage.Japanese:
                    m_database.Add(new GameContentTTSRow(int.Parse(jsonitem["ID"].ToString()), jsonitem["String_ID"].ToString(), jsonitem["Description"].ToString(), jsonitem["JP"].ToString()));
                    break;

                default:
                    //假如為空的話就去找預設語言的內容,目前為簡體中文
                    m_database.Add(new GameContentTTSRow(int.Parse(jsonitem["ID"].ToString()), jsonitem["String_ID"].ToString(), jsonitem["Description"].ToString(), jsonitem["CN"].ToString()));
                    break;
            }
        }
    }


    public override void SetupDatabase()
    {
        saveAndLoad = new SaveAndLoad();
        m_jsondata = saveAndLoad.LoadDataFormResources(DataNameFromResourses);
        //開始前有資料的話清除
        if (m_database.Count != 0) m_database.Clear();
        ConstructDatabase();
    }

}

public class GameContentTTSRow
{
    public int ID { get; set; }
    public string String_ID { get; set; }
    public string Description { get; set; }
    public string Content { get; set; }

    public GameContentTTSRow(int id, string string_id, string description, string content)
    {
        this.ID = id;
        this.String_ID = string_id;
        this.Description = description;
        this.Content = "//"+content;
    }
}
