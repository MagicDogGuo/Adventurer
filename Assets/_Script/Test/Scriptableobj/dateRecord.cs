using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//[CreateAssetMenu(menuName = "關卡存檔")]
public class dateRecord : ScriptableObject
{
    public List<RecordData> RecordDatas;
}

[System.Serializable]
public class RecordData
{
    public int Level;
    public bool isPass;
    public Vector3 vector;
}