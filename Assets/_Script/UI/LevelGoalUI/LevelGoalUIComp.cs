using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelGoalUIComp : MonoBehaviour {

    [SerializeField]
    Button CloseLevelGaolBtn;

    [SerializeField]
    LevelGoalRecord LevelGoalRecord;

    [SerializeField]
    GameObject BoxGroup;

    [SerializeField]
    GameObject Box;

    [SerializeField]
    GoalObjs BoxObjs;

    GameObject ControlBlockUICanvas;
 
    void Start () {

        GetComponent<Canvas>().worldCamera = Camera.main;    

        if (CloseLevelGaolBtn == null)
            Debug.LogError("Can't find EndGIF Button");
        else
            CloseLevelGaolBtn.onClick.AddListener(() => {
                CloseLevelGoal();
                AudioManager.Instance.GetComponent<GetAudioSource>().PlayButtonSound();
                if (GameEventSystem.Instance.OnPushLevelGoalCloseBtn!=null)
                    GameEventSystem.Instance.OnPushLevelGoalCloseBtn();
            });

        CreateGoalContent();

        if (MainGameManager.Instance.CurrentState != MainGameStateControl.GameFlowState.LevelGoal)
        {
            ControlBlockUICanvas = GameObject.Find("ControlBlockUICanvas(Clone)");
            if (ControlBlockUICanvas != null)
                ControlBlockUICanvas.SetActive(false);
        }
    }

    private void CreateGoalContent()
    {
        int level = MainGameManager.NowLevel;
        //TODO 這邊改成外部Dictionary，讓他可以新增關卡的時候用KEY來代替INDEX，才不會想測試哪一關的時候會變成生成其他關的教學
        if (LevelGoalRecord.LevelGaolDatas == null || LevelGoalRecord.LevelGaolDatas[level-1] == null || LevelGoalRecord.LevelGaolDatas.Count <= 0)
        {
            Debug.LogError("Level Goal not in GoalList");
            return;
        }
       
        LevelGaolData levelGaolData = LevelGoalRecord.LevelGaolDatas[level - 1];
        int nowLevelGoalAmount = levelGaolData.m_GoalObjects.Length;

        //目標BOX數量
        //把目標種類放入BOX
        if(nowLevelGoalAmount < 4 && nowLevelGoalAmount > 0)
        {
            foreach (var m_goalObjects in levelGaolData.m_GoalObjects)
            {
                InstanceBoxObjs(m_goalObjects.GoalObjectEnums, BoxGroup.transform);
            }
        }
        else
        {
            Debug.Log("==============目標個數沒有在1~3的範圍!==============");
        }

        //目標TTS
        string levelTTSID = levelGaolData.PageTTSID;
        string tts = DatabaseManager.Instance.FetchFromSrting_ID_LevelGoalTTSRow(levelTTSID).Content;
        TTSCtrl.Instance.StartTTS(tts);
        //GameGoalContent_01
    }

    public void CloseLevelGoal()
    {
        TTSCtrl.Instance.StopTTS();
        Debug.Log("Close LevelGoal");
        Time.timeScale = 1;      
        Destroy(transform.gameObject);
    }

    private void OnDisable()
    {
        if (ControlBlockUICanvas != null)
            ControlBlockUICanvas.SetActive(true);

        CloseLevelGaolBtn.onClick.RemoveAllListeners();
    }


    void InstanceBoxObjs(GoalObjectEnum goalObjectEnum, Transform parentObjTras)
    {
        GameObject mBox = null;
        mBox = Instantiate(Box, parentObjTras);
        BoxComp boxComp = mBox.GetComponent<BoxComp>();
        Image mBoxObjImg = boxComp.BoxObjImg;
        string levelContentID = null;
        
        switch (goalObjectEnum)
        {
            case GoalObjectEnum.Bettery:
                mBoxObjImg.sprite = BoxObjs.Bettery;
                levelContentID = GoalObjs.BetteryString;
                break;
            case GoalObjectEnum.FeedDog:
                mBoxObjImg.sprite = BoxObjs.FeedDog;
                levelContentID = GoalObjs.FeedDogString;
                break;
            case GoalObjectEnum.FindDog:
                mBoxObjImg.sprite = BoxObjs.FindDog;
                levelContentID = GoalObjs.FindDogString;
                break;
            case GoalObjectEnum.Toy:
                mBoxObjImg.sprite = BoxObjs.Toy;
                levelContentID = GoalObjs.ToyString;
                break;
            case GoalObjectEnum.Umbrella:
                mBoxObjImg.sprite = BoxObjs.Umbrella;
                levelContentID = GoalObjs.UmbrellaString;
                break;
            case GoalObjectEnum.DigHole:
                mBoxObjImg.sprite = BoxObjs.DigHole;
                levelContentID = GoalObjs.DigHoleString;
                break;
            case GoalObjectEnum.FindThing:
                mBoxObjImg.sprite = BoxObjs.TakeThing;
                levelContentID = GoalObjs.TakeThingString;
                break;
            case GoalObjectEnum.MoveFront:
                mBoxObjImg.sprite = BoxObjs.MoveFront;
                levelContentID = GoalObjs.MoveFrontString;
                break;
            case GoalObjectEnum.MoveBack:
                mBoxObjImg.sprite = BoxObjs.MoveBack;
                levelContentID = GoalObjs.MoveBackString;
                break;
            case GoalObjectEnum.MoveRight:
                mBoxObjImg.sprite = BoxObjs.MoveRight;
                levelContentID = GoalObjs.MoveRightString;
                break;
            case GoalObjectEnum.MoveLeft:
                mBoxObjImg.sprite = BoxObjs.MoveLeft;
                levelContentID = GoalObjs.MoveLeftString;
                break;
            case GoalObjectEnum.East:
                mBoxObjImg.sprite = BoxObjs.East;
                levelContentID = GoalObjs.EastString;
                break;
            case GoalObjectEnum.West:
                mBoxObjImg.sprite = BoxObjs.West;
                levelContentID = GoalObjs.WestString;
                break;
            case GoalObjectEnum.South:
                mBoxObjImg.sprite = BoxObjs.South;
                levelContentID = GoalObjs.SouthString;
                break;
            case GoalObjectEnum.North:
                mBoxObjImg.sprite = BoxObjs.North;
                levelContentID = GoalObjs.NorthString;
                break;
            default:
                Debug.Log("==============沒有目標物件!=============");
                break;
        }

        string boxContent = DatabaseManager.Instance.FetchFromSrting_ID_GameContentTextRow(levelContentID).Content;
        boxComp.BoxObjTxt.text = boxContent;
    }
}


