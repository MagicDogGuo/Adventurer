using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartReadBlock : Block
{
    override protected void CreateConnections()
    {
        //設定自己的類型
        this.blockType = BlockType.BlockTypeInscrution;
        Connection nextConnection = new Connection(this, new Vector2(123, 36), Connection.ConnectionType.ConnectionTypeMale);

        //設定可以吸附的類型
        nextConnection.SetAcceptableBlockType(BlockType.BlockTypeInscrution);

        this.connections.Add(nextConnection);
    }
}
