using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 方塊會做出的動作(整合機器人動作+地圖角色動作)
/// </summary>
public class BlockFunction : MonoBehaviour {
    
    RoleContorl roleContorl;

    /// <summary>
    /// 初始設定
    /// </summary>
    /// <param name="roleObj"></param>
    public void BlockFuntionInit(GameObject roleObj)
    {
        roleContorl = roleObj.GetComponent<RoleContorl>();
    }

    /// <summary>
    /// 往前方塊
    /// </summary>
    public void MoveFront ()
    {
        roleContorl.MoveFront();
    }

    /// <summary>
    /// 挖洞方塊
    /// </summary>
    public void DigHoleBlock ()
    {
        roleContorl.DigHole();
    }

    /// <summary>
    /// 拿物件方塊
    /// </summary>
    public void TakeItemBlock ()
    {
        roleContorl.TakeItem();
    }

    /// <summary>
    /// 遮雨方塊
    /// </summary>
    public void OpenUmbrella ()
    {
        roleContorl.OpenUmbrella();
    }
    /// <summary>
    /// 
    /// </summary>
    public void GiveFoodToInterRole()
    {
        roleContorl.GiveFoodToInterRole();
    }
    /// <summary>
    /// 
    /// </summary>
    public void OverlookDigHole()
    {
        roleContorl.OverlookDigHole();
    }
    /// <summary>
    /// 
    /// </summary>
    public void OverlookMoveUp ()
    {
        roleContorl.OverlookMoveUp();
    }
    /// <summary>
    /// 
    /// </summary>
    public void OverlookMoveDown ()
    {
        roleContorl.OverlookMoveDown();
    }
    /// <summary>
    /// 
    /// </summary>
    public void OverlookMoveLeft ()
    {
        roleContorl.OverlookMoveLeft();
    }
    /// <summary>
    /// 
    /// </summary>
    public void OverlookMoveRight ()
    {
        roleContorl.OverlookMoveRight();

    }
    /// <summary>
    /// 
    /// </summary>
    public void OverlookMoveEast000 ()
    {
        roleContorl.OverlookMoveEast000();

    }
    /// <summary>
    /// 
    /// </summary>
    public void OverlookMoveWeat000 ()
    {
        roleContorl.OverlookMoveWeat000();

    }
    /// <summary>
    /// 
    /// </summary>
    public void OverlookMoveSouth000 ()
    {
        roleContorl.OverlookMoveSouth000();

    }
    /// <summary>
    /// 
    /// </summary>
    public void OverlookMoveNorth000 ()
    {
        roleContorl.OverlookMoveNorth000();

    }
    /// <summary>
    /// 
    /// </summary>
    public void OverlookMoveEast090 ()
    {
        roleContorl.OverlookMoveEast090();

    }
    /// <summary>
    /// 
    /// </summary>
    public void OverlookMoveWeat090 ()
    {
        roleContorl.OverlookMoveWeat090();

    }
    /// <summary>
    /// 
    /// </summary>
    public void OverlookMoveSouth090 ()
    {
        roleContorl.OverlookMoveSouth090();

    }
    /// <summary>
    /// 
    /// </summary>
    public void OverlookMoveNorth090 ()
    {
        roleContorl.OverlookMoveNorth090();

    }
    /// <summary>
    /// 
    /// </summary>
    public void OverlookMoveEast180 ()
    {
        roleContorl.OverlookMoveEast180();

    }
    /// <summary>
    /// 
    /// </summary>
    public void OverlookMoveWeat180 ()
    {
        roleContorl.OverlookMoveWeat180();

    }
    /// <summary>
    /// 
    /// </summary>
    public void OverlookMoveSouth180 ()
    {
        roleContorl.OverlookMoveSouth180();

    }
    /// <summary>
    /// 
    /// </summary>
    public void OverlookMoveNorth180 ()
    {
        roleContorl.OverlookMoveNorth180();

    }
    /// <summary>
    /// 
    /// </summary>
    public void OverlookMoveEast270 ()
    {
        roleContorl.OverlookMoveEast270();

    }
    /// <summary>
    /// 
    /// </summary>
    public void OverlookMoveWeat270 ()
    {
        roleContorl.OverlookMoveWeat270();

    }
    /// <summary>
    /// 
    /// </summary>
    public void OverlookMoveSouth270 ()
    {
        roleContorl.OverlookMoveSouth270();

    }
    /// <summary>
    /// 
    /// </summary>
    public void OverlookMoveNorth270 ()
    {
        roleContorl.OverlookMoveNorth270();

    }

    /// <summary>
    /// 
    /// </summary>
    public void LoopUp()
    {

    }
    /// <summary>
    /// 
    /// </summary>
    public void LookDown()
    {

    }
}

