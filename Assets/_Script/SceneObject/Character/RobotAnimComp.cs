using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 撥放機器人用的動畫(連續圖檔, 非motion檔)
/// </summary>
public class RobotAnimComp : MonoBehaviour
{
    /// <summary>
    /// 機器人用的動畫對應enum
    /// </summary>
    public enum ERobotAnim
    {
        defaultStand,
        moveFrontSuc,
        moveFrontFail,
        digHoleSuc,
        digHoleFail,
        takeItemSuc,
        takeItemFail,
        openUmbrellaSuc,
        openUmbrellaFail,
        giveFoodToInterRoleSuc,
        giveFoodToInterRoleFail,
        overlookDigHoleSuc,
        overlookDigHoleFail,
        overlookMoveUp,
        overlookMoveUpFail,
        overlookMoveDown,
        overlookMoveDownFail,
        overlookMoveLeft,
        overlookMoveLeftFail,
        overlookMoveRight,
        overlookMoveRightFail
    }

    private SimpleAnimation mAnim;

    private ERobotAnim mCurrentPlayAnim = ERobotAnim.defaultStand;
    public ERobotAnim CurrenPlayAnim { get { return mCurrentPlayAnim; } }

    /// <summary>
    /// 撥放動畫結束的事件
    /// </summary>
    public Action<ERobotAnim> OnPlayAnimFinishedEvent;
    
    #region 狀態
    [SerializeField]
    [Header("淋濕的Sprite")]
    Sprite WetSprite;

    [SerializeField]
    [Header("雨傘(躲雨)的Sprite")]
    Sprite UmbrellaSprite;

    [SerializeField]
    [Header("開心凱比")]
    Sprite HappyKebbiSprite;

    [SerializeField]
    [Header("慌張凱比")]
    Sprite PanicKebbiSprite;

    #endregion


    [SerializeField]
    [Header("機器人狀態生成的位置")]
    Transform RobotStateSpriteTrans;

    [SerializeField]
    [Header("機器人狀態prefab")]
    GameObject RobotStatePrefab;

    [SerializeField]
    [Header("機器人取得道具動作會出現的Sprite")]
    SpriteRenderer RobotTakeItemSpriteRenderer;


    #region 物件
    [SerializeField]
    [Header("電池")]
    Sprite BatterySprite;

    [SerializeField]
    [Header("玩具")]
    Sprite DogToySprite;

    [SerializeField]
    [Header("食物")]
    Sprite FoodSprite;

    [SerializeField]
    [Header("放大鏡")]
    Sprite MagnifyinglensSprite;
    #endregion

    #region 音效
    [Header("方塊音效")]
    [SerializeField]
    AudioClip DigHoleSucClip;
    [SerializeField]
    AudioClip DigHoleFailClip;

    [SerializeField]
    AudioClip TakeItemSucClip;
    [SerializeField]
    AudioClip TakeItemFailClip;

    [SerializeField]
    AudioClip OpenUmbrellaSucClip;
    [SerializeField]
    AudioClip OpenUmbrellaFailClip;

    [SerializeField]
    AudioClip GiveFoodInterRoleSucClip;
    [SerializeField]
    AudioClip GiveFoodInterRoleFailClip;

    [SerializeField]
    AudioClip OverlookDigHoleSucClip;
    [SerializeField]
    AudioClip OverlookDigHoleFailClip;

    [SerializeField]
    AudioClip WalkSucClip;
    [SerializeField]
    AudioClip WalkFailClip;


    #endregion

    RoleContorl m_RoleControl;
    RoleStatus m_RoleStatus;
    string m_RolePackageLastItem;

    /// <summary>
    /// 是否成功拿到道具
    /// </summary>
    private bool mIsTakeItemSuccess = false;

