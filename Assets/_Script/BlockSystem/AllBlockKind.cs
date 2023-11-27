using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BlockKind
{
    StartReadBlock,
    MoveFontBlock,
    DigHoleBlock,
    TakeItemBlock,
    OpenUmbrellaBlock,
    GiveFoodToInterRoleBlock,
    OverlookDigHoleBlock,
    //abs
    OverlookMoveUpBlock,
    OverlookMoveDownBlock,
    OverlookMoveLeftBlock,
    OverlookMoveRightBlock,
    //000
    OverlookMoveEast000Block,
    OverlookMoveWeat000Block,
    OverlookMoveSouth000Block,
    OverlookMoveNorth000Block,
    //090
    OverlookMoveEast090Block,
    OverlookMoveWeat090Block,
    OverlookMoveSouth090Block,
    OverlookMoveNorth090Block,
    //180
    OverlookMoveEast180Block,
    OverlookMoveWeat180Block,
    OverlookMoveSouth180Block,
    OverlookMoveNorth180Block,
    //270
    OverlookMoveEast270Block,
    OverlookMoveWeat270Block,
    OverlookMoveSouth270Block,
    OverlookMoveNorth270Block,
    //Loop
    LoopUpBlock,
    LoopDownBlock,
};

public class AllBlockKind : MonoBehaviour{
//方格生成位置設定

    [SerializeField]
    GameObject StartReadBlock;

    [SerializeField]
    GameObject DigHoleBlock;
    [SerializeField]
    GameObject TakeItemBlock;

    [SerializeField]
    GameObject MoveFontBlock;
    [SerializeField]
    GameObject MoveBackBlock;

    [SerializeField]
    GameObject OpenUmbrellaBlock;

    [SerializeField]
    GameObject GiveFoodToInterRoleBlock;
    [SerializeField]
    GameObject OverlookDigHoleBlock;

    [SerializeField]
    [Header("abs")]
    GameObject OverlookMoveUpBlock;
    [SerializeField]
    GameObject OverlookMoveDownBlock;
    [SerializeField]
    GameObject OverlookMoveLeftBlock;
    [SerializeField]
    GameObject OverlookMoveRightBlock;

    [SerializeField]
    [Header("000")]
    GameObject OverlookMoveEast000Block;
    [SerializeField]
    GameObject OverlookMoveWeat000Block;
    [SerializeField]
    GameObject OverlookMoveSouth000Block;
    [SerializeField]
    GameObject OverlookMoveNorth000Block;

    [SerializeField]
    [Header("090")]
    GameObject OverlookMoveEast090Block;
   [SerializeField]
    GameObject OverlookMoveWeat090Block;
   [SerializeField]
    GameObject OverlookMoveSouth090Block;
    [SerializeField]
    GameObject OverlookMoveNorth090Block;

    [SerializeField]
    [Header("180")]
    GameObject OverlookMoveEast180Block;
    [SerializeField]
    GameObject OverlookMoveWeat180Block;
    [SerializeField]
    GameObject OverlookMoveSouth180Block;
    [SerializeField]
    GameObject OverlookMoveNorth180Block;

    [SerializeField]
    [Header("270")]
    GameObject OverlookMoveEast270Block;
    [SerializeField]
    GameObject OverlookMoveWeat270Block;
    [SerializeField]
    GameObject OverlookMoveSouth270Block;
    [SerializeField]
    GameObject OverlookMoveNorth270Block;

    [SerializeField]
    [Header("Loop")]
    GameObject LoopUpBlock;
    [SerializeField]
    GameObject LoopDownBlock;


    //儲存生成的物件
    List<GameObject> blockObj = new List<GameObject>();

    //先預設10個位置
    Vector3[] blockPos = new Vector3[10];

    /// <summary>
    /// 在場景產生初始方塊
    /// </summary>
    /// <param name="Content"></param>
    /// <param name="blockKind">參數陣列</param>
    public void InstanceBlock(GameObject Content, params BlockKind[] blockKind)
    {
        blockObj.Clear();
        foreach (var item in blockKind)
        {
            SwitchInstantiateBlock(item);
        }
        SetBlockPos(Content);
    }

    public void InstanceBlock_Array(GameObject Content, LevelBlock[] levelBlock)
    {
        blockObj.Clear();
        if(Content.transform.childCount < levelBlock.Length)
        {
            foreach (var item in levelBlock)
            {
                SwitchInstantiateBlock(item.BlockKinds);
            }
            SetBlockPos(Content);
        }
    }


