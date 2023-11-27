using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitState : IMainGameState
{
    GameObject[] AllBlockGOs = MainGameManager.Instance.AllblockInScene();

    LevelPool levelPool = new LevelPool();

    public InitState(MainGameStateControl Controller) : base(Controller)  //Controller=GameLoop的m_SceneStateController
    {
        this.StateName = "InitState";
    }

    //開始
    public override void StateBegin()
    {
        //歸位mibo動作
        Mibo.motionReset();

        //不能拖曳block
        foreach (var allBlockGOs in AllBlockGOs)
        {
            if (allBlockGOs != null) allBlockGOs.GetComponent<Block>().isCanMoveBlock = false;
        }

        Debug.Log("level: "+ MainGameManager.NowLevel);

        //設定關卡
        levelPool.SetLevel(MainGameManager.NowLevel);

        if( MainGameManager.Instance.IsReplayByStopCodeBtn == false && MainGameManager.Instance.IsHaveToturial==false)
        {
            GameEventSystem.Instance.OnPushRestartBtn += RestartState;
            GameEventSystem.Instance.OnPushNextLevelBtn += NextLevel;
            GameEventSystem.Instance.OnPushMenuBtn += MenuScene;
        }
    }

    //更新
    public override void StateUpdate() {
        m_Conrtoller.SetState(MainGameStateControl.GameFlowState.Toturial, m_Conrtoller);
    }

    //結束
    public override void StateEnd() { }

    /// <summary>
    /// 重新開始 0619 改成回選單，方法名我就先不改了
    /// </summary>
    void RestartState()
    {
        m_Conrtoller.SetState(MainGameStateControl.GameFlowState.Init, m_Conrtoller);
        GameEventSystem.Instance.OnPushMenuBtn -= MenuScene;
        GameEventSystem.Instance.OnPushNextLevelBtn -= NextLevel;
        GameEventSystem.Instance.OnPushRestartBtn -= RestartState;
        MainGameManager.Instance.IsReplayByStopCodeBtn = false;
    }

    void NextLevel()
    {
        MainGameManager.NowLevel++;
        m_Conrtoller.SetState(MainGameStateControl.GameFlowState.Init, m_Conrtoller);
        GameEventSystem.Instance.OnPushMenuBtn -= MenuScene;
        GameEventSystem.Instance.OnPushNextLevelBtn -= NextLevel;
        GameEventSystem.Instance.OnPushRestartBtn -= RestartState;
        MainGameManager.Instance.IsReplayByStopCodeBtn = false;

        //Debug.Log("==============================================");
    }

    void MenuScene()
    {
        m_Conrtoller.SetState(MainGameStateControl.GameFlowState.Init, m_Conrtoller);
        GameEventSystem.Instance.OnPushMenuBtn -= MenuScene;
        GameEventSystem.Instance.OnPushNextLevelBtn -= NextLevel;
        GameEventSystem.Instance.OnPushRestartBtn -= RestartState;
        MainGameManager.Instance.IsReplayByStopCodeBtn = false;
    }
}
