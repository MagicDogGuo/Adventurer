using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSource : MonoBehaviour {

    [Header("背景音效")]
    public GetAudioSource.BGMKinds BGM;

    [Header("主角")]
    public RoleObject RoleObjects;

    [Header("互動角色")]
    public InterRoleObject InterRoleObjects;

    [Header("拿取物件")]
    public GetItemObject[] GetItemObjects;

    [Header("沾濕物件")]
    public WetObject[] WetObjects;

    [Header("關卡物件")]
    public LevelBlock[] LevelBlocks;

}

[System.Serializable]
public class RoleObject
{
    public Vector2 ObjPos;
}

[System.Serializable]
public class InterRoleObject
{
    public bool IsAppear;
    public Sprite ObjSprite;
    public Vector2 ObjPos;
}

public enum GetItemTypes
{
    none,
    battery,
    dogToy,
    food,
    magnifyinglens
}

[System.Serializable]
public class GetItemObject
{
    public bool IsKey;
    public bool IsUnderGround;
    public Sprite ObjSprite;
    public Vector2 ObjPos;
    public GetItemTypes GetItemType;
}

[System.Serializable]
public class WetObject
{
    public Sprite ObjSprite;
    public Vector2 ObjPos;
}


[System.Serializable]
public class LevelBlock
{
    public BlockKind BlockKinds;
}
