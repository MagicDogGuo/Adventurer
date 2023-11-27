using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuState : ISceneState {

    public MainMenuState(SceneStateControler Controler) : base(Controler)
    {
        this.StateName = "MainMenuState";
    }

    public override void StateBegin()
    {
        //BGM
        GetAudioSource comp = AudioManager.Instance.GetComponent<GetAudioSource>();
        if (comp != null) comp.PlayBGM(GetAudioSource.BGMKinds.MainMenu);

        //生成UI
        MainMenuManager.Instance.InstantiateInitObject();

        GameEventSystem.Instance.OnPushBackTitleBtn += BackToTitle;

        GameEventSystem.Instance.OnPushSceneBtn += ChangeToNextScene;
    }

    public override void StateUpdate()
    {
    }

    public override void StateEnd()
    {
        GameEventSystem.Instance.OnPushBackTitleBtn -= BackToTitle;
        GameEventSystem.Instance.OnPushSceneBtn -= ChangeToNextScene;
    }

    void BackToTitle()
    {
        GameEventSystem.Instance.OnPushBackTitleBtn -= BackToTitle;
        TTSCtrl.Instance.StopTTS();
        AudioManager.Instance.GetComponent<GetAudioSource>().PlayButtonSound();
        m_Controler.SetState(new TitleState(m_Controler), "TitleState");
    }


    void ChangeToNextScene(int level)
    {
        GameEventSystem.Instance.OnPushSceneBtn -= ChangeToNextScene;
        //TTSCtrl.Instance.StopTTS();////////////////////////////////////////////////////
        //AudioManager.Instance.GetComponent<GetAudioSource>().PlayButtonSound();
        m_Controler.SetState(new MainGameState(m_Controler), "MainGameState");
        MainGameManager.NowLevel = level;
    }
}
