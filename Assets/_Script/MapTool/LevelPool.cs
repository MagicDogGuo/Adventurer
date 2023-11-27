using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPool  {

    Vector3 m_rolePos;
    Vector3 m_goalPos;
    Vector3 m_getItemPos;
    Vector3 m_isOpenWetObjPos;

    bool m_IsOpenRole;
    bool m_IsOpenGoal;
    bool m_IsOpenGetItem;
    bool m_isOpenWetObj;

    //原始位置
    //Vector3 mapZeroPos = new Vector3(0.48f, 0.48f,0);

    //float m_MapBlockWidth = 0.96f;
    //float m_MapBlockHeight = 0.96f;
    //方格(6,3) = 世界(0,0)

    public void SetLevel(int level)
    {
        //呼叫地圖
        Level(level);
       
    }

    /// <summary>
    /// 關卡
    /// </summary>
     void Level(int level)
    {
        //生成地圖
        MainGameManager.Instance.InstantiateInitObject(level-1);//陣列從0開始

        //設定地圖初始物件
        MainGameManager.Instance.SetInitMapObject();

    }


    /// <summary>
    /// 每一關給的方塊
    /// </summary>
    /// <param name="level"></param>
    public void InstanceLevelBlock()
    {
        //生成初始方塊種類(設定出現的種類，不用設定出現的位置)
        GameObject BlockUIContent = MainGameManager.Instance.ControlBlockUICanvases.GetComponentInChildren<ControlBlockUIComp>().Content;
        //不用管第幾關，去抓Mapsource上的欄位就好
        LevelBlock[] mapSourceLevelBlock = MainGameManager.Instance.NowMapGridObjs.GetComponent<MapSource>().LevelBlocks;

        MainGameManager.Instance.GetComponent<AllBlockKind>().InstanceBlock_Array(BlockUIContent, mapSourceLevelBlock);
      
    }   
}
