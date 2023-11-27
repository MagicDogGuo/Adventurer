using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleStatus : MonoBehaviour {

    RoleContorl m_RoleContorl = null;

    private void Awake()
    {
        m_RoleContorl = GetComponent<RoleContorl>();
    }

    /// <summary>
    /// 背包，拿到的物件
    /// </summary>
    public List<string> RoleBackpack
    {
        get { return m_RoleContorl.roleBackpack; }
    }

    #region 狀態
    /// <summary>
    /// 是否碰到互動角色
    /// </summary>
    public bool IsTouchInterRole
    {
        get { return m_RoleContorl.isTouchInterRole; }
    }

    /// <summary>
    /// 拿到關鍵物件的數量
    /// </summary>
    public int TakeKeyItemAmount
    {
        get { return m_RoleContorl.takeKeyItemAmount; }
    }

    /// <summary>
    /// 是否被淋濕
    /// </summary>
    public bool IsWetting
    {
        get { return m_RoleContorl.isWetting; }
    }

    /// <summary>
    /// 撐傘狀態
    /// </summary>
    public bool IsOpenUmbrella
    {
        get { return m_RoleContorl.isOpenUmbrella; }

    }

    public bool IsHappyKebbi
    {
        get { return m_RoleContorl.isHappyKebbi; }
    }

    public bool IsPanicKebbi
    {
        get { return m_RoleContorl.isPanicKebbi; }
    }

    #endregion



    #region 事件(動態)，角色動作、機器人動作對應事件

    /// <summary>
    /// 向前移動事件
    /// </summary>
    public Action<bool> MoveFrontEvent;

    /// <summary>
    /// 挖土事件
    /// </summary>
    public Action<bool> DigHoleEvent;

    /// <summary>
    /// 拿東西事件
    /// </summary>
    public Action<bool> TakeItemEvent;

    /// <summary>
    /// 遮雨事件
    /// </summary>
    public Action<bool> OpenUmbrellaEvent;

    public Action<bool> GiveFoodToInterRoleEvent;

    public Action<bool> OverlookDigHoleEvent;

    public Action<bool> OverlookMoveUpEvent;
    public Action<bool> OverlookMoveDownEvent;
    public Action<bool> OverlookMoveLeftEvent;
    public Action<bool> OverlookMoveRightEvent;

    public Action<bool> OverlookMoveEast000Event;
    public Action<bool> OverlookMoveWeat000Event;
    public Action<bool> OverlookMoveSouth000Event;
    public Action<bool> OverlookMoveNorth000Event;

    public Action<bool> OverlookMoveEast090Event;
    public Action<bool> OverlookMoveWeat090Event;
    public Action<bool> OverlookMoveSouth090Event;
    public Action<bool> OverlookMoveNorth090Event;

    public Action<bool> OverlookMoveEast180Event;
    public Action<bool> OverlookMoveWeat180Event;
    public Action<bool> OverlookMoveSouth180Event;
    public Action<bool> OverlookMoveNorth180Event;

    public Action<bool> OverlookMoveEast270Event;
    public Action<bool> OverlookMoveWeat270Event;
    public Action<bool> OverlookMoveSouth270Event;
    public Action<bool> OverlookMoveNorth270Event;
    #endregion
}

