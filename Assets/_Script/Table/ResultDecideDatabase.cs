using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

//用scriptableObject取代
public class ResultDecideDatabase : Database<ResultDecideRow>
{
    public ResultDecideDatabase()
    {
        this.DataNameFromResourses = "ResultDecide";
    }

    public override ResultDecideRow FetchFromID(int id)
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

    public  ResultDecideRow FetchRoleFromLevel(int level)
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

    public override ResultDecideRow FetchFromString_ID(string string_id) { throw new System.NotImplementedException();}


    protected override void ConstructDatabase()
    {
        foreach (JsonData jsonitem in m_jsondata)
        {
            m_database.Add(new ResultDecideRow(PraseToInt(jsonitem["ID"].ToString()), PraseToInt(jsonitem["Level"].ToString()), PraseToBool(jsonitem["Role_IsTouchInterRole"].ToString()), PraseToInt(jsonitem["Role_TakeKeyItemAmount"].ToString()),
   PraseToBool(jsonitem["Role_IsWetting"].ToString()), PraseToBool(jsonitem["Role_IsOpenUmbrella"].ToString()), PraseToBool(jsonitem["Role_IsHappyKebbi"].ToString()), PraseToBool(jsonitem["Role_IsPanicKebbi"].ToString())));

        }
    }

    int PraseToInt(string s)
    {
        return int.Parse(s);
    }

    bool PraseToBool(string s)
    {
        return bool.Parse(s);
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

public class ResultDecideRow
{
    public int ID { get; set; }
    public int Level { get; set; }
    public bool Role_IsTouchInterRole { get; set; }
    public int Role_TakeKeyItemAmount { get; set; }
    public bool Role_IsWetting { get; set; }
    public bool Role_IsOpenUmbrella { get; set; }
    public bool Role_IsHappyKebbi { get; set; }
    public bool Role_IsPanicKebbi { get; set; }


    public ResultDecideRow(int id, int level, bool role_IsTouchInterRole, int role_TakeKeyItemAmount, bool role_IsWetting, bool role_IsOpenUmbrella, bool role_IsHappyKebbi, bool role_IsSadKebbi)
    {
        this.ID = id;
        this.Level = level;
        this.Role_IsTouchInterRole = role_IsTouchInterRole;
        this.Role_TakeKeyItemAmount = role_TakeKeyItemAmount;
        this.Role_IsWetting = role_IsWetting;
        this.Role_IsOpenUmbrella = role_IsOpenUmbrella;
        this.Role_IsHappyKebbi = role_IsHappyKebbi;
        this.Role_IsPanicKebbi = role_IsSadKebbi;
    }    
  
}
