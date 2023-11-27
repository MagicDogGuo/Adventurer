using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompleteExeBlockState : IMainGameState
{
    GameObject roleObj = null;
    GameObject resultUICanvases = null;
    //ResultDecide resultDecide = null;

    public CompleteExeBlockState(MainGameStateControl Controller) : base(Controller)  //Controller=GameLoop的m_SceneStateController
    {
        this.StateName = "CompleteExeBlockState";
    }

    //開始
    public override void StateBegin()
    {
        Debug.Log("完成編程");

        //恢復暫停狀態
        MainGameManager.Instance.IsReplayByStopCodeBtn = false;

        //清空舊的陣列
        MainGameManager.Instance.StartBlockArray.Clear();
        //生成結果UI
        MainGameManager.Instance.InstantiateResultObject();
        //刪除教學的canvas
        //MainGameManager.Instance.DestroyToturialCanvas();
        
        //找到主角物件
        roleObj = MainGameManager.Instance.RoleObjs;
        //找到結果UI
        resultUICanvases = MainGameManager.Instance.ResultUICanvases;

        //顯示結果
        ResultDecide.Instance.ShowResult(resultUICanvases.GetComponentInChildren<ResultUIComp>(), roleObj.GetComponent<RoleStatus>());

        //刪除原本ui
        MainGameManager.Instance.DestroyInitUI();

    }

    //更新
    public override void StateUpdate()
    {
    }

    //結束
    public override void StateEnd() {

        //初始化重玩參數
        MainGameManager.Instance.IsReplayByStopCodeBtn = false;

        //刪除原本的物件
        MainGameManager.Instance.DestroyInitObject();
        MainGameManager.Instance.DestroyResultObject();
        MainGameManager.Instance.DestroyInitMapObject();
        MainGameManager.Instance.DestroyExecuteBlockObj();
        MainGameManager.Instance.DestroyHandtoHandToturialCanvas();
        MainGameManager.Instance.DestroyLevelGoalCanvas();


        //重新設定mibo動作
        Mibo.motionReset();
    }

    void InToturial()
    {

    }
}