    // Use this for initialization
    void Start()
    {
        
        mAnim = this.GetComponent<SimpleAnimation>();
        //m_RoleControl = this.GetComponentInParent<RoleContorl>();
        m_RoleStatus=this.GetComponentInParent<RoleStatus>();

        if (m_RoleStatus != null)
        {
            m_RoleStatus.MoveFrontEvent += OnMoveFront;
            m_RoleStatus.DigHoleEvent += OnDigHole;
            m_RoleStatus.OpenUmbrellaEvent += OnOpenUmbrella;
            m_RoleStatus.TakeItemEvent += OnTakeItem;

            m_RoleStatus.GiveFoodToInterRoleEvent += OnGiveFoodToInterRole;
            m_RoleStatus.OverlookDigHoleEvent += OnOverlookDigHole;

            m_RoleStatus.OverlookMoveUpEvent += OnOverlookMoveUp;
            m_RoleStatus.OverlookMoveDownEvent += OnOverlookMoveDown;
            m_RoleStatus.OverlookMoveLeftEvent += OnOverlookMoveLeft;
            m_RoleStatus.OverlookMoveRightEvent += OnOverlookMoveRight;

            m_RoleStatus.OverlookMoveEast000Event += OnOverlookMoveEast000;
            m_RoleStatus.OverlookMoveWeat000Event += OnOverlookMoveWeat000;
            m_RoleStatus.OverlookMoveSouth000Event += OnOverlookMoveSouth000;
            m_RoleStatus.OverlookMoveNorth000Event += OnOverlookMoveNorth000;

            m_RoleStatus.OverlookMoveEast090Event += OnOverlookMoveEast090;
            m_RoleStatus.OverlookMoveWeat090Event += OnOverlookMoveWeat090;
            m_RoleStatus.OverlookMoveSouth090Event += OnOverlookMoveSouth090;
            m_RoleStatus.OverlookMoveNorth090Event += OnOverlookMoveNorth090;

            m_RoleStatus.OverlookMoveEast180Event += OnOverlookMoveEast180;
            m_RoleStatus.OverlookMoveWeat180Event += OnOverlookMoveWeat180;
            m_RoleStatus.OverlookMoveSouth180Event += OnOverlookMoveSouth180;
            m_RoleStatus.OverlookMoveNorth180Event += OnOverlookMoveNorth180;

            m_RoleStatus.OverlookMoveEast270Event += OnOverlookMoveEast270;
            m_RoleStatus.OverlookMoveWeat270Event += OnOverlookMoveWeat270;
            m_RoleStatus.OverlookMoveSouth270Event += OnOverlookMoveSouth270;
            m_RoleStatus.OverlookMoveNorth270Event += OnOverlookMoveNorth270;
        }

        //RobotStateSpriteRenderer.enabled = false;
    }

    private void OnDestroy()
    {
        if (m_RoleStatus != null)
        {
            m_RoleStatus.MoveFrontEvent -= OnMoveFront;
            m_RoleStatus.DigHoleEvent -= OnDigHole;
            m_RoleStatus.OpenUmbrellaEvent -= OnOpenUmbrella;
            m_RoleStatus.TakeItemEvent -= OnTakeItem;

            m_RoleStatus.GiveFoodToInterRoleEvent -= OnGiveFoodToInterRole;
            m_RoleStatus.OverlookDigHoleEvent -= OnOverlookDigHole;

            m_RoleStatus.OverlookMoveUpEvent -= OnOverlookMoveUp;
            m_RoleStatus.OverlookMoveDownEvent -= OnOverlookMoveDown;
            m_RoleStatus.OverlookMoveLeftEvent -= OnOverlookMoveLeft;
            m_RoleStatus.OverlookMoveRightEvent -= OnOverlookMoveRight;

            m_RoleStatus.OverlookMoveEast000Event -= OnOverlookMoveEast000;
            m_RoleStatus.OverlookMoveWeat000Event -= OnOverlookMoveWeat000;
            m_RoleStatus.OverlookMoveSouth000Event -= OnOverlookMoveSouth000;
            m_RoleStatus.OverlookMoveNorth000Event -= OnOverlookMoveNorth000;

            m_RoleStatus.OverlookMoveEast090Event -= OnOverlookMoveEast090;
            m_RoleStatus.OverlookMoveWeat090Event -= OnOverlookMoveWeat090;
            m_RoleStatus.OverlookMoveSouth090Event -= OnOverlookMoveSouth090;
            m_RoleStatus.OverlookMoveNorth090Event -= OnOverlookMoveNorth090;

            m_RoleStatus.OverlookMoveEast180Event -= OnOverlookMoveEast180;
            m_RoleStatus.OverlookMoveWeat180Event -= OnOverlookMoveWeat180;
            m_RoleStatus.OverlookMoveSouth180Event -= OnOverlookMoveSouth180;
            m_RoleStatus.OverlookMoveNorth180Event -= OnOverlookMoveNorth180;

            m_RoleStatus.OverlookMoveEast270Event -= OnOverlookMoveEast270;
            m_RoleStatus.OverlookMoveWeat270Event -= OnOverlookMoveWeat270;
            m_RoleStatus.OverlookMoveSouth270Event -= OnOverlookMoveSouth270;
            m_RoleStatus.OverlookMoveNorth270Event -= OnOverlookMoveNorth270;
        }
    }

