using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleState : ISceneState
{

    public TitleState(SceneStateControler Controler) : base(Controler)
    {
        this.StateName = "TitleState";
    }

    public override void StateBegin()
    {
        //BGM
        GetAudioSource comp = AudioManager.Instance.GetComponent<GetAudioSource>();
        if (comp != null) comp.PlayBGM(GetAudioSource.BGMKinds.MainMenu);

        GameEventSystem.Instance.OnPushStartGameBtn += StartGame;
        GameEventSystem.Instance.OnPushExitGameBtn += ExitGame;

        GameEventSystem.Instance.OnPushToturailBtn += ChangeToToturailScene;
    }

    public override void StateUpdate()
    {
    }

    public override void StateEnd()
    {
        GameEventSystem.Instance.OnPushStartGameBtn -= StartGame;
        GameEventSystem.Instance.OnPushExitGameBtn -= ExitGame;
        GameEventSystem.Instance.OnPushToturailBtn -= ChangeToToturailScene;

    }

    void StartGame()
    {
        m_Controler.SetState(new MainMenuState(m_Controler), "MainMenuState");
    }


    void ExitGame()
    {
        Mibo.release();
        //System.Diagnostics.Process.GetCurrentProcess().Kill(); //強制關閉
        Application.Quit();

        Debug.Log("exit");
    }


    void ChangeToToturailScene(int level)
    {
        GameEventSystem.Instance.OnPushToturailBtn -= ChangeToToturailScene;
        m_Controler.SetState(new MainGameState(m_Controler), "MainGameState");
        MainGameManager.NowLevel = level;
    }
}
