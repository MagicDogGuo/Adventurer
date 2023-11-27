using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ExecuteBlockState : IMainGameState
{
    ExecuteBlock executeBlock = null;
    GameObject roleObj = null;

    public ExecuteBlockState(MainGameStateControl Controller) : base(Controller)  //Controller=GameLoop的m_SceneStateController
    {
        this.StateName = "ExecuteBlockState";
    }

    //開始
    public override void StateBegin()
    {
        List<string> StartBlockArray = MainGameManager.Instance.StartBlockArray;
        //生成執行方塊功能物件
        MainGameManager.Instance.InstantiateExecuteBlockObject();
        //找到執行方塊動作物件
        executeBlock = MainGameManager.Instance.ExecuteBlockObjs.GetComponent<ExecuteBlock>();
        //找到主角物件
        roleObj = MainGameManager.Instance.RoleObjs;
        //開始執行
        executeBlock.ExecuteBlockFuntion(StartBlockArray, 3, roleObj);
       
        //生成目前階段UI
        //MainGameManager.Instance.InstantiateShowNowFlowUICanvas();
        ////設定目前階段UI
        //GameObject showNowFlowUICanvas = MainGameManager.Instance.ShowNowFlowUICanvases;
        //showNowFlowUICanvas.GetComponentInChildren<ShowNowFloeUIComp>().ShowNowFlowTxt.text = this.GetType().ToString();

        //註冊事件
        //到最後方塊執行，進下一State
        executeBlock.CompleteEXEvent += ChangeToCompleteBlockState;
        //這關成功，進下一State
        executeBlock.SuccessThisLevelEvnet += ChangeToCompleteBlockState;

        //教學
        if (MainGameManager.Instance.IsHaveToturial) InToturial();

    }

    //更新
    public override void StateUpdate()
    {
    }

    //結束
    public override void StateEnd()
    {
        //刪除執行方塊功能物件
        //MainGameManager.Instance.DestroyExecuteBlockObj();
        //刪除顯示目前階段UI
        MainGameManager.Instance.DestoryShowNowFlowUICanvas();

        //取消註冊
        executeBlock.CompleteEXEvent -= ChangeToCompleteBlockState;
        executeBlock.SuccessThisLevelEvnet -= ChangeToCompleteBlockState;
    }

    void ChangeToCompleteBlockState()
    {
        m_Conrtoller.SetState(MainGameStateControl.GameFlowState.CompleteBlock, m_Conrtoller);
    }

    void InToturial()
    {
        MainGameManager.Instance.HandToHandToturialUICanvases.GetComponent<HandtoHandToturialComp>().ExecuteBlockStateInToturail(ChangeToCompleteBlockState);
    }
}
