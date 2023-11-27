using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class GameContentImageDatabase : Database<GameContentImageRow>
{
    public GameContentImageDatabase()
    {
        this.DataNameFromResourses = "GameContentImage";
    }

    public override GameContentImageRow FetchFromID(int id)
    {
        for (int i = 0; i < m_database.Count; i++)
        {
            if (m_database[i].ID == id)
            {
                if (m_database[i].Sprite_Content == null)
                {
                    switch (CurrLanguage.currLanguage)
                    {
                        case SystemLanguage.Chinese:
                        case SystemLanguage.ChineseSimplified:
                            m_database[i].Sprite_Content = Resources.Load<Sprite>("GameContentImage/CN/" + m_database[i].Sprite_ContentName);
                            break;

                        case SystemLanguage.ChineseTraditional:
                            m_database[i].Sprite_Content = Resources.Load<Sprite>("GameContentImage/TW/" + m_database[i].Sprite_ContentName);
                            break;

                        case SystemLanguage.English:
                            m_database[i].Sprite_Content = Resources.Load<Sprite>("GameContentImage/EN/" + m_database[i].Sprite_ContentName);
                            break;

                        case SystemLanguage.Japanese:
                            m_database[i].Sprite_Content = Resources.Load<Sprite>("GameContentImage/JP/" + m_database[i].Sprite_ContentName);
                            break;

                        default:
                            m_database[i].Sprite_Content = Resources.Load<Sprite>("GameContentImage/CN/" + m_database[i].Sprite_ContentName);
                            break;
                    }
                }

                return m_database[i];
            }
        }
        return null;
    }

    public override GameContentImageRow FetchFromString_ID(string string_id)
    {
        for (int i = 0; i < m_database.Count; i++)
        {
            if (m_database[i].String_ID == string_id)
            {
                if (m_database[i].Sprite_Content == null)
                {
                    switch (CurrLanguage.currLanguage)
                    {
                        case SystemLanguage.Chinese:
                        case SystemLanguage.ChineseSimplified:
                            m_database[i].Sprite_Content = Resources.Load<Sprite>("GameContentImage/CN/" + m_database[i].Sprite_ContentName);
                            break;

                        case SystemLanguage.ChineseTraditional:
                            m_database[i].Sprite_Content = Resources.Load<Sprite>("GameContentImage/TW/" + m_database[i].Sprite_ContentName);
                            break;

                        case SystemLanguage.English:
                            m_database[i].Sprite_Content = Resources.Load<Sprite>("GameContentImage/EN/" + m_database[i].Sprite_ContentName);
                            break;

                        case SystemLanguage.Japanese:
                            m_database[i].Sprite_Content = Resources.Load<Sprite>("GameContentImage/JP/" + m_database[i].Sprite_ContentName);
                            break;

                        default:
                            m_database[i].Sprite_Content = Resources.Load<Sprite>("GameContentImage/CN/" + m_database[i].Sprite_ContentName);
                            break;
                    }
                }            
                return m_database[i];
            }
        }
        return null;
    }

    protected override void ConstructDatabase()
    {
        foreach (JsonData jsonitem in m_jsondata)
        {
            m_database.Add(new GameContentImageRow(int.Parse(jsonitem["ID"].ToString()), jsonitem["String_ID"].ToString(), jsonitem["Description"].ToString(), jsonitem["Img_name"].ToString()));
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

public class GameContentImageRow
{
    public int ID { get; set; }
    public string String_ID { get; set; }
    public string Description { get; set; }
    public string Sprite_ContentName { get; set; }

    public Sprite Sprite_Content { get; set; }

    public GameContentImageRow(int id, string string_id, string description, string sprite_ContentName)
    {
        this.ID = id;
        this.String_ID = string_id;
        this.Description = description;
        this.Sprite_ContentName = sprite_ContentName;
    }
}