    #region 取得撥放Animation的事件
    /// <summary>
    /// 移動事件
    /// </summary>
    /// <param name="isSuccess"></param>
    private void OnMoveFront(bool isSuccess)
    {
        if (isSuccess)
        {
            PlayAnim(ERobotAnim.moveFrontSuc);
            Mibo.motionPlay(DatabaseManager.Instance.FetchFromSrting_ID_RobotmotionRow("Robot_Walk_Succ").Motion);
            AudioManager.Instance.PlaySoundFx(WalkSucClip);
        }
        else
        {
            PlayAnim(ERobotAnim.moveFrontFail);
            Mibo.motionPlay(DatabaseManager.Instance.FetchFromSrting_ID_RobotmotionRow("Robot_Walk_Fail").Motion);
            AudioManager.Instance.PlaySoundFx(WalkFailClip);
        }
        SetRobotStateSprite();
    }

    /// <summary>
    /// 挖洞
    /// </summary>
    private void OnDigHole(bool isSuccess)
    {
        if (isSuccess)
        {
            PlayAnim(ERobotAnim.digHoleSuc);
            Mibo.motionPlay(DatabaseManager.Instance.FetchFromSrting_ID_RobotmotionRow("Robot_Dig_Succ").Motion);
            AudioManager.Instance.PlaySoundFx(DigHoleSucClip);

        }
        else
        {
            PlayAnim(ERobotAnim.digHoleFail);
            Mibo.motionPlay(DatabaseManager.Instance.FetchFromSrting_ID_RobotmotionRow("Robt_DIg_Fail").Motion);
            AudioManager.Instance.PlaySoundFx(DigHoleFailClip);
        }
    }


    /// <summary>
    /// 取得道具
    /// </summary>
    /// <param name="isSuccess"></param>
    private void OnTakeItem(bool isSuccess)
    {
        if (isSuccess)
        {
            PlayAnim(ERobotAnim.takeItemSuc);
            Mibo.motionPlay(DatabaseManager.Instance.FetchFromSrting_ID_RobotmotionRow("Robot_TakeObi_Succ").Motion);
            AudioManager.Instance.PlaySoundFx(TakeItemSucClip);

            mIsTakeItemSuccess = isSuccess;
            RobotTakeItemSpriteRenderer.sprite = null;
        }
        else
        {
            PlayAnim(ERobotAnim.takeItemFail);
            Mibo.motionPlay(DatabaseManager.Instance.FetchFromSrting_ID_RobotmotionRow("Robot_TakeObj_Fail").Motion);
            AudioManager.Instance.PlaySoundFx(TakeItemFailClip);

        }

    }

