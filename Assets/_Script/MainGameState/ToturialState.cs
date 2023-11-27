using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToturialState : IMainGameState
{
    public ToturialState(MainGameStateControl Controller) : base(Controller)  //Controller=GameLoop的m_SceneStateController
    {
        this.StateName = "ToturialState";
    }

    //開始
    public override void StateBegin()
    {
        ///////////////測試用//////////////
        //PlayerPrefs.DeleteKey("Record");

        MainGameManager.Instance.IsHaveToturial = bool_AutoCheckToturialIsOpen();

        if (!MainGameManager.Instance.IsReplayByStopCodeBtn)
        {
            if (MainGameManager.Instance.IsHaveToturial)
                OpenToturial();
            //else
            //    CloseToturial();
        }
    }

    //更新
    public override void StateUpdate()
    {
        if (!MainGameManager.Instance.IsHaveToturial || MainGameManager.Instance.IsReplayByStopCodeBtn) CloseToturial();      
    }


    //結束
    public override void StateEnd() {

        if (MainGameManager.Instance.IsHaveToturial)GameEventSystem.Instance.OnPushToturailCloseBtn -= ChangeToLevelGoalState;
    }



    bool bool_AutoCheckToturialIsOpen()
    {
        int level = MainGameManager.NowLevel;
        //手把手教學
        if (level == 1)
        {
            if (PlayerPrefs.HasKey("Record") && !SingletonLoader.Instance.GetComponent<GameLoop>().IsOpenToturailByMenu)
            {
                //已經進來過第一關
                return false;
            }
            else
            {             
                return true;
            }
        }
        else
        {
            return false;
        }

        #region 舊
        //switch (level)
        //{
        //    case 1:
        //    case 4:
        //    case 10:
        //    case 13:
        //        return true;

        //    default:
        //        return false;

        //}
        #endregion
    }

    void OpenToturial()
    {
        //教學UI出現
        MainGameManager.Instance.InstantiateHandtoHandToturialCanvas();
        //GameEventSystem.Instance.OnPushToturailCloseBtn += ChangeToComposeBlockState;
        InToturial();
    }

    void CloseToturial()
    {
        ChangeToLevelGoalState();
    }


    void ChangeToLevelGoalState()
    {
        m_Conrtoller.SetState(MainGameStateControl.GameFlowState.LevelGoal, m_Conrtoller);
    }

    void ChangeToComposeBlockState()
    {
        m_Conrtoller.SetState(MainGameStateControl.GameFlowState.ComposeBlock, m_Conrtoller);
    }

    void InToturial()
    {
        MainGameManager.Instance.HandToHandToturialUICanvases.GetComponent<HandtoHandToturialComp>().ToturialStateInToturail(ChangeToComposeBlockState);
    }



}
