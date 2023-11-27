using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultUIComp : MonoBehaviour {

    //[SerializeField]
    //TextMeshProUGUI ResultText;

    [SerializeField]
    Button MenuBtn;

    [SerializeField]
    Button NextLevelButton;
    [SerializeField]
    Button RetryBtn;
    //[SerializeField]
    //Button FailTipButton;

    [SerializeField]
    Image winItemBG;
    [SerializeField]
    Image loseBG;
    [SerializeField]
    Image winNotItemBG;

    [SerializeField]
    RectTransform TileGroup;

    [SerializeField]
    RectTransform TileBox;

    [SerializeField]
    GameObject FailTipCanvasObj;
    [SerializeField]
    Sprite FailTipUnLightIconImg;
    [SerializeField]
    Sprite FailTipLightIconImg;

    public ResultBoxData resultBoxData;

    void Start() {
        MenuBtn.onClick.AddListener(delegate { if (GameEventSystem.Instance.OnPushMenuBtn != null) GameEventSystem.Instance.OnPushMenuBtn(); AudioManager.Instance.GetComponent<GetAudioSource>().PlayButtonSound(); });
        NextLevelButton.onClick.AddListener(delegate { if (GameEventSystem.Instance.OnPushNextLevelBtn != null) GameEventSystem.Instance.OnPushNextLevelBtn(); AudioManager.Instance.GetComponent<GetAudioSource>().PlayButtonSound(); });
        RetryBtn.onClick.AddListener(delegate { if (GameEventSystem.Instance.OnPushRestartBtn != null) GameEventSystem.Instance.OnPushRestartBtn(); AudioManager.Instance.GetComponent<GetAudioSource>().PlayButtonSound(); });
        //FailTipButton.onClick.AddListener(()=>
        //{
        //    AudioManager.Instance.GetComponent<GetAudioSource>().PlayButtonSound();
        //    InstanceFailTipCanvas();
        //});
    }


    public void SuccessUI(List<string> objsInfo)
    {
        AudioManager.Instance.GetComponent<GetAudioSource>().PlaySusessedSound();

        int ItemCount = SetItemBox(objsInfo);
        if (ItemCount <= 0)
            SetShowUI(ResultUIType.Win_NotItem);
        else
            SetShowUI(ResultUIType.Win_HaveItem);


        if (MainGameManager.NowLevel == MainGameManager.Instance.MapGridObjArray.Count)
        {
            NextLevelButton.gameObject.SetActive(false);
        }
        //StartCoroutine(IE_DelayTTS("GameOver_01", 0.409f));
        
        Mibo.motionPlay("666_BA_RzArmS90");

    }
    public void FailUI()
    {
        AudioManager.Instance.GetComponent<GetAudioSource>().PlayFailSound();

        SetShowUI(ResultUIType.Lose);

       //StartCoroutine(IE_DelayTTS("GameOver_02", 0.449f));

    }
   
    IEnumerator IE_DelayTTS(string s,float sec)
    {
        yield return new WaitForSeconds(sec);
        TTSCtrl.Instance.StartTTS(DatabaseManager.Instance.FetchFromString_ID_GameContentTTSRow(s).Content);
    }

    private int SetItemBox(List<string> objsInfo)
    {
        int count = objsInfo.Count;

        for (int i = 0; i < count; i++)
        {
            var obj = Instantiate(TileBox);
            obj.gameObject.SetActive(true);
            obj.SetParent(TileGroup);

            var t = obj.GetComponentInChildren<Image>();
            t.sprite = FindItem(objsInfo[i]);
            t.SetNativeSize();
        }
        return count;
    }
    private void SetItemBox(int emptyBoxCount)
    {
        
        for (int i = 0; i < emptyBoxCount; i++)
        {
            var obj = Instantiate(TileBox);
            obj.gameObject.SetActive(true);
            obj.SetParent(TileGroup);
            
        }

    }
    private Sprite FindItem(string name)
    {
        if (resultBoxData == null)
        {
            Debug.LogError("Result Box Is Null");
            return null;
        }
        
        for (int i = 0; i < resultBoxData.BoxDatas.Count; i++)
        {
            if (resultBoxData.BoxDatas[i].ItmeKey.ToString() == name)
                return resultBoxData.BoxDatas[i].ItemSprite;
           
        }

        Debug.LogError("Can't Find : "+name + " In ResultBoxData ");
        return null;

    }

    private void SetShowUI(ResultUIType resultUIType)
    {
        switch (resultUIType)
        {
            case ResultUIType.Win_HaveItem:
                winItemBG.gameObject.SetActive(true);
                loseBG.gameObject.SetActive(false);
                winNotItemBG.gameObject.SetActive(false);

                MenuBtn.gameObject.SetActive(true);
                NextLevelButton.gameObject.SetActive(true);
                RetryBtn.gameObject.SetActive(true);
                //FailTipButton.gameObject.SetActive(false);
                break;

            case ResultUIType.Win_NotItem:
                winItemBG.gameObject.SetActive(false);
                loseBG.gameObject.SetActive(false);
                winNotItemBG.gameObject.SetActive(true);

                MenuBtn.gameObject.SetActive(true);
                NextLevelButton.gameObject.SetActive(true);
                RetryBtn.gameObject.SetActive(true);
                //FailTipButton.gameObject.SetActive(false);

                break;
            case ResultUIType.Lose:
                winItemBG.gameObject.SetActive(false);
                loseBG.gameObject.SetActive(true);
                winNotItemBG.gameObject.SetActive(false);


                MenuBtn.gameObject.SetActive(true);
                NextLevelButton.gameObject.SetActive(false);
                RetryBtn.gameObject.SetActive(true);
                //FailTipButton.gameObject.SetActive(true);
                //StartCoroutine(IE_delayChangeImgFailTip());
                break;
        }
    }

    IEnumerator IE_delayChangeImgFailTip()
    {
        Debug.Log("==================="+ AudioManager.Instance.VoiceSource.isPlaying);
        while (AudioManager.Instance.VoiceSource.isPlaying)
        {
            yield return 0;
        }
        //Image failImg = FailTipButton.GetComponent<Image>();
        //while (true)
        //{
        //    //failImg.sprite = FailTipLightIconImg;
        //    //yield return new WaitForSeconds(0.2f);
        //    //failImg.sprite = FailTipUnLightIconImg;
        //    //yield return new WaitForSeconds(0.2f);
        //}
    }

    private enum ResultUIType
    {
        Win_NotItem,
        Win_HaveItem,
        Lose

    }

    void InstanceFailTipCanvas()
    {
        Instantiate(FailTipCanvasObj);
    }
}
