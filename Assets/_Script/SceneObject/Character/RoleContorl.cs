using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class RoleContorl : MonoBehaviour {

    //拿到的物件，背包
    public List<string> roleBackpack = new List<string>();

    #region 角色狀態，用來判斷關卡有無過關(只傳給RoleStatus)
    public bool isTouchInterRole = false;
    public int takeKeyItemAmount = 0;
    public bool isWetting = false;
    public bool isOpenUmbrella = false;
    public bool isHappyKebbi = false;
    public bool isPanicKebbi = false;

    #endregion

    #region 角色的其他狀態
    bool isTouchItem = false;

    bool isFrontHaveObstacle = false;
    bool isBackHaveObstacle = false;
    bool isUpHaveObstacle = false;
    bool isDownHaveObstacle = false;

    #endregion

    [SerializeField]
    [Header("挖洞物件")]
    GameObject DigHoleObj;

    GameObject m_DigHoleObj;
    GameObject m_DigHoleObjOnCollsionObj;

    //挖洞物件位置
    Vector3 digHoleObjPos;

    //探知前方
    int RaycastLong = 0;

    //現在地圖
    MapGridContent nowTilemap;

    //確認有無在目標物件裡(限制一次執行)
    //bool m_CheckInGoalOne = false;

    RoleStatus m_roleStatus = null;


    private void Start()
    {       
        RaycastLong = 1;
        
        //產生挖洞物件
        InstanceDigHoleObj();
        digHoleObjPos = Vector3.zero;

        //現在地圖
        nowTilemap = MainGameManager.Instance.NowMapGridObjs.GetComponent<MapGridContent>();

        m_roleStatus = this.GetComponent<RoleStatus>();

        //初始設定欄位
        isTouchInterRole = false;
        takeKeyItemAmount = 0;
        isWetting = false;
        isOpenUmbrella = false;
        isHappyKebbi = false;
        isPanicKebbi = false;
    }

    private void Update()
    {
        m_DigHoleObjOnCollsionObj = m_DigHoleObj.GetComponent<DigholeTool>().OnCollisionObj;

        //挖洞物件位置在主角身上時隱藏
        if ((Vector2)m_DigHoleObj.transform.localPosition == Vector2.zero) m_DigHoleObj.GetComponent<SpriteRenderer>().enabled = false;
        else m_DigHoleObj.GetComponent<SpriteRenderer>().enabled = true;

        //判斷前方物件
        CheckAroundWithRay();
        //判斷是否淋濕
        CheckInTheRainWithTrigger();
    }

    /// <summary>
    /// 產生挖洞物件
    /// </summary>
    void InstanceDigHoleObj()
    {
        m_DigHoleObj = Instantiate(DigHoleObj, this.transform);
        m_DigHoleObj.transform.localPosition = digHoleObjPos;
    }

    /// <summary>
    /// 通用移動
    /// </summary>
    /// <param name="direction"></param>
    enum MoveDirection { Up, Down, Left, Right }
    enum CompassRotation {_000 , _090 , _180 , _270 ,noneCompass}
    bool UsualMove(MoveDirection direction, bool isOverlook , CompassRotation compassRotation)
    {
        bool haveObstacle = false;
        switch (direction)
        {
            case MoveDirection.Up:
                haveObstacle = isUpHaveObstacle;
                break;
            case MoveDirection.Down:
                haveObstacle = isDownHaveObstacle;
                break;
            case MoveDirection.Left:
                haveObstacle = isBackHaveObstacle;
                break;
            case MoveDirection.Right:
                haveObstacle = isFrontHaveObstacle;
                break;
        }

        //判斷有無碰到障礙物
        if (!haveObstacle)
        {
            Debug.Log("前進一格");

            //移動時如果挖洞物件不在身上，收回身上
            if ((Vector2)m_DigHoleObj.transform.localPosition != Vector2.zero)
            {
                m_DigHoleObj.transform.localPosition = Vector2.zero;
                m_digHolePosY = 0;
            }

            //移動
            float selfPosX = this.gameObject.transform.position.x;
            float selfPosY = this.gameObject.transform.position.y;
            float plusSelfPosX = 0;
            float plusSelfPosY = 0;

            switch (direction)
            {
                case MoveDirection.Up:
                    plusSelfPosY = selfPosY + 0.96f;
                    StartCoroutine(IE_SmoothMove(selfPosX, plusSelfPosY, true, direction, compassRotation));

                    break;
                case MoveDirection.Down:
                    plusSelfPosY = selfPosY - 0.96f;
                    StartCoroutine(IE_SmoothMove(selfPosX, plusSelfPosY, true, direction, compassRotation));

                    break;
                case MoveDirection.Left:
                    plusSelfPosX = selfPosX - 0.96f;
                    StartCoroutine(IE_SmoothMove(plusSelfPosX, selfPosY, true, direction, compassRotation));

                    break;
                case MoveDirection.Right:
                    plusSelfPosX = selfPosX + 0.96f;
                    if (isOverlook) StartCoroutine(IE_SmoothMove(plusSelfPosX, selfPosY, true, direction, compassRotation));
                    else StartCoroutine(IE_SmoothMove(plusSelfPosX, selfPosY, false, direction, compassRotation));
              
                    break;
            }
            return true;
        }
        else
        {
            Debug.Log("前有障礙無法前進");
           
            //事件
            switch (direction)
            {
                //應該要判斷16種事件的發生，但現在先只用4種(因為表演一樣)
                case MoveDirection.Up:
                    if (m_roleStatus.OverlookMoveUpEvent != null) m_roleStatus.OverlookMoveUpEvent(false);
                    else Debug.LogWarning("往前事件沒註冊(false)"); break;
                case MoveDirection.Down:
                    if (m_roleStatus.OverlookMoveDownEvent != null) m_roleStatus.OverlookMoveDownEvent(false);
                    else Debug.LogWarning("往前事件沒註冊(false)"); break;
                case MoveDirection.Left:
                    if (m_roleStatus.OverlookMoveLeftEvent != null) m_roleStatus.OverlookMoveLeftEvent(false);
                    else Debug.LogWarning("往前事件沒註冊(false)"); break;
                case MoveDirection.Right:
                    if (m_roleStatus.OverlookMoveRightEvent != null) m_roleStatus.OverlookMoveRightEvent(false);
                    else Debug.LogWarning("往前事件沒註冊(false)"); break;
            }


            return false;
        }
    }
    IEnumerator IE_SmoothMove(float plusSelfPosX, float plusSelfPosY, bool isOverlook, MoveDirection moveDirection, CompassRotation compassRotation)
    {
        switch (moveDirection)
        {
            case MoveDirection.Up:
                switch (compassRotation)
                {
                    case CompassRotation._000:
                        if (m_roleStatus.OverlookMoveNorth000Event != null) m_roleStatus.OverlookMoveNorth000Event(true);

                        break;
                    case CompassRotation._090:
                        if (m_roleStatus.OverlookMoveEast090Event != null) m_roleStatus.OverlookMoveEast090Event(true);

                        break;
                    case CompassRotation._180:
                        if (m_roleStatus.OverlookMoveSouth180Event != null) m_roleStatus.OverlookMoveSouth180Event(true);

                        break;
                    case CompassRotation._270:
                        if (m_roleStatus.OverlookMoveWeat270Event != null) m_roleStatus.OverlookMoveWeat270Event(true);

                        break;
                    case CompassRotation.noneCompass:
                        if (m_roleStatus.OverlookMoveUpEvent != null) m_roleStatus.OverlookMoveUpEvent(true);

                        break;
                }
                break;
            case MoveDirection.Down:
                switch (compassRotation)
                {
                    case CompassRotation._000:
                        if (m_roleStatus.OverlookMoveSouth000Event != null) m_roleStatus.OverlookMoveSouth000Event(true);

                        break;
                    case CompassRotation._090:
                        if (m_roleStatus.OverlookMoveWeat090Event != null) m_roleStatus.OverlookMoveWeat090Event(true);

                        break;
                    case CompassRotation._180:
                        if (m_roleStatus.OverlookMoveNorth180Event != null) m_roleStatus.OverlookMoveNorth180Event(true);

                        break;
                    case CompassRotation._270:
                        if (m_roleStatus.OverlookMoveEast270Event != null) m_roleStatus.OverlookMoveEast270Event(true);

                        break;
                    case CompassRotation.noneCompass:
                        if (m_roleStatus.OverlookMoveDownEvent != null) m_roleStatus.OverlookMoveDownEvent(true);

                        break;
                }
                break;
            case MoveDirection.Left:
                switch (compassRotation)
                {
                    case CompassRotation._000:
                        if (m_roleStatus.OverlookMoveWeat000Event != null) m_roleStatus.OverlookMoveWeat000Event(true);

                        break;
                    case CompassRotation._090:
                        if (m_roleStatus.OverlookMoveNorth090Event != null) m_roleStatus.OverlookMoveNorth090Event(true);

                        break;
                    case CompassRotation._180:
                        if (m_roleStatus.OverlookMoveEast180Event != null) m_roleStatus.OverlookMoveEast180Event(true);

                        break;
                    case CompassRotation._270:
                        if (m_roleStatus.OverlookMoveSouth270Event != null) m_roleStatus.OverlookMoveSouth270Event(true);

                        break;
                    case CompassRotation.noneCompass:
                        if (m_roleStatus.OverlookMoveLeftEvent != null) m_roleStatus.OverlookMoveLeftEvent(true);

                        break;
                }
                break;
            case MoveDirection.Right:

                switch (compassRotation)
                {
                    case CompassRotation._000:
                        if (m_roleStatus.OverlookMoveEast000Event != null) m_roleStatus.OverlookMoveEast000Event(true);

                        break;
                    case CompassRotation._090:
                        if (m_roleStatus.OverlookMoveSouth090Event != null) m_roleStatus.OverlookMoveSouth090Event(true);

                        break;
                    case CompassRotation._180:
                        if (m_roleStatus.OverlookMoveWeat180Event != null) m_roleStatus.OverlookMoveWeat180Event(true);

                        break;
                    case CompassRotation._270:
                        if (m_roleStatus.OverlookMoveNorth270Event != null) m_roleStatus.OverlookMoveNorth270Event(true);

                        break;
                    case CompassRotation.noneCompass:
                        if (isOverlook)
                        {
                            if (m_roleStatus.OverlookMoveRightEvent != null) m_roleStatus.OverlookMoveRightEvent(true);
                        }
                        else
                        {
                            if (m_roleStatus.MoveFrontEvent != null) m_roleStatus.MoveFrontEvent(true);
                        }

                        break;
                }
                break;
        }


        float moveSpeed = 10;
        yield return new WaitForEndOfFrame();
        while (Math.Abs(this.gameObject.transform.position.x - plusSelfPosX) > 0.001f || Math.Abs(this.gameObject.transform.position.y - plusSelfPosY) > 0.001f)
        {
            Vector3 m_selfPos = this.gameObject.transform.position;
            this.gameObject.transform.position =
                Vector3.Lerp(m_selfPos, new Vector3(plusSelfPosX, plusSelfPosY, m_selfPos.z), 0.5f * Time.deltaTime * moveSpeed);
            yield return null;
        }

    }
 


    #region 角色技能
    /// <summary>
    /// 往前走
    /// </summary>
    public void MoveFront()
    {
        UsualMove(MoveDirection.Right,false,CompassRotation.noneCompass);
    }
  
    /// <summary>
    /// 用延伸方塊確認有無挖到物品(做功能)
    /// </summary>
    float m_digHolePosY = 0;
    Tilemap CanDigTilemap;
    public void DigHole()
    {
        //有無可以挖的Tile
        CanDigTilemap = nowTilemap.CanDigTileMap;

        //Y軸向下
        m_digHolePosY -= 0.96f;
        digHoleObjPos = new Vector3(digHoleObjPos.x, m_digHolePosY, -1);//-1跟目標物件位置相同

        //設定挖土物件位置
        m_DigHoleObj.transform.localPosition = digHoleObjPos;

        //挖土物件位置的世界座標都換算成格子座標
        var tilePos = CanDigTilemap.WorldToCell(m_DigHoleObj.transform.position);


        if(CanDigTilemap != null && CanDigTilemap.HasTile(tilePos))
        {
            //清空挖土物件下的格子
            CanDigTilemap.SetTile(tilePos, null);

            if (m_roleStatus.DigHoleEvent != null) m_roleStatus.DigHoleEvent(true);
            else Debug.LogWarning("挖洞事件沒註冊(true)");
        }
        else
        {
            if (m_roleStatus.DigHoleEvent != null) m_roleStatus.DigHoleEvent(false);
            else Debug.LogWarning("挖洞事件沒註冊(false)");
        }


        //碰到什麼物件
        StartCoroutine(DigCollisonCheck());
    }

    /// <summary>
    /// 碰撞到的物件
    /// </summary>
    /// <returns></returns>
    IEnumerator DigCollisonCheck()
    {
        //collider判斷較慢所以delay0.5f
        yield return new WaitForSeconds(0.5f);

        //確認有無碰到目標物件
        if (m_DigHoleObjOnCollsionObj != null)
        {
            m_DigHoleObjOnCollsionObj.GetComponent<GetItemObj>().IsUnderGround = false;

            Debug.Log("碰到");
        }
        else
        {
            Debug.Log("沒碰到東西");
        }      
    }

    /// <summary>
    /// 拿起物件
    /// </summary>
    public void TakeItem()
    {
        //回到初始值
        isTouchItem = false;

        //確認有無碰到目標物件
        if (m_DigHoleObjOnCollsionObj!= null )
        {
            if (m_DigHoleObjOnCollsionObj.GetComponent<GetItemObj>().IsUnderGround == false)
            {
                isTouchItem = true;
                Debug.Log("碰到地上的東西");
            }
        }

        if (isTouchItem)
        {
            Debug.Log("有拿到東西");

            GameObject digItem = m_DigHoleObj.GetComponent<DigholeTool>().OnCollisionObj;

            //物品(關鍵/不關鍵)新增到背包
            roleBackpack.Add(digItem.GetComponent<GetItemObj>().GetItemType.ToString());
            //拿到關鍵物件
            if (digItem.GetComponent<GetItemObj>().IsKey)
            {
                takeKeyItemAmount++;
            }
            //挖到的物件刪除
            Destroy(digItem);

            //事件
            if (m_roleStatus.TakeItemEvent != null) m_roleStatus.TakeItemEvent(true);
            else Debug.LogWarning("拿東西事件沒註冊(true)");
        }
        else
        {
            Debug.Log("沒有拿到東西");

            //事件
            if (m_roleStatus.TakeItemEvent != null) m_roleStatus.TakeItemEvent(false);
            else Debug.LogWarning("拿東西事件沒註冊(false)");
        }
    }

    /// <summary>
    /// 遮雨
    /// </summary>
    public void OpenUmbrella()
    {
        //身體乾的時候撐傘才進撐傘狀態
        if (isWetting == false)
        {
            isOpenUmbrella = true;
            isWetting = false;

            //事件
            if (m_roleStatus.OpenUmbrellaEvent != null) m_roleStatus.OpenUmbrellaEvent(true);
            else Debug.LogWarning("遮雨事件沒註冊(true)");
        }
        else
        {
            isOpenUmbrella = false;
            isWetting = true;

            //事件
            if (m_roleStatus.OpenUmbrellaEvent != null) m_roleStatus.OpenUmbrellaEvent(false);
            else Debug.LogWarning("遮雨事件沒註冊(false)");
        }
 
    }

    public void GiveFoodToInterRole()
    {
        //碰到InterRole
        if (isTouchInterRole)
        {
            if (roleBackpack.Count == 0)
            {
                isHappyKebbi = false;
                isPanicKebbi = true;

                //事件
                if (m_roleStatus.GiveFoodToInterRoleEvent != null) m_roleStatus.GiveFoodToInterRoleEvent(false);
                else Debug.LogWarning("GiveFoodToInterRoleEvent沒註冊");
            }
            else
            {
                //背包有食物
                foreach (var item in roleBackpack)
                {
                    if (item == GetItemTypes.food.ToString())
                    {
                        isHappyKebbi = true;
                        isPanicKebbi = false;

                        //事件
                        if (m_roleStatus.GiveFoodToInterRoleEvent != null) m_roleStatus.GiveFoodToInterRoleEvent(true);
                        else Debug.LogWarning("GiveFoodToInterRoleEvent沒註冊");
                    }
                    else
                    {
                        isHappyKebbi = false;
                        isPanicKebbi = true;

                        //事件
                        if (m_roleStatus.GiveFoodToInterRoleEvent != null) m_roleStatus.GiveFoodToInterRoleEvent(false);
                        else Debug.LogWarning("GiveFoodToInterRoleEvent沒註冊");
                    }
                }
            }
           
        }
        else
        {
            if (roleBackpack.Count == 0)
            {
                isHappyKebbi = false;
                isPanicKebbi = true;

                //事件
                if (m_roleStatus.GiveFoodToInterRoleEvent != null) m_roleStatus.GiveFoodToInterRoleEvent(false);
                else Debug.LogWarning("GiveFoodToInterRoleEvent沒註冊");
            }
            else
            {
                //背包有食物
                foreach (var item in roleBackpack)
                {
                    if (item == GetItemTypes.food.ToString())
                    {
                        isHappyKebbi = false;
                        isPanicKebbi = true;

                        //事件
                        if (m_roleStatus.GiveFoodToInterRoleEvent != null) m_roleStatus.GiveFoodToInterRoleEvent(false);
                        else Debug.LogWarning("GiveFoodToInterRoleEvent沒註冊");
                    }
                    else
                    {
                        isHappyKebbi = false;
                        isPanicKebbi = true;

                        //事件
                        if (m_roleStatus.GiveFoodToInterRoleEvent != null) m_roleStatus.GiveFoodToInterRoleEvent(false);
                        else Debug.LogWarning("GiveFoodToInterRoleEvent沒註冊");
                    }
                }
            }
        }    
    }
    public void OverlookDigHole()
    {
        //有無可以挖的Tile
        CanDigTilemap = nowTilemap.CanDigTileMap;

       
        digHoleObjPos = new Vector3(digHoleObjPos.x, digHoleObjPos.y, -1);//-1跟目標物件位置相同

        //設定挖土物件位置
        m_DigHoleObj.transform.localPosition = digHoleObjPos;

        //挖土物件位置的世界座標都換算成格子座標
        var tilePos = CanDigTilemap.WorldToCell(m_DigHoleObj.transform.position);


        if (CanDigTilemap != null && CanDigTilemap.HasTile(tilePos))
        {
            //清空挖土物件下的格子
            CanDigTilemap.SetTile(tilePos, null);

            if (m_roleStatus.DigHoleEvent != null) m_roleStatus.DigHoleEvent(true);
            else Debug.LogWarning("挖洞事件沒註冊(true)");
        }
        else
        {
            if (m_roleStatus.DigHoleEvent != null) m_roleStatus.DigHoleEvent(false);
            else Debug.LogWarning("挖洞事件沒註冊(false)");
        }

        //碰到什麼物件
        StartCoroutine(DigCollisonCheck());
    }

    ///abs/////////
    public void OverlookMoveUp()
    {
        UsualMove(MoveDirection.Up,true,CompassRotation.noneCompass);

    }
    public void OverlookMoveDown()
    {
        UsualMove(MoveDirection.Down, true, CompassRotation.noneCompass);

    }
    public void OverlookMoveLeft()
    {
        UsualMove(MoveDirection.Left, true, CompassRotation.noneCompass);

    }
    public void OverlookMoveRight()
    {
        UsualMove(MoveDirection.Right, true, CompassRotation.noneCompass);
    }

    //000/////////
    public void OverlookMoveEast000()
    {
        UsualMove(MoveDirection.Right, true, CompassRotation._000);

    }
    public void OverlookMoveWeat000()
    {
        UsualMove(MoveDirection.Left, true, CompassRotation._000);

    }
    public void OverlookMoveSouth000()
    {
        UsualMove(MoveDirection.Down, true, CompassRotation._000);

    }
    public void OverlookMoveNorth000()
    {
        UsualMove(MoveDirection.Up, true, CompassRotation._000);

    }

    ///090/////////
    public void OverlookMoveEast090()
    {
        UsualMove(MoveDirection.Up, true, CompassRotation._090);

    }
    public void OverlookMoveWeat090()
    {
        UsualMove(MoveDirection.Down, true, CompassRotation._090);

    }
    public void OverlookMoveSouth090()
    {
        UsualMove(MoveDirection.Right, true, CompassRotation._090);

    }
    public void OverlookMoveNorth090()
    {
        UsualMove(MoveDirection.Left, true, CompassRotation._090);

    }

    ///180/////////
    public void OverlookMoveEast180()
    {
        UsualMove(MoveDirection.Left, true, CompassRotation._180);

    }
    public void OverlookMoveWeat180()
    {
        UsualMove(MoveDirection.Right, true, CompassRotation._180);

    }
    public void OverlookMoveSouth180()
    {
        UsualMove(MoveDirection.Up, true, CompassRotation._180);

    }
    public void OverlookMoveNorth180()
    {
        UsualMove(MoveDirection.Down, true, CompassRotation._180);

    }

    ///270/////////
    public void OverlookMoveEast270()
    {
        UsualMove(MoveDirection.Down, true, CompassRotation._270);

    }
    public void OverlookMoveWeat270()
    {
        UsualMove(MoveDirection.Up, true, CompassRotation._270);

    }
    public void OverlookMoveSouth270()
    {
        UsualMove(MoveDirection.Left, true, CompassRotation._270);

    }
    public void OverlookMoveNorth270()
    {
        UsualMove(MoveDirection.Right, true, CompassRotation._270);

    }
    #endregion


    /// <summary>
    /// 確認有無撞到物品
    /// </summary>
    void CheckAroundWithRay()
    {
        isFrontHaveObstacle = RoleCheckRay(Vector3.right);
        isBackHaveObstacle = RoleCheckRay(Vector3.left);
        isUpHaveObstacle = RoleCheckRay(Vector3.up);
        isDownHaveObstacle = RoleCheckRay(Vector3.down);
    }

    bool RoleCheckRay(Vector3 RayDirection)
    {
        bool isHaveObstacle = false;

        Tilemap obstacleTileMap = null;
        //阻擋物件
        if (nowTilemap.ObstacleTileMap != null)
        {
            obstacleTileMap = nowTilemap.ObstacleTileMap;
        }


        RaycastHit2D[] rayhit = Physics2D.LinecastAll(this.transform.position + (RayDirection * RaycastLong), this.transform.position);
        Debug.DrawLine(this.transform.position + (RayDirection * RaycastLong), this.transform.position, Color.red);

        for (int i = 0; i < rayhit.Length; i++)
        {
            if (obstacleTileMap == null) { Debug.LogWarning("沒抓到TileMap"); return isHaveObstacle; }

            if (rayhit[i].collider.gameObject == obstacleTileMap.gameObject)
            {
                Debug.Log("Ray碰到阻擋物件");
                isHaveObstacle = true;
            }

        }

        //計算不是ObstacleTileMap的物件數量
        int inRayobjNoObstacleTileMap = 0;
        foreach (var item in rayhit)
        {
            if (obstacleTileMap == null) { Debug.LogWarning("沒抓到TileMap"); return isHaveObstacle; }

            if (item.collider.gameObject != obstacleTileMap.gameObject)
            {
                inRayobjNoObstacleTileMap++;
            }
        }
        //全部都不是ObstacleTileMap
        if (inRayobjNoObstacleTileMap == rayhit.Length) isHaveObstacle = false;

        return isHaveObstacle;
    }


    /// <summary>
    /// 用是否碰到淋濕物件來決定是否淋濕
    /// </summary>
    void CheckInTheRainWithTrigger()
    {
        if (isOpenUmbrella == false && isTouchWetObj==true)
        {
            Debug.Log("==========淋濕");

            isWetting = true;
            isOpenUmbrella = false;
        }
    }

 

    #region Trigger
    bool isTouchWetObj = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //沾濕物件
        if (collision.GetComponent<WetObj>() != null )
        {
            isTouchWetObj = true;
        }

        //互動角色
        if (collision.GetComponent<InterRoleContorl>() != null)
        {
            isTouchInterRole = true;
         
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //沾濕物件
        if (collision.GetComponent<WetObj>() != null)
        {
            isTouchWetObj = false;
        }

        //互動角色
        if (collision.GetComponent<InterRoleContorl>() != null)
        {
            isTouchInterRole = false;
        }
    }

    #endregion
}

