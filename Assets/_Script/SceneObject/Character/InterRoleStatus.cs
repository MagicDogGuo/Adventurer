using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterRoleStatus : MonoBehaviour {

    InterRoleContorl m_InterRoleContorl = null;

    private void Awake()
    {
        m_InterRoleContorl = GetComponent<InterRoleContorl>();
    }

    #region 狀態
    /// <summary>
    /// 是否碰到互動角色
    /// </summary>
    public bool IsFoodFull
    {
        get { return m_InterRoleContorl.isFoodFull; }
    }
    #endregion

    #region 動態
    public Action<bool> IAmConfuseEvent;

    public Action<bool> IAmFullEvent;

    public Action<bool> IAmHappyEvent;
    #endregion
}
