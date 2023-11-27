using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameState : ISceneState {

    public MainGameState(SceneStateControler Controler) : base(Controler)
    {
        this.StateName = "MainGameState";
    }

    public override void StateBegin()
    {
        MainGameManager.Instance.MainGameBegin();

        GameEventSystem.Instance.OnPushMenuBtn += MenuScene;
        //手把手教學
        GameEventSystem.Instance.OverToturialEvent += TitlrScene;
    }

    public override void StateUpdate()
    {
        MainGameManager.Instance.MainGameUpdate();
    }

    public override void StateEnd()
    {
        GameEventSystem.Instance.OnPushMenuBtn -= MenuScene;
        //手把手教學
        GameEventSystem.Instance.OverToturialEvent -= TitlrScene;
    }

    /// <summary>
    /// 回主選單
    /// </summary>
    public void MenuScene()
    {
        GameEventSystem.Instance.OnPushMenuBtn -= MenuScene;
        m_Controler.SetState(new MainMenuState(m_Controler), "MainMenuState");
    }


    /// <summary>
    /// 回開頭頁
    /// </summary>
    public void TitlrScene()
    {
        //手把手教學
        GameEventSystem.Instance.OverToturialEvent -= TitlrScene;
        m_Controler.SetState(new TitleState(m_Controler), "TitleState");
    }

}
