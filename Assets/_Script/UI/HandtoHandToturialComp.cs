using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HandtoHandToturialComp : MonoBehaviour {

    [SerializeField]
    Sprite BackToTileSprite;
    [SerializeField]
    Sprite GoToFirstLevelSprte;

    [SerializeField]
    Button PushToturailCloseBtn;

    [SerializeField]
    Image FocusBlockImg;

    [SerializeField]
    Image FocusBlockAndBarImg;

    [SerializeField]
    Image FocusStartBtnImg;

    [SerializeField]
    Image FocusStopBtnImg;

    [SerializeField]
    Image FocusPauseBtnImg;

    [SerializeField]
    Image FocusPauseUICnavasImg;

    [SerializeField]
    Animator TipAnim;

    [SerializeField]
    GameObject BlockBlockImage;

    ControlBlockUIComp controlBlockUIComp;

    int m_ToturailCompleteAmount = 0;

    void Start () {
        if (SingletonLoader.Instance.GetComponent<GameLoop>().IsOpenToturailByMenu == true)
        {
            PushToturailCloseBtn.GetComponent<Image>().sprite = BackToTileSprite;
            PushToturailCloseBtn.gameObject.SetActive(true);
            PushToturailCloseBtn.onClick.AddListener(CloseToturail);
        }
        else
        {
            PushToturailCloseBtn.GetComponent<Image>().sprite = GoToFirstLevelSprte;
            PushToturailCloseBtn.gameObject.SetActive(false);
        }

        DontDestroyOnLoad(this.gameObject);
        HandtoHandToturialComp[] handtoHandToturialComps = GameObject.FindObjectsOfType<HandtoHandToturialComp>();
        if (handtoHandToturialComps.Length > 1)
        {
            Destroy(this.gameObject);
        }
        m_ToturailCompleteAmount = 0;

        TipAnim.gameObject.SetActive(false);
    }


    private void OnDestroy()
    {
        Time.timeScale = 1;
        if (MainGameManager.Instance.ExecuteBlockObjs != null)
        {
            MainGameManager.Instance.ExecuteBlockObjs.GetComponent<ExecuteBlock>().NowPlayBlockSequenceEvent -= NowExeBlock;
        }
        GameEventSystem.Instance.OnPushStopCodeBtn -= OnPushStopCodeBtn;

        //第一次進第一關紀錄
        PlayerPrefs.SetString("Record", "AllreadyInLevel");

        SingletonLoader.Instance.GetComponent<GameLoop>().IsOpenToturailByMenu = false;
    }

    void CloseToturail()
    {
        Time.timeScale = 1;
        TTSCtrl.Instance.StopTTS();
        AudioManager.Instance.GetComponent<GetAudioSource>().PlayButtonSound();

        Debug.Log("Close Toturial");

        //if (GameEventSystem.Instance.OnPushToturailCloseBtn != null)
        //    GameEventSystem.Instance.OnPushToturailCloseBtn();
        //第一次進第一關紀錄
        PlayerPrefs.SetString("Record", "AllreadyInLevel");
        //回到選單
        Destroy(this.gameObject,0.1f);
        GameEventSystem.Instance.OverToturialEvent();

    }

    string TorurailContrntTTS(string s)
    {
      return DatabaseManager.Instance.FetchFromSrting_ID_HandToHandTutorialTTSRow(s).Content;
    }

    /// <summary>
    /// ToturialState
    /// </summary>
    /// <param name="toturialStateInToturailDelegate"></param>
    public void ToturialStateInToturail(ToturialStateInToturailDelegate toturialStateInToturailDelegate)
    {
        if (m_ToturailCompleteAmount < 1)
        {
            StartCoroutine(IEToturialStateInToturail(toturialStateInToturailDelegate));
        }
        else
        {
            Debug.Log("ToturialState錯誤");
        }
    }

    IEnumerator IEToturialStateInToturail(ToturialStateInToturailDelegate toturialStateInToturailDelegate )
    {
        yield return new WaitForEndOfFrame();
        TTSCtrl.Instance.StartTTS(TorurailContrntTTS("HandtoHandTutorial_01"));//"只有我的話會不知道該怎麼完成任務，所以要拜託你告訴我該做些什麼才好！接下來要教教你，該怎麼把指示傳達給我，請你一定要注意聽哦！"
        while (!TTSCtrl.Instance.TTSComplete) yield return 0;
        toturialStateInToturailDelegate();
    }
  

    /// <summary>
    /// ComposeBlockState
    /// </summary>
    /// <param name="toturialStateInToturailDelegate"></param>
    public void ComposeBlockStateInToturail(ComposeBlockStateInToturailDelegate toturialStateInToturailDelegate)
    {
        //關閉拿取方塊
        GameObject controlBlockUICanvases = MainGameManager.Instance.ControlBlockUICanvases;
        if(controlBlockUICanvases.GetComponentInChildren<TakeItemBlock>()!=null)
            controlBlockUICanvases.GetComponentInChildren<TakeItemBlock>().gameObject.SetActive(false);

        
        if (m_ToturailCompleteAmount < 1)
        {
            StartCoroutine(IEComposeBlockStateInToturail(toturialStateInToturailDelegate));
        }
        else
        {
            StartCoroutine(IEComposeBlockStateInToturailSecondTime(toturialStateInToturailDelegate));
        }
    }

    IEnumerator IEComposeBlockStateInToturail(ComposeBlockStateInToturailDelegate toturialStateInToturailDelegate)
    {
        yield return new WaitForEndOfFrame();
        
        //找到主遊戲UI
        controlBlockUIComp = MainGameManager.Instance.ControlBlockUICanvases.GetComponentInChildren<ControlBlockUIComp>();
        controlBlockUIComp.PauseGameBtn.enabled = false;
        foreach (var m_allBlockGOs in MainGameManager.Instance.AllblockInScene())
        {
            if (m_allBlockGOs != null) m_allBlockGOs.GetComponent<Block>().isCanMoveBlock = false;
        }
        controlBlockUIComp.StartCodeBtn.enabled = false;


        TTSCtrl.Instance.StartTTS(TorurailContrntTTS("HandtoHandTutorial_02"));
        FocusBlockImg.enabled = true;
        while (!TTSCtrl.Instance.TTSComplete) yield return 0;


        TTSCtrl.Instance.StartTTS(TorurailContrntTTS("HandtoHandTutorial_03"));//"我需要你按住方塊，然後拖曳到上面的框框放開，確定方塊有連接在一起！現在請你放三個挖土方塊到編輯框可以嗎？"
        FocusBlockAndBarImg.enabled = true;
        FocusBlockImg.enabled = false;
        //播放動畫
        TipAnim.gameObject.SetActive(true);
        BlockBlockImage.SetActive(true);

        while (!TTSCtrl.Instance.TTSComplete) yield return 0;
        foreach (var m_allBlockGOs in MainGameManager.Instance.AllblockInScene())
        {
            if (m_allBlockGOs != null) m_allBlockGOs.GetComponent<Block>().isCanMoveBlock = true;
        }


        while (!Input.GetMouseButtonDown(0))
        {
            //Debug.Log("000000000000000");
            yield return 0;
        }

        //停止動畫
        TipAnim.gameObject.SetActive(false);
        yield return new WaitForEndOfFrame();
        BlockBlockImage.SetActive(false);

        //判斷現在方塊數
        ArrayList startBlockArrayList = controlBlockUIComp.StartBlock.GetComponent<Block>().DescendingBlocksForStartBlock();
        while (startBlockArrayList.Count < 1)
        {
            startBlockArrayList = controlBlockUIComp.StartBlock.GetComponent<Block>().DescendingBlocksForStartBlock();
            yield return 0;
        }
        FocusBlockAndBarImg.enabled = false;
        FocusStartBtnImg.enabled = true;
        foreach (var m_allBlockGOs in MainGameManager.Instance.AllblockInScene())
        {
            if (m_allBlockGOs != null) m_allBlockGOs.GetComponent<Block>().isCanMoveBlock = false;
        }

        TTSCtrl.Instance.StartTTS(TorurailContrntTTS("HandtoHandTutorial_04"));//"連接好的指令傳達給我，只要按下這個播放鍵就可以囉！是不是很簡單呢？"
        while (!TTSCtrl.Instance.TTSComplete) yield return 0;
        controlBlockUIComp.StartCodeBtn.enabled = true;



        //State會自動換
        //Debug.Log("================換到Read================");
        //toturialStateInToturailDelegate();
    }

    IEnumerator IEComposeBlockStateInToturailSecondTime(ComposeBlockStateInToturailDelegate toturialStateInToturailDelegate)
    {
        controlBlockUIComp.StartCodeBtn.enabled = false;
        foreach (var m_allBlockGOs in MainGameManager.Instance.AllblockInScene())
        {
            if (m_allBlockGOs != null) m_allBlockGOs.GetComponent<Block>().isCanMoveBlock = false;
        }
        controlBlockUIComp.transform.parent.GetComponentInChildren<ScrollRect>().enabled = false;

        yield return new WaitForEndOfFrame();
        GameEventSystem.Instance.OnPauseGameBtn += OnPauseGameBtn;
        TTSCtrl.Instance.StartTTS(TorurailContrntTTS("HandtoHandTutorial_06"));//"當你不想玩或想重新確認任務目標時，可以按下遊戲暫停！"
        while (!TTSCtrl.Instance.TTSComplete) yield return 0;
        controlBlockUIComp.PauseGameBtn.enabled = true;

        while (!isOverToturail)
        {
            yield return 0;
        }
        toturialStateInToturailDelegate();

        MainGameManager.Instance.DestroyInitMapObject();
        MainGameManager.Instance.DestroyInitObject();

        //回到選單
        if (SingletonLoader.Instance.GetComponent<GameLoop>().IsOpenToturailByMenu == true)
        {
            GameEventSystem.Instance.OverToturialEvent();
        }

        Destroy(this.gameObject);       
    }

    bool isOverToturail = false;

    void OnPauseGameBtn()
    {
        StartCoroutine(IEOnPauseGameBtn());
    }

    IEnumerator IEOnPauseGameBtn()
    {
        FocusPauseBtnImg.enabled = false;
        FocusPauseUICnavasImg.enabled = true;

        GameEventSystem.Instance.OnPauseGameBtn -= OnPauseGameBtn;

        yield return new WaitForEndOfFrame();
        PauseGameUIComp pauseGameUIComp = MainGameManager.Instance.PauseGameUICanvases.GetComponentInChildren<PauseGameUIComp>();
        pauseGameUIComp.ResumeBtn.enabled = false;
        pauseGameUIComp.LevelGoalBtn.enabled = false;
        pauseGameUIComp.MenuBtn.enabled = false;

        TTSCtrl.Instance.StartTTS(TorurailContrntTTS("HandtoHandTutorial_07"));//"在這裡還可以回到關卡選單，重新選擇想要挑戰的任務哦！"
        while (!TTSCtrl.Instance.TTSComplete) yield return 0;

        TTSCtrl.Instance.StartTTS(TorurailContrntTTS("HandtoHandTutorial_08"));//"聽到這裡，你一定已經想趕快開始跟我一起執行任務了，在遊戲首頁隨時都可以再看一次教學，讓我們快去挑戰有趣的編程任務吧！
        while (!TTSCtrl.Instance.TTSComplete) yield return 0;

        Time.timeScale = 1;
        Destroy(pauseGameUIComp.transform.parent.gameObject);
        Destroy(MainGameManager.Instance.ControlBlockUICanvases);
        MainGameManager.Instance.IsReplayByStopCodeBtn = false;
        Debug.Log("重玩或回選單");
        yield return new WaitForEndOfFrame();
        isOverToturail = true;
    }

    /// <summary>
    /// ReadBlock
    /// </summary>
    /// <param name="toturialStateInToturailDelegate"></param>
    public void ReadBlockStateInToturail(ReadBlockStateInToturailDelegate readBlockStateInToturailDelegate)
    {
        if (m_ToturailCompleteAmount <1)
        {
            StartCoroutine(IEReadBlockStateInToturail(readBlockStateInToturailDelegate));
        }
        else
        {
            Debug.Log("ReadBlockState錯誤");
        }
    }

    IEnumerator IEReadBlockStateInToturail(ReadBlockStateInToturailDelegate readBlockStateInToturailDelegate)
    {
        yield return new WaitForEndOfFrame();

        controlBlockUIComp.StopCodeBtn.enabled = false;
        FocusStartBtnImg.enabled = false;

        yield return new WaitForSeconds(0.5f);
        MainGameManager.Instance.ExecuteBlockObjs.GetComponent<ExecuteBlock>().NowPlayBlockSequenceEvent += NowExeBlock;

        //readBlockStateInToturailDelegate();
    }
    int nowExeBlockNum = -1;
    void NowExeBlock(int nowBlockNum)
    {
        nowExeBlockNum = nowBlockNum;
        //Debug.Log("===============" + nowBlockNum);
    }

    /// <summary>
    /// ExecuteBlockState
    /// </summary>
    /// <param name="toturialStateInToturailDelegate"></param>
    public void ExecuteBlockStateInToturail(ExecuteBlockStateInToturailDelegate executeBlockStateInToturailDelegate )
    {
        if (m_ToturailCompleteAmount < 1)
        {
            StartCoroutine(IEExecuteBlockStateInToturail(executeBlockStateInToturailDelegate));
        }
        else
        {
            Debug.Log("ExecuteBlockState錯誤");
        }
        m_ToturailCompleteAmount++;
    }

    IEnumerator IEExecuteBlockStateInToturail(ExecuteBlockStateInToturailDelegate executeBlockStateInToturailDelegate)
    {
        yield return new WaitForEndOfFrame();
        while (nowExeBlockNum < 0)//第一個就停
        {
            yield return 0;
        }
        yield return new WaitForSeconds(2f);
        StopTime();
        FocusStopBtnImg.enabled = true;
        GameEventSystem.Instance.OnPushStopCodeBtn += OnPushStopCodeBtn;
        controlBlockUIComp.StartCodeBtn.enabled = false;

        TTSCtrl.Instance.StartTTS(TorurailContrntTTS("HandtoHandTutorial_05"));//"按下播放後，原本的按鈕變得不一樣了！如果在執行任務時發現傳達給我的指令不對，隨時可以按下這個按鍵重來，現在請你按按看吧！"
        while (!TTSCtrl.Instance.TTSComplete) yield return 0;
        controlBlockUIComp.StopCodeBtn.enabled = true;


        //executeBlockStateInToturailDelegate();
    }
    void OnPushStopCodeBtn()
    {
        controlBlockUIComp.StopCodeBtn.enabled = false;
        StartTime();
        FocusStopBtnImg.enabled = false;
        FocusPauseBtnImg.enabled = true;

        GameEventSystem.Instance.OnPushStopCodeBtn -= OnPushStopCodeBtn;
    }
    void StopTime()
    {
        Time.timeScale = 0;
    }
    void StartTime()
    {
        Time.timeScale = 1;
    }


}

public delegate void ToturialStateInToturailDelegate();
public delegate void ComposeBlockStateInToturailDelegate();
public delegate void ReadBlockStateInToturailDelegate();
public delegate void ExecuteBlockStateInToturailDelegate();