    /// <summary>
    /// 判斷要生成的Block
    /// </summary>
    /// <param name="blockKind"></param>
    void SwitchInstantiateBlock(BlockKind blockKind)
    {
        switch (blockKind)
        {
            case BlockKind.MoveFontBlock:
                blockObj.Add(Instantiate(MoveFontBlock));
                break;
            case BlockKind.DigHoleBlock:
                blockObj.Add(Instantiate(DigHoleBlock));
                break;
            case BlockKind.TakeItemBlock:
                blockObj.Add(Instantiate(TakeItemBlock));
                break;
            case BlockKind.OpenUmbrellaBlock:
                blockObj.Add(Instantiate(OpenUmbrellaBlock));
                break;
            case BlockKind.GiveFoodToInterRoleBlock:
                blockObj.Add(Instantiate(GiveFoodToInterRoleBlock));
                break;
            case BlockKind.OverlookDigHoleBlock:
                blockObj.Add(Instantiate(OverlookDigHoleBlock));
                break;

            case BlockKind.OverlookMoveUpBlock:
                blockObj.Add(Instantiate(OverlookMoveUpBlock));
                break;
            case BlockKind.OverlookMoveDownBlock:
                blockObj.Add(Instantiate(OverlookMoveDownBlock));
                break;
            case BlockKind.OverlookMoveLeftBlock:
                blockObj.Add(Instantiate(OverlookMoveLeftBlock));
                break;
            case BlockKind.OverlookMoveRightBlock:
                blockObj.Add(Instantiate(OverlookMoveRightBlock));
                break;
                //000
            case BlockKind.OverlookMoveEast000Block:
                blockObj.Add(Instantiate(OverlookMoveEast000Block));
                break;
            case BlockKind.OverlookMoveWeat000Block:
                blockObj.Add(Instantiate(OverlookMoveWeat000Block));
                break;
            case BlockKind.OverlookMoveSouth000Block:
                blockObj.Add(Instantiate(OverlookMoveSouth000Block));
                break;
            case BlockKind.OverlookMoveNorth000Block:
                blockObj.Add(Instantiate(OverlookMoveNorth000Block));
                break;
                //090
            case BlockKind.OverlookMoveEast090Block:
                blockObj.Add(Instantiate(OverlookMoveEast090Block));
                break;
            case BlockKind.OverlookMoveWeat090Block:
                blockObj.Add(Instantiate(OverlookMoveWeat090Block));
                break;
            case BlockKind.OverlookMoveSouth090Block:
                blockObj.Add(Instantiate(OverlookMoveSouth090Block));
                break;
            case BlockKind.OverlookMoveNorth090Block:
                blockObj.Add(Instantiate(OverlookMoveNorth090Block));
                break;
                //180
            case BlockKind.OverlookMoveEast180Block:
                blockObj.Add(Instantiate(OverlookMoveEast180Block));
                break;
            case BlockKind.OverlookMoveWeat180Block:
                blockObj.Add(Instantiate(OverlookMoveWeat180Block));
                break;
            case BlockKind.OverlookMoveSouth180Block:
                blockObj.Add(Instantiate(OverlookMoveSouth180Block));
                break;
            case BlockKind.OverlookMoveNorth180Block:
                blockObj.Add(Instantiate(OverlookMoveNorth180Block));
                break;
                //270
            case BlockKind.OverlookMoveEast270Block:
                blockObj.Add(Instantiate(OverlookMoveEast270Block));
                break;
            case BlockKind.OverlookMoveWeat270Block:
                blockObj.Add(Instantiate(OverlookMoveWeat270Block));
                break;
            case BlockKind.OverlookMoveSouth270Block:
                blockObj.Add(Instantiate(OverlookMoveSouth270Block));
                break;
            case BlockKind.OverlookMoveNorth270Block:
                blockObj.Add(Instantiate(OverlookMoveNorth270Block));
                break;
            case BlockKind.LoopUpBlock:
                blockObj.Add(Instantiate(LoopUpBlock));
                break;
            case BlockKind.LoopDownBlock:
                blockObj.Add(Instantiate(LoopDownBlock));
                break;
        }
    }

    /// <summary>
    /// 設定方格位置
    /// </summary>
    void SetBlockPos(GameObject Content)
    {  
        //設定位置
        int blockWeith = 96;
        for (int i = 0; i < blockPos.Length; i++)
        {
            blockPos[i] = new Vector3(83 + blockWeith * i, 58 , 0);
        }

        //設定方塊
        for (int i = 0; i < blockObj.Count; i++)
        {
            if (blockObj[i] != null)
            {
                blockObj[i].transform.SetParent(Content.transform,false);
                blockObj[i].GetComponent<Block>().leaveClone = true;
                blockObj[i].GetComponent<RectTransform>().anchoredPosition = blockPos[i];
            }
        }
    }

}
