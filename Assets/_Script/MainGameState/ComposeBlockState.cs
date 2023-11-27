using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComposeBlockState : IMainGameState
{
    GameObject[] allBlockGOs = MainGameManager.Instance.AllblockInScene();
    ControlBlockUIComp controlBlockUIComp = null;

    const int showScrollbarBlockCount = 9;

    Block m_startBlock = null;
    Scrollbar m_scrollBar = null;
    RectTransform m_editorAera = null;
    Image m_scrollBarImage01 = null;
    Image m_scrollBarImage02 = null;
    LevelPool levelPool = new LevelPool();
    ControlRobotMotion controlRobotMotion ;
    GameObject controlRobotMotionObj;
    ScrollRect scrollRect;
    int isDragBlockAmount = 0;

    public ComposeBlockState(MainGameStateControl Controller) : base(Controller)  //Controller=GameLoop的m_SceneStateController
    {
        this.StateName = "ComposeBlockState";
    }

    //開始
    public override void StateBegin()
    {
        if (GameObject.FindObjectOfType<ControlRobotMotion>() != null)
        {
            controlRobotMotion = GameObject.FindObjectOfType<ControlRobotMotion>();
        }
        else
        {
            controlRobotMotionObj = new GameObject();
            controlRobotMotion = controlRobotMotionObj.AddComponent<ControlRobotMotion>();
        }     
         
        //生成UI
        MainGameManager.Instance.InstantiateInitUI();
        levelPool.InstanceLevelBlock();

        allBlockGOs = MainGameManager.Instance.AllblockInScene();
        controlBlockUIComp = MainGameManager.Instance.ControlBlockUICanvases.GetComponentInChildren<ControlBlockUIComp>();
        m_startBlock = controlBlockUIComp.StartBlock.GetComponent<Block>();
        m_scrollBar = controlBlockUIComp.Scrollbar.GetComponent<Scrollbar>();
        m_editorAera = controlBlockUIComp.EditorAera.GetComponent<RectTransform>();
        m_scrollBarImage01 = controlBlockUIComp.Scrollbar.GetComponent<Image>();
        m_scrollBarImage02 = controlBlockUIComp.Scrollbar.transform.GetChild(0).GetComponentInChildren<Image>();
        scrollRect = controlBlockUIComp.ScrollObj.GetComponentInChildren<ScrollRect>();

        //註冊壓下開始按鈕
        GameEventSystem.Instance.OnPushStartCodeBtn += ChangeToReadState;

        //開啟start按鈕
        controlBlockUIComp.StartCodeBtn.enabled = true;
        //可以拖曳block
        foreach (var m_allBlockGOs in allBlockGOs)
        {
            if (m_allBlockGOs != null) m_allBlockGOs.GetComponent<Block>().isCanMoveBlock = true;
        }
        //關閉拖曳
        //scrollRect.enabled = false;////////////////////////////////////////////////////

        //教學
        if (MainGameManager.Instance.IsHaveToturial) InToturial();

        //鎖頭部
        controlRobotMotion.LockRobotMotion(Mibo.MiboMotorType.neck_y);
        controlRobotMotion.LockRobotMotion(Mibo.MiboMotorType.neck_z);

    }

    //bool isShowBar = false;
    //bool isDragingBlock = false;
    //float[] changeBarlengthNum = { 800, 2000 };

    //更新
    public override void StateUpdate()
    {
        //在Block的IBeginDragHandler, IDragHandler, IEndDragHandler自己更新
        ArrayList startBlockArrayList = m_startBlock.DescendingBlocksForStartBlock();
        GameObject[] AllBlockGOs = GameObject.FindGameObjectsWithTag("Block");

        #region 用方塊位置判斷bar長度
        //foreach (var item in AllBlockGOs)
        //{
        //    isShowBar = false;
        //    if (item.GetComponent<RectTransform>().anchoredPosition.x > changeBarlengthNum[0])
        //    {
        //        ChangeBarLength(startBlockArrayList.Count);
        //        ShowScrollBar();
        //        isShowBar = true;
        //        break;
        //    }
        //}

        //foreach (var item in AllBlockGOs)
        //{
        //    isDragingBlock = false;
        //    if (item.GetComponent<Block>().IsDraging == true)
        //    {
        //        isDragingBlock = true;
        //        break;
        //    }
        //}

        //if (!isShowBar && !isDragingBlock)
        //{
        //    HideScrollBar();
        //}
        #endregion

        #region 用方塊數量判斷bar長度
        if (startBlockArrayList != null)
        {
            if (controlBlockUIComp != null)
            {
                if (startBlockArrayList.Count >= showScrollbarBlockCount)
                {
                    ChangeBarLength(startBlockArrayList.Count);
                    ShowScrollBar();
                }
                else
                {
                    isDragBlockAmount = 0;
                    foreach (var item in AllBlockGOs)
                    {
                        if (item.GetComponent<Block>().IsDraging == true)
                        {
                            isDragBlockAmount = item.GetComponent<Block>().IsDragingBlockAmount;
                        }
                    }

                    if (startBlockArrayList.Count + isDragBlockAmount < showScrollbarBlockCount)
                    {
                        ChangeBarLength(startBlockArrayList.Count);

                        HideScrollBar();
                    }
                }
            }
        }
        #endregion
    }

    //結束
    public override void StateEnd() {

        //取消註冊
        GameEventSystem.Instance.OnPushStartCodeBtn -= ChangeToReadState;
        //disable Start按鈕
        if(controlBlockUIComp!=null) controlBlockUIComp.StartCodeBtn.enabled = false;

        //不能拖曳block
        //更新找到哪些
        allBlockGOs = MainGameManager.Instance.AllblockInScene();
        foreach (var m_allBlockGOs in allBlockGOs)
        {
            //Debug.Log(m_allBlockGOs);
            m_allBlockGOs.GetComponent<Block>().isCanMoveBlock = false;
        }
    }

    /// <summary>
    /// 按下start轉換到讀取Block階段
    /// </summary>
    void ChangeToReadState()
    {
        m_Conrtoller.SetState(MainGameStateControl.GameFlowState.ReadBlock, m_Conrtoller);
    }

    void ChangeToInitState()
    {
        m_Conrtoller.SetState(MainGameStateControl.GameFlowState.Init, m_Conrtoller);
    }

    void ShowScrollBar()
    {
        //scrollRect.enabled = true;\\\\\\\\\\\\\\\\\\\
        m_scrollBarImage01.enabled = true;
        m_scrollBarImage02.enabled = true;
        m_scrollBar.enabled = true;
    }

    void HideScrollBar()
    {
        //scrollRect.horizontalNormalizedPosition = 0;\\\\\\\
        //scrollRect.enabled = false;
        m_scrollBarImage01.enabled = false;
        m_scrollBarImage02.enabled = false;
        m_scrollBar.enabled = false;
    }

    void ChangeBarLength(int editorBlockAmount)
    {
        int addLengthBlockAmount = 9;
        int contentAddLength = 1703/2;
        if(editorBlockAmount < ((Block.limitBlockNum / addLengthBlockAmount)) * showScrollbarBlockCount)
        {
            m_editorAera.sizeDelta = new Vector2(((editorBlockAmount / addLengthBlockAmount) + 1) * contentAddLength, m_editorAera.sizeDelta.y);
        }
    }


    void InToturial()
    {
       MainGameManager.Instance.HandToHandToturialUICanvases.GetComponent<HandtoHandToturialComp>().ComposeBlockStateInToturail(ChangeToInitState);
    }
}
