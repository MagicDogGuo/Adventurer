using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public abstract class Database<T> {


    protected string DataNameFromResourses = "default";

    protected JsonData m_jsondata;

    protected List<T> m_database = new List<T>();

    protected SaveAndLoad saveAndLoad = null;

    /// <summary>
    /// 從ID抓資料
    /// </summary>
    public abstract T FetchFromID(int id);

    /// <summary>
    /// 從Srting_ID抓資料
    /// </summary>
    public abstract T FetchFromString_ID(string string_id);

    /// <summary>
    /// 對應Json欄位
    /// </summary>
    protected abstract void ConstructDatabase();

    /// <summary>
    /// 設定
    /// </summary>
    public abstract void SetupDatabase();
}
