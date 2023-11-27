using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGoalState : IMainGameState
{
    public LevelGoalState(MainGameStateControl Controller) : base(Controller)  //Controller=GameLoop的m_SceneStateController
    {
        this.StateName = "LevelGoalState";
    }

    //開始
    public override void StateBegin()
    {
        //關卡目標UI出現
        if (!MainGameManager.Instance.IsReplayByStopCodeBtn)
        {
            MainGameManager.Instance.InstantiateLevelGoalCanvas();

            GameEventSystem.Instance.OnPushLevelGoalCloseBtn += ChangeToComposeState;

        }


    }

    //更新
    public override void StateUpdate()
    {
        if (MainGameManager.Instance.IsReplayByStopCodeBtn)
        {
            ChangeToComposeState();
        }

    }


    //結束
    public override void StateEnd() {
        GameEventSystem.Instance.OnPushLevelGoalCloseBtn -= ChangeToComposeState;

 
    }


    void ChangeToComposeState()
    {
        m_Conrtoller.SetState(MainGameStateControl.GameFlowState.ComposeBlock, m_Conrtoller);
    }

}
