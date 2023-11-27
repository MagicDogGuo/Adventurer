using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class RobotmotionDatabase : Database<RobotmotionRow>
{
    public RobotmotionDatabase()
    {
        this.DataNameFromResourses = "Robotmotion";
    }

    public override RobotmotionRow FetchFromID(int id)
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

    public override RobotmotionRow FetchFromString_ID(string string_id)
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
            m_database.Add(new RobotmotionRow(int.Parse(jsonitem["ID"].ToString()), jsonitem["String_ID"].ToString(), jsonitem["Motion"].ToString()));
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

public class RobotmotionRow
{
    public int ID { get; set; }
    public string String_ID { get; set; }
    public string Motion { get; set; }

    public RobotmotionRow(int id, string string_id, string motion)
    {
        this.ID = id;
        this.String_ID = string_id;
        this.Motion = motion;
    }
}
