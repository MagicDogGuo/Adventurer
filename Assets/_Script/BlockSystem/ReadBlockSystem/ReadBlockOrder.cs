using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadBlockOrder  {

    ArrayList startBlockConnect;

    /// <summary>
    /// 抓取StartBlock連接方塊的陣列
    /// </summary>
    /// <param name="getStartBlockConnectBlocks"></param>
    /// <returns></returns>
    public List<string> ReadBlocksOrder(ArrayList getStartBlockConnectBlocks)
    {
        List<string> funtionName = new List<string>();
        for (int i = 0;i < getStartBlockConnectBlocks.Count; i++)
        {
           funtionName.Add(getStartBlockConnectBlocks[i].GetType().ToString());
        }
        return funtionName;
    }
}
