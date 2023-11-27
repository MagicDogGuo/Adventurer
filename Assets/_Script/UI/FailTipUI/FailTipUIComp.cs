using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FailTipUIComp : MonoBehaviour {

    [SerializeField]
    Button CloseLevelGaolBtn;

    [SerializeField]
    LevelGoalRecord LevelGoalRecord;

    [SerializeField]
    GameObject BoxGroup;

    [SerializeField]
    GameObject TipBox;

    [SerializeField]
    GoalObjs BoxObjs;

    GameObject ControlBlockUICanvas;

    void Start()
    {
       // GetComponent<Canvas>().worldCamera = Camera.main;

        ShowFailItem();

        if (CloseLevelGaolBtn == null)
            Debug.LogError("Can't find EndGIF Button");
        else
            CloseLevelGaolBtn.onClick.AddListener(() => {
                CloseLevelGoal();
                AudioManager.Instance.GetComponent<GetAudioSource>().PlayButtonSound();
            });

        if (MainGameManager.Instance.CurrentState != MainGameStateControl.GameFlowState.LevelGoal)
        {
            ControlBlockUICanvas = GameObject.Find("ControlBlockUICanvas(Clone)");
            if (ControlBlockUICanvas != null)
                ControlBlockUICanvas.SetActive(false);
        }
    }
    private void OnDisable()
    {
        if (ControlBlockUICanvas != null)
            ControlBlockUICanvas.SetActive(true);

        CloseLevelGaolBtn.onClick.RemoveAllListeners();
    }

    public void CloseLevelGoal()
    {
        TTSCtrl.Instance.StopTTS();
        Debug.Log("Close LevelGoal");
        Time.timeScale = 1;
        Destroy(transform.gameObject);
    }

    void InstanceBoxObjs(GoalObjectEnum goalObjectEnum, Transform parentObjTras)
    {
        GameObject mBox = null;
        mBox = Instantiate(TipBox, parentObjTras);
        BoxComp boxComp = mBox.GetComponent<BoxComp>();
        Image mBoxObjImg = boxComp.BoxObjImg;
        string levelContentID = null;

        switch (goalObjectEnum)
        {
            case GoalObjectEnum.Bettery:
                mBoxObjImg.sprite = BoxObjs.Bettery;
                levelContentID = GoalObjs.BetteryTipString;
                break;
            case GoalObjectEnum.FeedDog:
                mBoxObjImg.sprite = BoxObjs.FeedDog;
                levelContentID = GoalObjs.FeedDogTipString;
                break;
            case GoalObjectEnum.FindDog:
                mBoxObjImg.sprite = BoxObjs.FindDog;
                levelContentID = GoalObjs.FindDogTipString;
                break;
            case GoalObjectEnum.Toy:
                mBoxObjImg.sprite = BoxObjs.Toy;
                levelContentID = GoalObjs.ToyTipString;
                break;
            case GoalObjectEnum.Umbrella:
                mBoxObjImg.sprite = BoxObjs.Umbrella;
                levelContentID = GoalObjs.UmbrellaTipString;
                break;
            case GoalObjectEnum.DigHole:
                mBoxObjImg.sprite = BoxObjs.DigHole;
                levelContentID = GoalObjs.DigHoleTipString;
                break;
            case GoalObjectEnum.FindThing:
                mBoxObjImg.sprite = BoxObjs.TakeThing;
                levelContentID = GoalObjs.TakeThingTipString;
                break;
            case GoalObjectEnum.MoveFront:
                mBoxObjImg.sprite = BoxObjs.MoveFront;
                levelContentID = GoalObjs.MoveFrontTipString;
                break;
            case GoalObjectEnum.MoveBack:
                mBoxObjImg.sprite = BoxObjs.MoveBack;
                levelContentID = GoalObjs.MoveBackTipString;
                break;
            case GoalObjectEnum.MoveRight:
                mBoxObjImg.sprite = BoxObjs.MoveRight;
                levelContentID = GoalObjs.MoveRightTipString;
                break;
            case GoalObjectEnum.MoveLeft:
                mBoxObjImg.sprite = BoxObjs.MoveLeft;
                levelContentID = GoalObjs.MoveLeftTipString;
                break;
            case GoalObjectEnum.East:
                mBoxObjImg.sprite = BoxObjs.East;
                levelContentID = GoalObjs.EastTipString;
                break;
            case GoalObjectEnum.West:
                mBoxObjImg.sprite = BoxObjs.West;
                levelContentID = GoalObjs.WestTipString;
                break;
            case GoalObjectEnum.South:
                mBoxObjImg.sprite = BoxObjs.South;
                levelContentID = GoalObjs.SouthTipString;
                break;
            case GoalObjectEnum.North:
                mBoxObjImg.sprite = BoxObjs.North;
                levelContentID = GoalObjs.NorthTipString;
                break;
            default:
                Debug.Log("==============沒有目標物件!=============");
                break;
        }

        string boxContent = DatabaseManager.Instance.FetchFromSrting_ID_GameContentTextRow(levelContentID).Content;
        boxComp.BoxObjTxt.text = boxContent;
    }



    void ShowFailItem()
    {
        int level = MainGameManager.NowLevel;

        RoleStatus m_roleStatus = MainGameManager.Instance.RoleObjs.GetComponent<RoleStatus>();
        bool m_RoleIsTouchInterRole = m_roleStatus.IsTouchInterRole;
        int m_RoleTakeItemAmount = m_roleStatus.TakeKeyItemAmount;
        bool m_RoleIsWetting = m_roleStatus.IsWetting;
        bool m_RoleIsOpenUmbrella = m_roleStatus.IsOpenUmbrella;
        bool m_RoleIsHapptKebbi = m_roleStatus.IsHappyKebbi;
        bool m_RoleIsPanicKebbi = m_roleStatus.IsPanicKebbi;

        List<string> m_RoleBackpack = m_roleStatus.RoleBackpack;

        ResultData m_resultDecideRow = SaveLoadLevelData.Instance.FetchResultDataFromLevelNo(level);// DatabaseManager.Instance.FetchRoleFromLevel_ResultDecideRow(level);
        //FindDog、MoveFront、MoveBack、MoveRight、MoveLeft、East、West、South、North
        bool m_istouchInterRoleData = m_resultDecideRow.Role_IsTouchInterRole;
        
        //Bettery、Toy、FindThing
        int m_takeItemAmountData = m_resultDecideRow.Role_TakeKeyItemAmount;

        bool m_isWettingData = m_resultDecideRow.Role_IsWetting;
        
        //Umbrella
        bool m_isOpenUmbrellaData = m_resultDecideRow.Role_IsOpenUmbrella;

        //FeedDog
        bool m_isHappyKebbiData = m_resultDecideRow.Role_IsHappyKebbi;

        //bool m_isSadKebbiData = m_resultDecideRow.Role_IsPanicKebbi;


        //DigHole
        
        LevelGaolData levelGaolData = LevelGoalRecord.LevelGaolDatas[level - 1];
        foreach (var item in levelGaolData.m_GoalObjects)
        {
            //Debug.Log("==============" + item.GoalObjectEnums);
            switch (item.GoalObjectEnums)
            {
                case GoalObjectEnum.FindDog:
                case GoalObjectEnum.MoveBack:
                case GoalObjectEnum.MoveFront:
                case GoalObjectEnum.MoveLeft:
                case GoalObjectEnum.MoveRight:
                case GoalObjectEnum.North:
                case GoalObjectEnum.South:
                case GoalObjectEnum.West:
                case GoalObjectEnum.East:
                    if (m_RoleIsTouchInterRole != m_istouchInterRoleData)
                        InstanceBoxObjs(item.GoalObjectEnums, BoxGroup.transform);
                    break;
                case GoalObjectEnum.Bettery:
                case GoalObjectEnum.Toy:
                case GoalObjectEnum.FindThing:
                    if (m_RoleTakeItemAmount != m_takeItemAmountData)
                    {
                        if (m_RoleBackpack.Count == 0)
                        {
                            InstanceBoxObjs(item.GoalObjectEnums, BoxGroup.transform);
                        }
                        else
                        {
                            foreach (var backpack in m_RoleBackpack)
                            {
                                //true=0 false=-1
                                if (backpack.IndexOf("電池") == -1 && item.GoalObjectEnums == GoalObjectEnum.Bettery)
                                    InstanceBoxObjs(GoalObjectEnum.Bettery, BoxGroup.transform);
                                if (backpack.ToLower().IndexOf("dogtoy") == -1 && item.GoalObjectEnums == GoalObjectEnum.Toy)
                                    InstanceBoxObjs(GoalObjectEnum.Toy, BoxGroup.transform);
                                if (backpack.ToLower().IndexOf("magnifying") == -1 && item.GoalObjectEnums == GoalObjectEnum.FindThing)
                                    InstanceBoxObjs(GoalObjectEnum.FindThing, BoxGroup.transform);
                            }
                        }              
                    }
                    break;
                case GoalObjectEnum.Umbrella:
                    if (m_RoleIsOpenUmbrella != m_isOpenUmbrellaData && m_RoleIsWetting != m_isWettingData)
                        InstanceBoxObjs(item.GoalObjectEnums, BoxGroup.transform);          
                    break;
                case GoalObjectEnum.FeedDog:
                    if (m_RoleIsHapptKebbi != m_isHappyKebbiData)
                        InstanceBoxObjs(item.GoalObjectEnums, BoxGroup.transform);
                    break;
                case GoalObjectEnum.DigHole:
                    //沒辦法判斷
                    InstanceBoxObjs(item.GoalObjectEnums, BoxGroup.transform);
                    break;
            }
        }
    }
}