    /// <summary>
    /// 撥放取得道具的Animation的Event，放在Animation中當事件!
    /// </summary>
    public void OnTakeItemAnimEvent()
    {
        m_RolePackageLastItem = m_RoleStatus.RoleBackpack[m_RoleStatus.RoleBackpack.Count - 1];

        if (mIsTakeItemSuccess)
        {
            GetItemTypes rolePackageLastItem = (GetItemTypes)Enum.Parse(typeof(GetItemTypes), m_RolePackageLastItem);

            switch (rolePackageLastItem)
            {
                case GetItemTypes.battery:
                    RobotTakeItemSpriteRenderer.sprite = BatterySprite;

                    break;
                case GetItemTypes.dogToy:
                    RobotTakeItemSpriteRenderer.sprite = DogToySprite;

                    break;
                case GetItemTypes.food:
                    RobotTakeItemSpriteRenderer.sprite = FoodSprite;

                    break;
                case GetItemTypes.magnifyinglens:
                    RobotTakeItemSpriteRenderer.sprite = MagnifyinglensSprite;

                    break;
            }
            RobotTakeItemSpriteRenderer.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 躲雨事件
    /// </summary>
    /// <param name="isSuccess"></param>
    private void OnOpenUmbrella(bool isSuccess)
    {
        if (isSuccess)
        {
            PlayAnim(ERobotAnim.openUmbrellaSuc);
            Mibo.motionPlay(DatabaseManager.Instance.FetchFromSrting_ID_RobotmotionRow("Robot_Umrbella_Succ").Motion);
            AudioManager.Instance.PlaySoundFx(OpenUmbrellaSucClip);


        }
        else
        {
            PlayAnim(ERobotAnim.openUmbrellaFail);
            Mibo.motionPlay(DatabaseManager.Instance.FetchFromSrting_ID_RobotmotionRow("Robot_Umrbella_Fail").Motion);
            AudioManager.Instance.PlaySoundFx(OpenUmbrellaFailClip);


        }
    }

 
    void OnGiveFoodToInterRole(bool isSuccess)
    {
        if (isSuccess)
        {
            PlayAnim(ERobotAnim.giveFoodToInterRoleSuc);
            Mibo.motionPlay(DatabaseManager.Instance.FetchFromSrting_ID_RobotmotionRow("Robot_Feed_Succ").Motion);
            AudioManager.Instance.PlaySoundFx(GiveFoodInterRoleSucClip);

        }
        else
        {
            PlayAnim(ERobotAnim.giveFoodToInterRoleFail);
            Mibo.motionPlay(DatabaseManager.Instance.FetchFromSrting_ID_RobotmotionRow("Robot_Feed_Fail").Motion);
            AudioManager.Instance.PlaySoundFx(GiveFoodInterRoleFailClip);

        }
    }

    void OnOverlookDigHole(bool isSuccess)
    {
        if (isSuccess)
        {
            PlayAnim(ERobotAnim.overlookDigHoleSuc);
            Mibo.motionPlay(DatabaseManager.Instance.FetchFromSrting_ID_RobotmotionRow("Robot_Dig_Succ").Motion);
            AudioManager.Instance.PlaySoundFx(OverlookDigHoleSucClip);

        }
        else
        {
            PlayAnim(ERobotAnim.overlookDigHoleFail);
            Mibo.motionPlay(DatabaseManager.Instance.FetchFromSrting_ID_RobotmotionRow("Robt_DIg_Fail").Motion);
            AudioManager.Instance.PlaySoundFx(OverlookDigHoleFailClip);

        }
    }

    //abs///////
    void OnOverlookMoveUp(bool isSuccess)
    {
        UsualMoveUp(isSuccess);
    }
    void OnOverlookMoveDown(bool isSuccess)
    {
        UsualMoveDown(isSuccess);
    }
    void OnOverlookMoveLeft(bool isSuccess)
    {
        UsualMoveLeft(isSuccess);
    }
    void OnOverlookMoveRight(bool isSuccess)
    {
        UsualMoveRight(isSuccess);
    }

    //000//////////
    void OnOverlookMoveEast000(bool isSuccess)
    {
        UsualMoveRight(isSuccess);

    }
    void OnOverlookMoveWeat000(bool isSuccess)
    {
        UsualMoveLeft(isSuccess);

    }
    void OnOverlookMoveSouth000(bool isSuccess)
    {
        UsualMoveDown(isSuccess);

    }
    void OnOverlookMoveNorth000(bool isSuccess)
    {
        UsualMoveUp(isSuccess);
    }

    //090/////////
    void OnOverlookMoveEast090(bool isSuccess)
    {
        UsualMoveUp(isSuccess);
    }
    void OnOverlookMoveWeat090(bool isSuccess)
    {
        UsualMoveDown(isSuccess);

    }
    void OnOverlookMoveSouth090(bool isSuccess)
    {
        UsualMoveRight(isSuccess);

    }
    void OnOverlookMoveNorth090(bool isSuccess)
    {
        UsualMoveLeft(isSuccess);

    }

    //180///////////
    void OnOverlookMoveEast180(bool isSuccess)
    {
        UsualMoveLeft(isSuccess);


    }
    void OnOverlookMoveWeat180(bool isSuccess)
    {
        UsualMoveRight(isSuccess);

    }
    void OnOverlookMoveSouth180(bool isSuccess)
    {
        UsualMoveUp(isSuccess);
    }
    void OnOverlookMoveNorth180(bool isSuccess)
    {
        UsualMoveDown(isSuccess);

    }

    //270////////
    void OnOverlookMoveEast270(bool isSuccess)
    {
        UsualMoveDown(isSuccess);

    }
    void OnOverlookMoveWeat270(bool isSuccess)
    {
        UsualMoveUp(isSuccess);
    }
    void OnOverlookMoveSouth270(bool isSuccess)
    {
        UsualMoveLeft(isSuccess);

    }
    void OnOverlookMoveNorth270(bool isSuccess)
    {
        UsualMoveRight(isSuccess);

    }


    #endregion 取得撥放Animation的事件

    void UsualMoveUp(bool isSuccess)
    {
        if (isSuccess)
        {
            PlayAnim(ERobotAnim.overlookMoveUp);
            Mibo.motionPlay(DatabaseManager.Instance.FetchFromSrting_ID_RobotmotionRow("Robot_WalkBack_Succ").Motion);
            AudioManager.Instance.PlaySoundFx(WalkSucClip);

        }
        else
        {
            PlayAnim(ERobotAnim.overlookMoveUpFail);
            Mibo.motionPlay(DatabaseManager.Instance.FetchFromSrting_ID_RobotmotionRow("Robot_WalkBack_Fail").Motion);
            AudioManager.Instance.PlaySoundFx(WalkFailClip);
        }
        SetRobotStateSprite();
    }

    void UsualMoveDown(bool isSuccess)
    {
        if (isSuccess)
        {
            PlayAnim(ERobotAnim.overlookMoveDown);
            Mibo.motionPlay(DatabaseManager.Instance.FetchFromSrting_ID_RobotmotionRow("Robot_WalkFront_Succ").Motion);
            AudioManager.Instance.PlaySoundFx(WalkSucClip);

        }
        else
        {
            PlayAnim(ERobotAnim.overlookMoveDownFail);
            Mibo.motionPlay(DatabaseManager.Instance.FetchFromSrting_ID_RobotmotionRow("Robot_WalkFront_Fail").Motion);
            AudioManager.Instance.PlaySoundFx(WalkFailClip);
        }
        SetRobotStateSprite();
    }

    void UsualMoveLeft(bool isSuccess)
    {
        if (isSuccess)
        {
            PlayAnim(ERobotAnim.overlookMoveLeft);
            Mibo.motionPlay(DatabaseManager.Instance.FetchFromSrting_ID_RobotmotionRow("Robot_WalkL_Succ").Motion);
            AudioManager.Instance.PlaySoundFx(WalkSucClip);

        }
        else
        {
            PlayAnim(ERobotAnim.overlookMoveLeftFail);
            Mibo.motionPlay(DatabaseManager.Instance.FetchFromSrting_ID_RobotmotionRow("Robot_WalkL_Fail").Motion);
            AudioManager.Instance.PlaySoundFx(WalkFailClip);
        }
        SetRobotStateSprite();
    }


    void UsualMoveRight(bool isSuccess)
    {
        if (isSuccess)
        {
            PlayAnim(ERobotAnim.overlookMoveRight);
            Mibo.motionPlay(DatabaseManager.Instance.FetchFromSrting_ID_RobotmotionRow("Robot_WalkR_Succ").Motion);
            AudioManager.Instance.PlaySoundFx(WalkSucClip);

        }
        else
        {
            PlayAnim(ERobotAnim.overlookMoveRightFail);
            Mibo.motionPlay(DatabaseManager.Instance.FetchFromSrting_ID_RobotmotionRow("Robot_WalkR_Fail").Motion);
            AudioManager.Instance.PlaySoundFx(WalkFailClip);
        }
        SetRobotStateSprite();
    }

    public void PlayAnim(ERobotAnim anim)
    {
        Debug.Log("Play anim  : " + anim.ToString() +" , prev : " + mCurrentPlayAnim.ToString());
        mCurrentPlayAnim = anim;
        mAnim.Stop();
        mAnim.Play(anim.ToString());
        SetRobotStateSprite();
    }

    /// <summary>
    /// 撥放動畫結束後，放在Animation中當事件!
    /// </summary>
    protected void OnPlayAnimFinished()
    {
        //先對外呼叫撥放動畫結束
        if (OnPlayAnimFinishedEvent != null)
            OnPlayAnimFinishedEvent(mCurrentPlayAnim);

        //Debug.LogWarning("On Play anim Finished : currAnim: " + mCurrentPlayAnim);
        //再來判斷播完哪個動畫後要做甚麼
        switch (mCurrentPlayAnim)
        {
            case ERobotAnim.defaultStand:
                break;
            default:
                PlayAnim(ERobotAnim.defaultStand);
                //Mibo.motionPlay(DatabaseManager.Instance.FetchFromSrting_ID_RobotmotionRow("robot_dig").Motion);

                break;
        }
        //動作做完後判定狀態
        SetRobotStateSprite();

        //拿到物件圖片關閉
        RobotTakeItemSpriteRenderer.gameObject.SetActive(false);

    }


    List<GameObject> stateObj = new List<GameObject>();
    GameObject umbrallaObj;
    private void SetRobotStateSprite()
    {

        if (m_RoleStatus != null)
        {

            //先全部刪掉
            if (stateObj.Count != 0)
            {
                foreach (var item in stateObj)
                {
                    Destroy(item);
                    Destroy(umbrallaObj);
                }
                stateObj.Clear();
            }

            //再重新生成
            if (m_RoleStatus.IsWetting)
            {
                SetStateObj(WetSprite);
            }
            if (m_RoleStatus.IsOpenUmbrella)//特例
            {
                if (umbrallaObj == null)
                {
                    umbrallaObj = Instantiate(RobotStatePrefab, RobotStateSpriteTrans);
                    umbrallaObj.GetComponent<SpriteRenderer>().sprite = UmbrellaSprite;
                    umbrallaObj.transform.localPosition = new Vector3(0,-0.24f,0); //Vector3.zero;
                }
            }
            if (m_RoleStatus.IsHappyKebbi)
            {
                SetStateObj(HappyKebbiSprite);
            }
            if (m_RoleStatus.IsPanicKebbi)
            {
                SetStateObj(PanicKebbiSprite);

            }
        }

    }

    void SetStateObj(Sprite stateSprite)
    {
        stateObj.Add(Instantiate(RobotStatePrefab, RobotStateSpriteTrans));
        int lastObj = stateObj.Count - 1;
        stateObj[lastObj].GetComponent<SpriteRenderer>().sprite = stateSprite;
        stateObj[lastObj].transform.localPosition = new Vector3(-0.5f + (-0.5f * lastObj), -0.4f, 0);
    }

    
}
