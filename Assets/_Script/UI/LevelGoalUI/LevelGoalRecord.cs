using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "教學目標")]
public class LevelGoalRecord : ScriptableObject {
   public List<LevelGaolData> LevelGaolDatas;
}

[System.Serializable]
public class LevelGaolData
{
    public int Level;
    public string PageTTSID;
    public GoalObject[] m_GoalObjects;

    [System.Serializable]
    public class GoalObject
    {
        [EditorName("目標物件(1~3個)")]
        public GoalObjectEnum GoalObjectEnums;
    }
}


public enum GoalObjectEnum
{
    Bettery,
    FeedDog,
    FindDog,
    Toy,
    Umbrella,
    DigHole,
    FindThing,//不關鍵物件
    MoveFront,
    MoveBack,
    MoveRight,
    MoveLeft,
    East,
    West,
    South,
    North
}