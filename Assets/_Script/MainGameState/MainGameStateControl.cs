using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameStateControl {

    public enum GameFlowState
    {
        Init,
        Toturial,
        LevelGoal,
        ComposeBlock,
        ReadBlock,
        ExecuteBlock,
        CompleteBlock
    }
    
    public GameFlowState GameState { get; private set; }

    IMainGameState m_State = null; //現在三個子類別 MainMenuState、LoadingState、MainGameState
    bool m_bRunBegin = false;

    public MainGameStateControl() { }

    //設定狀態
    public void SetState(GameFlowState State, MainGameStateControl m_MainGameStateController)
    {
        GameState = State;
        m_bRunBegin = false;

        if (m_State != null)
        {
            m_State.StateEnd();
        }

        //設定State
        switch (State)
        {
            case GameFlowState.Init:
                m_State = new InitState(m_MainGameStateController);
                break;
            case GameFlowState.Toturial:
                m_State = new ToturialState(m_MainGameStateController);
                break;
            case GameFlowState.LevelGoal:
                m_State = new LevelGoalState(m_MainGameStateController);
                break;
            case GameFlowState.ComposeBlock:
                m_State = new ComposeBlockState(m_MainGameStateController);
                break;
            case GameFlowState.ReadBlock:
                m_State = new ReadBlockState(m_MainGameStateController);
                break;
            case GameFlowState.ExecuteBlock:
                m_State = new ExecuteBlockState(m_MainGameStateController);
                break;
            case GameFlowState.CompleteBlock:
                m_State = new CompleteExeBlockState(m_MainGameStateController);
                break;
        }
    }

    //更新
    public void StateUpdate()
    {
        //通知新的State開始
        if (m_State != null && m_bRunBegin == false)
        {
            m_State.StateBegin();
            m_bRunBegin = true;
        }

        if (m_State != null)
        {
            m_State.StateUpdate();
        }
    }

}
