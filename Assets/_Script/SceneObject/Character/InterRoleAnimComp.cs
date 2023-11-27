using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterRoleAnimComp : MonoBehaviour {

    /// <summary>
    /// 機器人用的動畫對應enum
    /// </summary>
    public enum EInterRoleAnim
    {
        Default,
        confuse,
        full,
        happy
    }

    private SimpleAnimation mAnim;

    private EInterRoleAnim mCurrentPlayAnim = EInterRoleAnim.Default;
    public EInterRoleAnim CurrenPlayAnim { get { return mCurrentPlayAnim; } }

    /// <summary>
    /// 撥放動畫結束的事件
    /// </summary>
    public System.Action<EInterRoleAnim> OnPlayAnimFinishedEvent;


    [SerializeField]
    [Header("飽足的Sprite")]
    Sprite EatFullSprite;

    [SerializeField]
    [Header("互動角色狀態的SpriteRenderer")]
    SpriteRenderer InterRoleStateSpriteRenderer;

    InterRoleContorl m_InterRoleContorl;
    InterRoleStatus m_interRoleStatus;

    void Start()
    {
        mAnim = this.GetComponent<SimpleAnimation>();
        m_InterRoleContorl = this.GetComponentInParent<InterRoleContorl>();
        m_interRoleStatus = this.GetComponentInParent<InterRoleStatus>();
        if (m_InterRoleContorl != null)
        {
            m_interRoleStatus.IAmConfuseEvent += OnIAmConfuse;
            m_interRoleStatus.IAmFullEvent += OnIAmFull;
            m_interRoleStatus.IAmHappyEvent += OnIAmHappy;
        }
        PlayAnim(EInterRoleAnim.Default);

        InterRoleStateSpriteRenderer.enabled = false;
    }

    private void OnDestroy()
    {
        if (m_InterRoleContorl != null)
        {
            m_interRoleStatus.IAmConfuseEvent -= OnIAmConfuse;
            m_interRoleStatus.IAmFullEvent -= OnIAmFull;
            m_interRoleStatus.IAmHappyEvent -= OnIAmHappy;
        }
    }

    #region 取得撥放Animation的事件

    void OnIAmConfuse(bool isSuccess)
    {
        if (!isSuccess)
        {
            PlayAnim(EInterRoleAnim.confuse);
        }
        else { }
    }

    void OnIAmFull(bool isSuccess)
    {
        if (isSuccess)
        {
            PlayAnim(EInterRoleAnim.full);
        }
        else { }
    }

    void OnIAmHappy(bool isSuccess)
    {
        if (isSuccess)
        {
            PlayAnim(EInterRoleAnim.happy);

            //碰到狗音效
            AudioManager.Instance.GetComponent<GetAudioSource>().PlayDogSound(GetAudioSource.DogSounds.happy);
        }
        else { }
    }

    #endregion

    public void PlayAnim(EInterRoleAnim anim)
    {
        //Debug.Log("Play anim  : " + anim.ToString() + " , prev : " + mCurrentPlayAnim.ToString());
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
            case EInterRoleAnim.Default:
                break;
            default:
                //Debug.Log("OnPlayAnimFinished , curr anim : " + mCurrentPlayAnim.ToString());

                PlayAnim(EInterRoleAnim.Default);
                break;
        }
        //動作做完後判定狀態
        SetRobotStateSprite();
    }


    private void SetRobotStateSprite()
    {
        if (m_InterRoleContorl != null)
        {
            //Debug.LogWarning("isWetting : " + mRoleControl.IsWetting + " , IsKeepOffRain : " + mRoleControl.IsKeepOffRain);
            if (m_InterRoleContorl.GetComponent<InterRoleStatus>().IsFoodFull)
            {
                InterRoleStateSpriteRenderer.sprite = EatFullSprite;
                InterRoleStateSpriteRenderer.enabled = true;
            }
            else
                InterRoleStateSpriteRenderer.enabled = false;
        }

    }
}
