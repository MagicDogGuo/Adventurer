using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadLevelData : MonoBehaviour {

    static SaveLoadLevelData m_Instance;
    public static SaveLoadLevelData Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = FindObjectOfType(typeof(SaveLoadLevelData)) as SaveLoadLevelData;

                if (m_Instance == null)
                {
                    var gameObject = new GameObject(typeof(SaveLoadLevelData).Name);
                    m_Instance = gameObject.AddComponent(typeof(SaveLoadLevelData)) as SaveLoadLevelData;
                }
            }
            return m_Instance;
        }
    }

    private void Start()
    {
        SaveLoadLevelData[] saveLoadLevelDatas = FindObjectsOfType<SaveLoadLevelData>();
        if (saveLoadLevelDatas.Length == 1)
        {
            DontDestroyOnLoad(gameObject);

        }

    }

    [SerializeField]
    ResultRecord ResultRecords;


    //List<LevelPassData> m_levelData = new List<LevelPassData>();
    List<ResultData> m_resultData = new List<ResultData>();

  

    public ResultData FetchResultDataFromLevelNo(int levelNo)
    {
        m_resultData = ResultRecords.ResultDatas;

        for (int i = 0; i < m_resultData.Count; i++)
        {
            if (m_resultData[i].Level == levelNo)
            {
                return m_resultData[i];
            }
        }
        return null;
    }
}
