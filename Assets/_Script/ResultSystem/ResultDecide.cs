using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultDecide : MonoSingleton<ResultDecide> {

    bool m_isSuccess = false;

    public bool CheckResult(int level, RoleStatus roleStatus)
    {
        bool m_RoleIsTouchInterRole = roleStatus.IsTouchInterRole;
        int m_RoleTakeItemAmount = roleStatus.TakeKeyItemAmount;
        bool m_RoleIsWetting = roleStatus.IsWetting;
        bool m_RoleIsOpenUmbrella = roleStatus.IsOpenUmbrella;
        bool m_RoleIsHapptKebbi = roleStatus.IsHappyKebbi;
        bool m_RoleIsPanicKebbi = roleStatus.IsPanicKebbi;


        ResultData m_resultDecideRow = SaveLoadLevelData.Instance.FetchResultDataFromLevelNo(level);// DatabaseManager.Instance.FetchRoleFromLevel_ResultDecideRow(level);
        bool m_istouchInterRoleData = m_resultDecideRow.Role_IsTouchInterRole;
        int m_takeItemAmountData = m_resultDecideRow.Role_TakeKeyItemAmount;
        bool m_isWettingData = m_resultDecideRow.Role_IsWetting;
        bool m_isOpenUmbrellaData = m_resultDecideRow.Role_IsOpenUmbrella;
        bool m_isHappyKebbiData = m_resultDecideRow.Role_IsHappyKebbi;
        bool m_isSadKebbiData = m_resultDecideRow.Role_IsPanicKebbi;

        if (m_istouchInterRoleData == m_RoleIsTouchInterRole &&
            m_takeItemAmountData == m_RoleTakeItemAmount &&
            m_isWettingData == m_RoleIsWetting &&
            m_isOpenUmbrellaData == m_RoleIsOpenUmbrella &&
            m_isHappyKebbiData == m_RoleIsHapptKebbi &&
            m_isSadKebbiData == m_RoleIsPanicKebbi)
        {
            m_isSuccess = true;
            return true;
        }
        else
        {
            m_isSuccess = false;
            return false;
        }
    }

    public void SavePassLevel()
    {
        if (MainGameManager.NowLevel < MainGameManager.Instance.MapGridObjArray.Count)
        {
            //SaveLoadLevelData.Instance.FetchLevelPassDataFromLevelNo(MainGameManager.NowLevel + 1).isPass = true;
            DatabaseManager.Instance.LevelPassToJsonSava(MainGameManager.NowLevel + 1, true);
        }
    }

    /// <summary>
    /// 顯示結果
    /// </summary>
    /// <param name="resultUIComp"></param>
    public void ShowResult(ResultUIComp resultUIComp, RoleStatus roleStatus)
    {
        //放物品背包
        List<string> m_RoleBackpack = new List<string>();
        m_RoleBackpack = roleStatus.RoleBackpack;


        if (m_isSuccess)
        {
            resultUIComp.SuccessUI(m_RoleBackpack);
        }
        else
        {
            resultUIComp.FailUI();
        }
    }

#region 舊
    //public ResultDecide()//RoleContorl rolecontrol
    //{
    //    //m_RoleIsTouchInterRole = rolecontrol.GetComponent<RoleStatus>().IsTouchInterRole;
    //    //m_RoleTakeItemAmount = rolecontrol.GetComponent<RoleStatus>().TakeKeyItemAmount;
    //    //m_RoleIsWetting = rolecontrol.GetComponent<RoleStatus>().IsWetting;
    //    //m_RoleIsOpenUmbrella = rolecontrol.GetComponent<RoleStatus>().IsOpenUmbrella;
    //    //m_RoleIsHapptKebbi = rolecontrol.GetComponent<RoleStatus>().IsHappyKebbi;
    //    //m_RoleIsPanicKebbi = rolecontrol.GetComponent<RoleStatus>().IsPanicKebbi;
    //    //m_RoleBackpack = rolecontrol.GetComponent<RoleStatus>().RoleBackpack;
    //}


    //public void CheckResult(int level, ResultUIComp resultUIComp)
    //{
    //    //try
    //    //{
    //    //    ResultDecideRow m_resultDecideRow = DatabaseManager.Instance.FetchRoleFromLevel_ResultDecideRow(level);
    //    //    bool m_istouchInterRoleData = m_resultDecideRow.Role_IsTouchInterRole;
    //    //    int m_takeItemAmountData = m_resultDecideRow.Role_TakeKeyItemAmount;
    //    //    bool m_isWettingData = m_resultDecideRow.Role_IsWetting;
    //    //    bool m_isOpenUmbrellaData = m_resultDecideRow.Role_IsOpenUmbrella;
    //    //    bool m_isHappyKebbiData = m_resultDecideRow.Role_IsHappyKebbi;
    //    //    bool m_isSadKebbiData = m_resultDecideRow.Role_IsPanicKebbi;

    //    //    if (level == DatabaseManager.Instance.FetchRoleFromLevel_ResultDecideRow(level).Level)
    //    //    {
    //    //        CheckLevelPass(resultUIComp, m_istouchInterRoleData, m_takeItemAmountData, m_isWettingData, m_isOpenUmbrellaData, m_isHappyKebbiData, m_isSadKebbiData);
    //    //    }
    //    //}
    //    //catch (System.Exception)
    //    //{
    //    //    throw;
    //    //}

    //}


    //void CheckLevelPass(ResultUIComp resultUIComp, bool role_IsTouchInterRole, int role_TakeKeyItemAmount
    //    , bool role_IsWetting, bool role_IsOpenUmbrella, bool role_IsHappyKebbi,bool role_IsPanicKebbi)
    //{
    //    //if( role_IsTouchInterRole == m_RoleIsTouchInterRole &&
    //    //    role_TakeKeyItemAmount == m_RoleTakeItemAmount &&
    //    //    role_IsWetting == m_RoleIsWetting &&
    //    //    role_IsOpenUmbrella == m_RoleIsOpenUmbrella &&
    //    //    role_IsHappyKebbi == m_RoleIsHapptKebbi &&
    //    //    role_IsPanicKebbi == m_RoleIsPanicKebbi)
    //    //{
    //    //    resultUIComp.SuccessUI("battery");////////////////////////////////////////////////////
    //    //}
    //    //else
    //    //{
    //    //    resultUIComp.FailUI(1);
    //    //}
    //}
#endregion
}
