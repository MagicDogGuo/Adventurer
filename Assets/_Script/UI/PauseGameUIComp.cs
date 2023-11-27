using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGameUIComp : MonoBehaviour {

    public Button ResumeBtn;
    public Button LevelGoalBtn;
    //public Button ToturialBtn;
    public Button MenuBtn;

    private void Start()
    {
        StartCoroutine(PauseStateFlow());

        ResumeBtn.onClick.AddListener(OnResumeEvent);
        LevelGoalBtn.onClick.AddListener(OnLevelGoalEvent);
        //ToturialBtn.onClick.AddListener(OnToutrialEvent);
        MenuBtn.onClick.AddListener(OnMenuEvent);

        //if (!MainGameManager.Instance.IsHaveToturial)
        //    ToturialBtn.interactable = false;
    }

    private void OnDisable()
    {
        ResumeBtn.onClick.RemoveAllListeners();
        LevelGoalBtn.onClick.RemoveAllListeners();
        //ToturialBtn.onClick.RemoveAllListeners();
        MenuBtn.onClick.RemoveAllListeners();
    }

    public void OnPauseEvent()
    {
        Debug.Log("Pause");
        Time.timeScale = 0;
    }

    public void OnResumeEvent()
    {
        Debug.Log("Resume");
        Time.timeScale = 1;

        AudioManager.Instance.GetComponent<GetAudioSource>().PlayButtonSound();
        Destroy(transform.parent.gameObject);
    }

    public void OnToutrialEvent()
    {
        //OnResumeEvent();
        MainGameManager.Instance.InstantiateHandtoHandToturialCanvas();

        AudioManager.Instance.GetComponent<GetAudioSource>().PlayButtonSound();
        Destroy(transform.parent.gameObject);
    }

    public void OnLevelGoalEvent()
    {
        //OnResumeEvent();
        MainGameManager.Instance.InstantiateLevelGoalCanvas();

        AudioManager.Instance.GetComponent<GetAudioSource>().PlayButtonSound();
        Destroy(transform.parent.gameObject);
    }

    public void OnMenuEvent()
    {
      
        if (GameEventSystem.Instance.OnPushMenuBtn != null)
        {
            GameEventSystem.Instance.OnPushMenuBtn(); 
        }
        AudioManager.Instance.GetComponent<GetAudioSource>().PlayButtonSound();
        OnResumeEvent();
    }

    IEnumerator PauseStateFlow()
    {
        yield return null;
        OnPauseEvent();

    }

}
