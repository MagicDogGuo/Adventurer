using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadBlockState : IMainGameState {

    public ReadBlockState(MainGameStateControl Controller) : base(Controller)  //Controller=GameLoop的m_SceneStateController
    {
        this.StateName = "ReadBlockState";
    }

    ReadBlockOrder readBlockOreder = new ReadBlockOrder();
    ControlBlockUIComp controlBlockUIComp;

    //開始
    public override void StateBegin()
    {
        //找到Start方塊
        controlBlockUIComp = MainGameManager.Instance.ControlBlockUICanvases.GetComponentInChildren<ControlBlockUIComp>();
        ArrayList startBlockArrayList = controlBlockUIComp.StartBlock.GetComponent<Block>().DescendingBlocksForStartBlock();
        //讀取方塊順序
        MainGameManager.Instance.StartBlockArray = readBlockOreder.ReadBlocksOrder(startBlockArrayList);
        //生成目前階段UI
        //MainGameManager.Instance.InstantiateShowNowFlowUICanvas();

        //設定目前階段UI
        //GameObject showNowFlowUICanvas = MainGameManager.Instance.ShowNowFlowUICanvases;
        //showNowFlowUICanvas.GetComponentInChildren<ShowNowFloeUIComp>().ShowNowFlowTxt.text = this.GetType().ToString();


        //教學
        if (MainGameManager.Instance.IsHaveToturial) InToturial();
    }


    //更新
    public override void StateUpdate()
    {
        if (MainGameManager.Instance.StartBlockArray.Count == 0)
        {//如果沒有讀取到任何方塊，回到ComposeState
            MainGameManager.Instance.InstantiateWarningBlockCanvas();
            GameEventSystem.Instance.OnPushWarningBtn += ChangeToComposeBlockState;
        }
        else
        {//有讀取到方塊往下一步
            ChangeToExecuteBlockState();

            //編程停止按鈕/////////////////////////////////////////////
            controlBlockUIComp.StopCodeBtn.gameObject.SetActive(true);
            controlBlockUIComp.StartCodeBtn.gameObject.SetActive(false);
            GameEventSystem.Instance.OnPushStopCodeBtn += OnPushStopCodeBtn;
        }
    }

    //結束
    public override void StateEnd() {
        //刪除顯示目前階段UI
        MainGameManager.Instance.DestoryShowNowFlowUICanvas();
        GameEventSystem.Instance.OnPushWarningBtn -= ChangeToComposeBlockState;
    }


    void ChangeToComposeBlockState()
    {
        m_Conrtoller.SetState(MainGameStateControl.GameFlowState.ComposeBlock, m_Conrtoller);
    }

    void ChangeToExecuteBlockState()
    {
        m_Conrtoller.SetState(MainGameStateControl.GameFlowState.ExecuteBlock, m_Conrtoller);
    }

    void OnPushStopCodeBtn()
    {
        controlBlockUIComp = MainGameManager.Instance.ControlBlockUICanvases.GetComponentInChildren<ControlBlockUIComp>();/////////////////

        controlBlockUIComp.StopCodeBtn.gameObject.SetActive(false);
        controlBlockUIComp.StartCodeBtn.gameObject.SetActive(true);

        m_Conrtoller.SetState(MainGameStateControl.GameFlowState.Init, m_Conrtoller);

        MainGameManager.Instance.IsReplayByStopCodeBtn = true;

        MainGameManager.Instance.StartBlockArray.Clear();
        //刪除原本的物件
        MainGameManager.Instance.DestroyInitObject();
        MainGameManager.Instance.DestroyResultObject();
        MainGameManager.Instance.DestroyInitMapObject();
        //MainGameManager.Instance.DestroyExecuteBlockObj();
        //MainGameManager.Instance.DestroyHandtoHandToturialCanvas();
        MainGameManager.Instance.DestroyLevelGoalCanvas();

        //重新設定mibo動作
        Mibo.motionReset();

        ////////////////////////////////////////////
        GameEventSystem.Instance.OnPushStopCodeBtn -= OnPushStopCodeBtn;
    }

    void InToturial()
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        MainGameManager.Instance.HandToHandToturialUICanvases.GetComponent<HandtoHandToturialComp>().ReadBlockStateInToturail(ChangeToExecuteBlockState);
    }
}
