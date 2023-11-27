using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoSingleton<DatabaseManager>
{
    LevelGoalTTSDatabase m_levelGoalTTSDatabase = new LevelGoalTTSDatabase();
    //TutorialTTSDatabase m_tutorialTTSDatabase = new TutorialTTSDatabase();
    //ResultDecideDatabase m_resultDecideDatabase = new ResultDecideDatabase();
    RobotmotionDatabase m_robotmotionDatabase = new RobotmotionDatabase();
    GameContentTextDatabase m_gameContentTextDatabase = new GameContentTextDatabase();
    GameContentImageDatabase m_gameContentImageDatabase = new GameContentImageDatabase();
    GameContentTTSDatabase m_gameContentTTSDatabase = new GameContentTTSDatabase();
    LevelPassDatabase m_levelPassDatabase = new LevelPassDatabase();
    HandToHandToturialTTSDatabase m_handToHandToturialTTSDatabase = new HandToHandToturialTTSDatabase();
    GameContentAudioDatabase m_gameContentAudioDatabase = new GameContentAudioDatabase();

    protected void Awake()
    {
        //m_tutorialTTSDatabase.SetupDatabase();
        m_levelGoalTTSDatabase.SetupDatabase();
        //m_resultDecideDatabase.SetupRoleDatabase();
        m_robotmotionDatabase.SetupDatabase();
        m_gameContentTextDatabase.SetupDatabase();
        m_gameContentImageDatabase.SetupDatabase();
        m_gameContentTTSDatabase.SetupDatabase();
        m_levelPassDatabase.SetupDatabase();
        m_handToHandToturialTTSDatabase.SetupDatabase();
        m_gameContentAudioDatabase.SetupDatabase();
    }
    
    /// <summary>
    /// 教學TTS(舊)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    //public TutorialTTSRow FetchFromID_TutorialTTSRow(int id)
    //{
    //    if (id == 0)
    //    {
    //        Debug.LogWarning("====id不能為0======");
    //        return null;
    //    }
    //    else return m_tutorialTTSDatabase.FetchFromID(id);
    //}
    //public TutorialTTSRow FetchFromSrting_ID_TutorialTTSRow(string string_id)
    //{
    //    return m_tutorialTTSDatabase.FetchFromString_ID(string_id);
    //}

    /// <summary>
    /// 關卡目標TTS
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public LevelGoalTTSRow FetchFromID_LevelGoalTTSRow(int id)
    {
        if (id == 0)
        {
            Debug.LogWarning("====id不能為0======");
            return null;
        }
        else return m_levelGoalTTSDatabase.FetchFromID(id);
    }
    public LevelGoalTTSRow FetchFromSrting_ID_LevelGoalTTSRow(string string_id)
    {
        return m_levelGoalTTSDatabase.FetchFromString_ID(string_id);
    }


    /// <summary>
    /// 過關條件
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    //public ResultDecideRow FetchFromID_ResultDecideRow(int id)
    //{
    //    if (id == 0)
    //    {
    //        Debug.LogWarning("====id不能為0======");
    //        return null;
    //    }
    //    else return m_resultDecideDatabase.FetchFromID(id);
    //}
    //public ResultDecideRow FetchRoleFromLevel_ResultDecideRow(int id)
    //{
    //    if (id == 0)
    //    {
    //        Debug.LogWarning("====id不能為0======");
    //        return null;
    //    }
    //    else return m_resultDecideDatabase.FetchRoleFromLevel(id);
    //}

    /// <summary>
    /// 機器人動作
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public RobotmotionRow FetchFromID_RobotmotionRow(int id)
    {
        if (id == 0)
        {
            Debug.LogWarning("====id不能為0======");
            return null;
        }
        else return m_robotmotionDatabase.FetchFromID(id);
    }
    public RobotmotionRow FetchFromSrting_ID_RobotmotionRow(string string_id)
    {
        return m_robotmotionDatabase.FetchFromString_ID(string_id);
    }

    /// <summary>
    /// 遊戲內容文字
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public GameContentTextRow FetchFromID_GameContentTextRow(int id)
    {
        if (id == 0)
        {
            Debug.LogWarning("====id不能為0======");
            return null;
        }
        else return m_gameContentTextDatabase.FetchFromID(id);
    }
    public GameContentTextRow FetchFromSrting_ID_GameContentTextRow(string string_id)
    {
        return m_gameContentTextDatabase.FetchFromString_ID(string_id);
    }

    /// <summary>
    /// 遊戲內容圖片
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public GameContentImageRow FetchFromID_GameContentImageRow(int id)
    {
        if (id == 0)
        {
            Debug.LogWarning("====id不能為0======");
            return null;
        }
        else return m_gameContentImageDatabase.FetchFromID(id);
    }
    public GameContentImageRow FetchFromSrting_ID_GameContentImageRow(string string_id)
    {
        return m_gameContentImageDatabase.FetchFromString_ID(string_id);
    }
    
    /// <summary>
    /// 遊戲內容文字
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public GameContentAudioRow FetchFromID_GameContentAudioRow(int id)
    {
        if (id == 0)
        {
            Debug.LogWarning("====id不能為0======");
            return null;
        }
        else return m_gameContentAudioDatabase.FetchFromID(id);
    }
    public GameContentAudioRow FetchFromSrting_ID_GameContentAudioRow(string string_id)
    {
        return m_gameContentAudioDatabase.FetchFromString_ID(string_id);
    }


    /// 遊戲內容TTS
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public GameContentTTSRow FetchFromID_GameContentTTSRow(int id)
    {
        if (id == 0)
        {
            Debug.LogWarning("====id不能為0======");
            return null;
        }
        else return m_gameContentTTSDatabase.FetchFromID(id);
    }
    public GameContentTTSRow FetchFromString_ID_GameContentTTSRow(string string_id)
    {
        return m_gameContentTTSDatabase.FetchFromString_ID(string_id);
    }


    /// <summary>
    /// 有無過關
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public LevelPassRow FetchFromID_LevelPassRow(int level)
    {
        if (level == 0)
        {
            Debug.LogWarning("====id不能為0======");
            return null;
        }
        else return m_levelPassDatabase.FetchFromID(level);
    }
    //存檔
    public void LevelPassToJsonSava(int level, bool isPass)
    {
        m_levelPassDatabase.LevelPassToJson(level, isPass);
        LevelPass_LoadSetUp();
    }

    public void LevelPass_LoadSetUp()/////////////////////////////////
    {
        StartCoroutine(IEDelayLoad());
    }
    IEnumerator IEDelayLoad()
    {
        m_levelPassDatabase = null;
        m_levelPassDatabase = new LevelPassDatabase();
        yield return new WaitForEndOfFrame();
        m_levelPassDatabase.SetupDatabase();

    }


    /// <summary>
    /// 手把手教學TTS
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public HandToHandToturialTTSRow FetchFromID_HandToHandTutorialTTSRow(int id)
    {
        if (id == 0)
        {
            Debug.LogWarning("====id不能為0======");
            return null;
        }
        else return m_handToHandToturialTTSDatabase.FetchFromID(id);
    }
    public HandToHandToturialTTSRow FetchFromSrting_ID_HandToHandTutorialTTSRow(string string_id)
    {
        return m_handToHandToturialTTSDatabase.FetchFromString_ID(string_id);
    }
}
