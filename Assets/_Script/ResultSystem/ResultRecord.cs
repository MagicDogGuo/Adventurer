using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="過關條件")]
public class ResultRecord : ScriptableObject {

    public List<ResultData>  ResultDatas;
}

[System.Serializable]
public class ResultData
{
    public int Level;
    public bool Role_IsTouchInterRole;
    public int Role_TakeKeyItemAmount;
    public bool Role_IsWetting;
    public bool Role_IsOpenUmbrella;
    public bool Role_IsHappyKebbi;
    public bool Role_IsPanicKebbi;
}
