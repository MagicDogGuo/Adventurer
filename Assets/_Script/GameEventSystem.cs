using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventSystem : MonoSingleton<GameEventSystem> {
    
    //標題頁
    public UnityAction OnPushStartGameBtn;
    public UnityAction OnPushExitGameBtn;
    public UnityAction OnPushInfoBtn;
    public UnityAction<int> OnPushToturailBtn;

    //主選單
    public UnityAction OnPushBackTitleBtn;
    public UnityAction<int> OnPushSceneBtn;

    //主遊戲
    public UnityAction OnPushToturailCloseBtn;
    public UnityAction OnPushLevelGoalCloseBtn;
    public UnityAction OnPushStartCodeBtn;
    public UnityAction OnPushStopCodeBtn;
    public UnityAction OnPushRestartBtn;
    public UnityAction OnPushNextLevelBtn;
    public UnityAction OnPushMenuBtn;
    public UnityAction OnPushWarningBtn;
    public UnityAction OnPauseGameBtn;

    //手把手教學
    public UnityAction OverToturialEvent;


    public void DisRegistEvents_Title()
    {
        OnPushStartGameBtn = null;
        OnPushExitGameBtn = null;
    }


    public void DisRegistEvents_MainMenu()
    {
        OnPushBackTitleBtn = null;
        OnPushSceneBtn = null;
        OnPushInfoBtn = null;
        OnPushToturailBtn = null;
    }

    public void DisRegistEvents_MainGame()
    {
        OnPushToturailCloseBtn = null;
        OnPushLevelGoalCloseBtn = null;
        OnPushStartCodeBtn = null;
        OnPushStopCodeBtn = null;
        OnPushRestartBtn = null;
        OnPushNextLevelBtn = null;
        OnPushMenuBtn = null;
        OnPushWarningBtn = null;
        OnPauseGameBtn = null;
    }

}
