using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUIComp : MonoBehaviour {

    [SerializeField]
    Button StartGameBtn;

    [SerializeField]
    Button ExitGameBtn;

    [SerializeField]
    Button InfoBtn;

    [SerializeField]
    Button ToturailBtn;

    [SerializeField]
    GameObject LoadingUIObj;

    [SerializeField]
    GameObject InfoUICanvas;

    [SerializeField]
    GameObject LoadingToturialObj;

    void Start () {
        StartGameBtn.onClick.AddListener(delegate { Instantiate(LoadingUIObj); StartCoroutine(IEDelayStart()); });

        ExitGameBtn.onClick.AddListener(delegate { if (GameEventSystem.Instance.OnPushExitGameBtn != null) GameEventSystem.Instance.OnPushExitGameBtn(); });

        InfoBtn.onClick.AddListener(OnPushInfoBtn);

        //PlayerPrefs.DeleteKey("Record");

        //一開始不出現教學按鈕
        if (!PlayerPrefs.HasKey("Record"))
        {
            Debug.Log("無教學存檔");
            ToturailBtn.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("有教學存檔");
            ToturailBtn.gameObject.SetActive(true);
        }

        ToturailBtn.onClick.AddListener(delegate { SingletonLoader.Instance.GetComponent<GameLoop>().IsOpenToturailByMenu = true;
            OnPushToturialBtn();});

        //Debug.Log(DatabaseManager.Instance.FetchFromID_LevelGoalTTSRow(1).Content);
        //Debug.Log(DatabaseManager.Instance.FetchFromID_TutorialTTSRow(1).Content);
        //Debug.Log(DatabaseManager.Instance.FetchRoleFromLevel_ResultDecideRow(1).Role_IsOpenUmbrella);
        //Debug.Log(DatabaseManager.Instance.FetchFromID_RobotmotionRow(1).Motion);
        //Debug.Log(DatabaseManager.Instance.FetchFromID_GameContentTextRow(1).Content);
        //Debug.Log(DatabaseManager.Instance.FetchFromID_GameContentImageRow(1).Sprite_ContentName);
    }

    void OnPushInfoBtn()
    {
        AudioManager.Instance.GetComponent<GetAudioSource>().PlayButtonSound();

        Instantiate(InfoUICanvas);
    }

    IEnumerator IEDelayStart()
    {
        AudioManager.Instance.GetComponent<GetAudioSource>().PlayButtonSound();
        TTSCtrl.Instance.StartTTS(DatabaseManager.Instance.FetchFromString_ID_GameContentTTSRow("FallWarning_01").Content);
        while (!TTSCtrl.Instance.TTSComplete)
        {
            yield return 0;
        }
        if (GameEventSystem.Instance.OnPushStartGameBtn != null) GameEventSystem.Instance.OnPushStartGameBtn();
    }


    void OnPushToturialBtn()
    {
        Instantiate(LoadingToturialObj);
        StartCoroutine(DelayLoadScene());
    }

    IEnumerator DelayLoadScene()
    {
        yield return new WaitForSeconds(0.5f);
        GameEventSystem.Instance.OnPushToturailBtn(1);

    }
}
