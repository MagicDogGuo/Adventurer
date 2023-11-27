using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAudioSource : MonoBehaviour {

    [SerializeField]
    [Header("背景音樂標題/主選單")]
    AudioClip MainMenuBGM;

    [SerializeField]
    [Header("背景音樂晴天A")]
    AudioClip MainGameSunBGM_A;
    [SerializeField]
    [Header("背景音樂晴天B")]
    AudioClip MainGameSunBGM_B;

    [SerializeField]
    [Header("背景音樂雨天A")]
    AudioClip MainGameRainBGM_A;
    [SerializeField]
    [Header("背景音樂雨天B")]
    AudioClip MainGameRainBGM_B;

    [SerializeField]
    [Header("背景音樂Boss")]
    AudioClip MainGameBossBGM;

    #region UI
    [SerializeField]
    [Header("開始音效")]
    AudioClip StartSound;
    [SerializeField]
    [Header("離開音效")]
    AudioClip ExitSound;
    [SerializeField]
    [Header("返回音效")]
    AudioClip ReturnSound;

    [SerializeField]
    [Header("按鈕音效")]
    AudioClip ButtonSound;

    [SerializeField]
    [Header("叉叉音效")]
    AudioClip CrossSound;

    [SerializeField]
    [Header("拖曳音效")]
    AudioClip DragSound;
    [SerializeField]
    [Header("拖曳結束音效")]
    AudioClip DragEndSound;
    [SerializeField]
    [Header("執行編程按鈕音效")]
    AudioClip ExecuteCodeSound;
    #endregion

    [SerializeField]
    [Header("狗開心音效")]
    AudioClip HappyDogSound;
    [SerializeField]
    [Header("狗困惑音效")]
    AudioClip ConfuseDogSound;
    [SerializeField]
    [Header("狗飽足音效")]
    AudioClip FullDogSound;

    public enum BGMKinds
    {
        MainMenu,
        MainGameSunA,
        MainGameRainA,

    }

    public void PlayBGM(BGMKinds bGMKinds)
    {
        
        switch (bGMKinds)
        {
            case BGMKinds.MainMenu:
                AudioManager.Instance.PlayLoopMusic(MainMenuBGM);
                break;
            case BGMKinds.MainGameSunA:
                AudioManager.Instance.PlayLoopMusic(MainGameSunBGM_A);
                break;
            case BGMKinds.MainGameRainA:
                AudioManager.Instance.PlayLoopMusic(MainGameRainBGM_A);
                break;

        }
    }


    #region UI
    public void PlayStartSound()
    {
        AudioManager.Instance.PlayVoice(StartSound);
    }
    public void PlayExitSound()
    {
        AudioManager.Instance.PlayVoice(ExitSound);
    }
    public void PlayReturnSound()
    {
        AudioManager.Instance.PlayVoice(ReturnSound);
    }

    public void PlayButtonSound()
    {
        AudioManager.Instance.PlayVoice(ButtonSound);
    }

    public void PlayCrossSound()
    {
        AudioManager.Instance.PlayVoice(CrossSound);
    }

    public void PlayDragSound()
    {
        AudioManager.Instance.PlayVoice(DragSound);
    }
    
    public void PlayDragEndSound()
    {
        AudioManager.Instance.PlayVoice(DragEndSound);
    }

    public void PlayExecuteCodeSound()
    {
        AudioManager.Instance.PlayVoice(ExecuteCodeSound);
    }

    #endregion


    public void PlaySusessedSound()
    {
        //Debug.Log(CurrLanguage.currLanguage);
        AudioClip ac = DatabaseManager.Instance.FetchFromSrting_ID_GameContentAudioRow("ResultSound_01").Audio_Content;
        AudioManager.Instance.PlayVoice(ac);
    }
    public void PlayFailSound()
    {
        AudioClip ac = DatabaseManager.Instance.FetchFromSrting_ID_GameContentAudioRow("ResultSound_02").Audio_Content;
        AudioManager.Instance.PlayVoice(ac);
    }
    //public void PlaySettleBtnSound()
    //{
    //    AudioManager.Instance.PlayVoice(SettleBtnSound);
    //}


    public enum DogSounds
    {
        happy,
        confuse,
        full
    }

    public void PlayDogSound(DogSounds dogSounds)
    {
        switch (dogSounds)
        {
            case DogSounds.happy:
                AudioManager.Instance.PlayVoice(HappyDogSound);
                break;
            case DogSounds.confuse:
                AudioManager.Instance.PlayVoice(ConfuseDogSound);

                break;
            case DogSounds.full:
                AudioManager.Instance.PlayVoice(FullDogSound);

                break;
        }
    }
}
