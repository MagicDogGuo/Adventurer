using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InterRoleContorl : MonoBehaviour {

    public bool isFoodFull = false;

    InterRoleStatus m_interRoleStatus = null;

	void Start () {
        m_interRoleStatus = GetComponent<InterRoleStatus>();

        MainGameManager.Instance.RoleObjs.GetComponent<RoleStatus>().GiveFoodToInterRoleEvent += ExecuteIAmFullEvent;
        MainGameManager.Instance.RoleObjs.GetComponent<RoleStatus>().GiveFoodToInterRoleEvent += ExecuteIAmConfuseEvent;
    }

    void ExecuteIAmFullEvent(bool isSus)
    {
        if(isSus) isFoodFull = true;

        m_interRoleStatus.IAmFullEvent(isSus);
    }

    void ExecuteIAmConfuseEvent(bool isSus)
    {
        if(!isSus) isFoodFull = false;

        m_interRoleStatus.IAmConfuseEvent(isSus);
    }


    #region Trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<RoleContorl>() != null)
        {
            m_interRoleStatus.IAmHappyEvent(true);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
    #endregion
}
