using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ExecuteBlock : MonoBehaviour
{
    public Action<int> NowPlayBlockSequenceEvent;
    public Action CompleteEXEvent;
    public Action SuccessThisLevelEvnet;


    BlockFunction blockFuntion;
    RoleStatus m_roleStatus;

    /// <summary>
    /// 場景要重載才會初始化
    /// </summary>
    private void Awake()
    {
        blockFuntion = this.gameObject.AddComponent<BlockFunction>();
    }

    /// <summary>
    /// 場景未重載，但流程重新時會更新，初始化
    /// </summary>
    /// <param name="StartBlockArray"></param>
    /// <param name="delayTime"></param>
    /// <param name="roleObj"></param>
    public void ExecuteBlockFuntion(List<string> StartBlockArray, float delayTime,GameObject roleObj)
    {
        //設定BlockFun初始
        blockFuntion.BlockFuntionInit(roleObj);

        //執行動作
        StartCoroutine(IE_DelayExecuteBlock(StartBlockArray, delayTime));
    }

    /// <summary>
    /// 依照方塊順序執行功能
    /// </summary>
    /// <param name="StartBlockArray"></param>
    /// <param name="delayTime"></param>
    /// <returns></returns>
    IEnumerator IE_DelayExecuteBlock(List<string> StartBlockArray,float delayTime)
    {
        //先動作歸位
        Mibo.motionReset();

        yield return new WaitForSeconds(1);

        //再做動作
        for (int i = 0; i < StartBlockArray.Count; i++)
        {
            BlockKind blockKind = (BlockKind)Enum.Parse(typeof(BlockKind), StartBlockArray[i]);
            switch (blockKind)
            { 
                //方塊上掛的程式名字
                case BlockKind.MoveFontBlock:
                    blockFuntion.MoveFront();
                    break;
                case BlockKind.DigHoleBlock:
                    blockFuntion.DigHoleBlock();
                    break;
                case BlockKind.TakeItemBlock:
                    blockFuntion.TakeItemBlock();
                    break;
                case BlockKind.OpenUmbrellaBlock:
                    blockFuntion.OpenUmbrella();
                    break;
                case BlockKind.GiveFoodToInterRoleBlock:
                    blockFuntion.GiveFoodToInterRole();
                    break;
                case BlockKind.OverlookDigHoleBlock:
                    blockFuntion.OverlookDigHole();
                    break;
                //abs
                case BlockKind.OverlookMoveUpBlock:
                    blockFuntion.OverlookMoveUp();
                    break;
                case BlockKind.OverlookMoveDownBlock:
                    blockFuntion.OverlookMoveDown();
                    break;
                case BlockKind.OverlookMoveLeftBlock:
                    blockFuntion.OverlookMoveLeft();
                    break;
                case BlockKind.OverlookMoveRightBlock:
                    blockFuntion.OverlookMoveRight();
                    break;
                //000
                case BlockKind.OverlookMoveEast000Block:
                    blockFuntion.OverlookMoveEast000();
                    break;
                case BlockKind.OverlookMoveWeat000Block:
                    blockFuntion.OverlookMoveWeat000();
                    break;
                case BlockKind.OverlookMoveSouth000Block:
                    blockFuntion.OverlookMoveSouth000();
                    break;
                case BlockKind.OverlookMoveNorth000Block:
                    blockFuntion.OverlookMoveNorth000();
                    break;
                //090
                case BlockKind.OverlookMoveEast090Block:
                    blockFuntion.OverlookMoveEast090();
                    break;
                case BlockKind.OverlookMoveWeat090Block:
                    blockFuntion.OverlookMoveWeat090();
                    break;
                case BlockKind.OverlookMoveSouth090Block:
                    blockFuntion.OverlookMoveSouth090();
                    break;
                case BlockKind.OverlookMoveNorth090Block:
                    blockFuntion.OverlookMoveNorth090();
                    break;
                //180
                case BlockKind.OverlookMoveEast180Block:
                    blockFuntion.OverlookMoveEast180();
                    break;
                case BlockKind.OverlookMoveWeat180Block:
                    blockFuntion.OverlookMoveWeat180();
                    break;
                case BlockKind.OverlookMoveSouth180Block:
                    blockFuntion.OverlookMoveSouth180();
                    break;
                case BlockKind.OverlookMoveNorth180Block:
                    blockFuntion.OverlookMoveNorth180();
                    break;
                //270
                case BlockKind.OverlookMoveEast270Block:
                    blockFuntion.OverlookMoveEast270();
                    break;
                case BlockKind.OverlookMoveWeat270Block:
                    blockFuntion.OverlookMoveWeat270();
                    break;
                case BlockKind.OverlookMoveSouth270Block:
                    blockFuntion.OverlookMoveSouth270();
                    break;
                case BlockKind.OverlookMoveNorth270Block:
                    blockFuntion.OverlookMoveNorth270();
                    break;
                //Loop
                case BlockKind.LoopUpBlock:
                    blockFuntion.LoopUp();
                    LoopUpBlockFuntion(i, StartBlockArray);
                    break;
                case BlockKind.LoopDownBlock:
                    blockFuntion.LookDown();
                    break;
            }
            //觸發事件
            NowPlayBlockSequenceEvent(i);

            //等3秒執行下一個動作
             yield return new WaitForSeconds(delayTime);


            m_roleStatus = MainGameManager.Instance.RoleObjs.GetComponent<RoleStatus>();
            //判斷有無成功
            if (m_roleStatus != null && MainGameManager.Instance.IsHaveToturial == false)
            {
                if (ResultDecide.Instance.CheckResult(MainGameManager.NowLevel, m_roleStatus))
                {
                    ResultDecide.Instance.SavePassLevel();
                    yield return new WaitForSeconds(0.5f);
                    if (SuccessThisLevelEvnet!=null)SuccessThisLevelEvnet();
                    break;//跳出迴圈
                }
                else
                {
                    //判斷是不是陣列最後一個方塊執行完成
                    if (i == (StartBlockArray.Count - 1))
                    {
                        yield return new WaitForSeconds(1f);
                        if (CompleteEXEvent != null) CompleteEXEvent();
                    }
                }
            }
            else
            {
                Debug.LogWarning("找不到主角色");
            }
            
        }
    }


    void LoopUpBlockFuntion(int i, List<string> StartBlockArray/*,out bool isLooping*/)
    {
        List<string> loopInsideBlock = new List<string>();
        bool isHaveLoopBlock = false;
        for (int x = i; x < StartBlockArray.Count; x++)
        {
            int loopDownBlockPosID = 0;
            if (StartBlockArray[x] == "LoopDownBlock")
            {
                loopDownBlockPosID = x;
                isHaveLoopBlock = true;
                break;
            }
            if (x != i || x != loopDownBlockPosID)
            {
                loopInsideBlock.Add(StartBlockArray[x]);
            }

        }
        if (!isHaveLoopBlock)
        {
            loopInsideBlock.Clear();
            Debug.Log("===Not Have Down!=====");
        }
        else
        {
            foreach (var item in loopInsideBlock)
            {
                Debug.Log("===========" + item);

            }
            StartCoroutine(IE_DelayExecuteBlock(loopInsideBlock, 3));
            
        }
    }
}


