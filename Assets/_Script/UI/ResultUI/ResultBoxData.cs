using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(menuName = "資料/結算方塊")]
public class ResultBoxData : ScriptableObject
{
    [SerializeField]
    public List<BoxData> BoxDatas;
}


[Serializable]
public struct BoxData
{
    [SerializeField]
    public GetItemTypes ItmeKey;
    [SerializeField]
    public Sprite ItemSprite;
}