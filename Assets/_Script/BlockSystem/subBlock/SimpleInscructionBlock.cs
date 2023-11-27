using UnityEngine;
using System.Collections;

public class SimpleInscructionBlock : Block {

	override protected void CreateConnections () {
        //設定自己的類型
		this.blockType = BlockType.BlockTypeInscrution;
		Connection previousConnection 	= new Connection(this, new Vector2(0f, 42), Connection.ConnectionType.ConnectionTypeFemale);
		Connection nextConnection 		= new Connection(this, new Vector2(84f, 42), Connection.ConnectionType.ConnectionTypeMale);
		
        //設定可以吸附的類型
		previousConnection.SetAcceptableBlockType(BlockType.BlockTypeInscrution);
        nextConnection.SetAcceptableBlockType(BlockType.BlockTypeInscrution);
		
		this.connections.Add(previousConnection);
		
		this.connections.Add(nextConnection);
	}
}
