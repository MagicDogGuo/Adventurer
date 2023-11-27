using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public abstract class Block : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public class Connection
    {
        const float m_MinimumAttachRadius = 30.0f;//不超過下方拉取方塊區域的高
        public enum ConnectionType
        {
            ConnectionTypeMale,
            ConnectionTypeFemale
        };

        public Block ownerBlock;
        public ConnectionType connectionType;
        public Vector2 relativePos;
        public Block connectedBlock;
        public BlockType acceptableBlockType;

        public Connection(Block ownerBlock, Vector2 relativePos, ConnectionType connectionType)
        {
            this.ownerBlock = ownerBlock;
            this.relativePos = relativePos;
            this.connectionType = connectionType;
        }

        /// <summary>
        /// 設定可以吸附的類型
        /// </summary>
        /// <param name="acceptableBlockType"></param>
        public void SetAcceptableBlockType(BlockType acceptableBlockType)
        {
            this.acceptableBlockType = acceptableBlockType;
        }

        /// <summary>
        /// 抓取與自己連接的方塊
        /// </summary>
        /// <returns></returns>
        public Block GetConnectedBlock()
        {
            return this.connectedBlock;
        }

        /// <summary>
        /// 算出自己磁吸點的絕對位置
        /// </summary>
        /// <returns></returns>
        public Vector2 AbsPos()
        {
            float parentScale = this.ownerBlock.transform.parent.GetComponent<RectTransform>().localScale.x;
            Vector2 absPos = new Vector2(this.ownerBlock.transform.position.x, this.ownerBlock.transform.position.y)
                + new Vector2((this.relativePos.x - this.ownerBlock.GetComponent<RectTransform>().sizeDelta.x / 2) * parentScale,
                              (this.relativePos.y - this.ownerBlock.GetComponent<RectTransform>().sizeDelta.y / 2) * parentScale);
            return absPos;
        }

        /// <summary>
        /// 自己磁吸點與圖上所有磁吸點的距離
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        float DistanceTo(Connection connection)
        {
            return Vector2.Distance(this.AbsPos(), connection.AbsPos());
        }

        /// <summary>
        /// 判斷可否連接方塊，並移動可連接方塊到吸附點
        /// </summary>
        /// <param name="block"></param>
        /// <returns></returns>
        public bool TryAttachWithBlock(Block block, bool isMoveBlock)
        {
            foreach (Connection connection in block.connections)
            { //block.connections:圖上所有方塊的磁吸點(connection)

                //Debug.Log("_a:"+(this.connectionType == connection.connectionType)+"_b:"+ connection.connectedBlock+"_c:"+ this.connectedBlock+"_d:"+((this.acceptableBlockType & block.GetBlockType()) != BlockType.BlockTypeNone));

                if (this.connectionType != connection.connectionType //兩點磁性相反
                    &&
                    connection.connectedBlock == null //要吸的點沒有連接其他方塊
                    &&
                    this.connectedBlock == null //自己沒有連接其他方塊
                    &&
                    (this.acceptableBlockType & block.GetBlockType()) != BlockType.BlockTypeNone //自己與要連接的點的類型不為BlockType.BlockTypeNone
                    &&
                    this.DistanceTo(connection) < m_MinimumAttachRadius)//距離要夠近
                {
                    //Debug.Log(this.ownerBlock.connections.IndexOf(this));///////////////////////////////////////

                    //可以移動方塊
                    if (isMoveBlock == true)
                    {

                        //用要連接的方塊(connections)判斷自己是在要連接的方塊的上方還是下方 0:自己上方 1:自己下方
                        if (this.ownerBlock.connections.IndexOf(this) == 0)//查詢connections(ArrayList)裡是否有this。
                        {//是否在connections的第0個找到this
                            Vector2 delta = connection.AbsPos() - this.AbsPos();

                            //移動自己+與自己相連方塊
                            this.ownerBlock.ApplyDelta(delta);
                        }
                        else
                        {
                            Vector2 delta = this.AbsPos() - connection.AbsPos();

                            //移動自己+與自己相連方塊
                            block.ApplyDelta(delta);
                        }
                        this.connectedBlock = block;//設置連接的方塊(自己)
                        connection.connectedBlock = this.ownerBlock;//設置與自己連接的方塊連到自己
                    }
                    return true;
                }
            }
            return false;
        }



        /// <summary>
        /// 斷開磁吸點
        /// </summary>
        public void Detach()
        {
            if (this.connectedBlock != null)//自己有連接方塊
            {
                foreach (Connection connection in this.connectedBlock.connections)
                {
                    if (connection.connectedBlock != null && connection.connectedBlock.Equals(this.ownerBlock))
                    {//找出與自己連接的第一個方塊
                        connection.connectedBlock = null;//清空與自己連接的第一個方塊
                        break;
                    }
                }
                this.connectedBlock = null;//清空自己
            }
        }
    }


    [System.Flags]//讓enum可以使用位元運算
    public enum BlockType
    {
        BlockTypeNone = 0,
        BlockTypeInscrution = 1 << 0 //<<向左偏移(2進位)
    };

    protected ArrayList connections = new ArrayList(); //方塊上的磁吸點
    protected BlockType blockType;

    [SerializeField]
    public bool leaveClone = false;//在scene手動設定
    [SerializeField]
    public bool staticBlock = false;//在scene手動設定

    public bool IsDraging = false;
    public int IsDragingBlockAmount = 0;
   

    //紀錄連接到哪一端
    Connection.ConnectionType m_beConnectionType;

    //場景中所有的Block物件
    GameObject[] AllBlockGOs = null;

    [SerializeField]
    [Header("預覽連接物件")]
    GameObject PreContect;

    //預覽連接物件
    GameObject perviewGo = null;
    //最近生成的預覽連接物件
    Block lastPerConnectBlock = null;

    //可以移動方塊
    [HideInInspector]
    public bool isCanMoveBlock;

    public BlockType GetBlockType()
    {
        return this.blockType;
    }

    protected abstract void CreateConnections();

    void Start()
    {
        CreateConnections();

        GameEventSystem.Instance.OnPushStopCodeBtn += ResetBlockColor;
    }


    private void Update()
    {
        ResignNowPlayBlockSequenceEvent();
        //Debug.Log(timer);
        //if (timer > canDragTime)
        //{
        //    BlockChangeColor(new Color32(130, 130, 130, 255), this.gameObject);
        //}
        //canDragTime = TestSpeed.BlockPushSpeed;
    }


    void NowExeSqu(int x)
    {
        //Debug.Log("x: " + x+"  "+ DescendingBlocksForStartBlock_GameObj().Count);

        for (int i = 0; i < DescendingBlocksForStartBlock_GameObj().Count; i++)
        {
            if (i == x)
            {
                DescendingBlocksForStartBlock_GameObj()[i].GetComponent<Image>().color = new Color32(255, 62, 62, 255);
            }
            if (i != x)
            {
                DescendingBlocksForStartBlock_GameObj()[i].GetComponent<Image>().color = Color.white;
            }
        }
    }
    void ResetBlockColor()
    {
        NowExeSqu(-1);
    }

    bool checkone = false;
    void ResignNowPlayBlockSequenceEvent()
    {
        //測試用，StartBlock上，讀現在正在執行何種方塊
        if (staticBlock == true && MainGameManager.Instance.ExecuteBlockObjs == null) return;
        if (staticBlock == true && MainGameManager.Instance.ExecuteBlockObjs.GetComponent<ExecuteBlock>() != null && checkone == false)
        {   //ExecuteBlockState進入

            MainGameManager.Instance.ExecuteBlockObjs.GetComponent<ExecuteBlock>().NowPlayBlockSequenceEvent += NowExeSqu;
            checkone = true;
        }
    }


    /// <summary>
    /// 畫出磁鐵
    /// </summary>
	void OnDrawGizmos()
    {
        foreach (Connection connection in this.connections)
        {
            if (connection.connectionType == Connection.ConnectionType.ConnectionTypeMale)
            {
                Gizmos.color = Color.blue;
            }
            else
            {
                Gizmos.color = Color.red;
            }
            Gizmos.DrawSphere(connection.AbsPos(), 10);
        }
    }

    /// <summary>
    /// 斷開磁吸點
    /// </summary>
    public void Detach()
    {
        Connection firstConnection = this.connections[0] as Connection;
        firstConnection.Detach();
    }

    /// <summary>
    /// 自己+與自己相連方塊的移動
    /// </summary>
    /// <param name="delta"></param>
    public void ApplyDelta(Vector2 delta)
    {
        ArrayList descendingBlocks = DescendingBlocks(); //自己+連接的方塊
        foreach (Block block in descendingBlocks)
        {
            block.transform.position = block.transform.position + new Vector3(delta.x, delta.y);
        }
    }

    public void IsDragingBlock(bool isdrag)
    {
        ArrayList descendingBlocks = DescendingBlocks(); //自己+連接的方塊
        IsDraging = isdrag;
        if (isdrag)
        {
            IsDragingBlockAmount = descendingBlocks.Count;
        }
        else
        {
            IsDragingBlockAmount = 0;
        }
    }

    /// <summary>
    /// 判斷連接有無可以連接的方塊，並回傳bool
    /// </summary>
    /// <param name="block"></param>
    /// <param name="isMoveBlock"></param>
    /// <returns></returns>
    public bool TryAttachInSomeConnectionWithBlock(Block block, bool isMoveBlock)
    {
        if (this.Equals(block))
        {
            return false;
        }

        ArrayList descendingBlocks = this.DescendingBlocks();
        foreach (Block aBlock in descendingBlocks)
        {
            foreach (Connection conection in aBlock.connections)
            {
                if (conection.TryAttachWithBlock(block, isMoveBlock))
                {
                    if (isMoveBlock == false)
                    {
                        //紀錄連接到哪一端
                        m_beConnectionType = conection.connectionType;
                    }
                    return true;
                }

            }
        }
        return false;
    }
    /// <summary>
    /// ///////////////////////////////////////////////////////////////////////////
    /// </summary>
    /// <returns></returns>
    public List<GameObject> DescendingBlocks_GameObj()
    {
        List<GameObject> arrayListGO = new List<GameObject>();
        //自己的方塊
        arrayListGO.Add(this.gameObject);

        //其他的方塊
        for (int i = 1; i < this.connections.Count; ++i)//connections.Count方塊磁吸點數量(不計算第一個磁吸點，因為有先+自己了)
        {
            Connection connection = this.connections[i] as Connection;
            if (connection.GetConnectedBlock() != null && connection.GetConnectedBlock().Equals(this) == false)//不是連接到自己
            {
                List<GameObject> descendingBlocks = connection.GetConnectedBlock().DescendingBlocks_GameObj();//抓到連接的方塊後，連接的方塊繼續往下抓(自動往下抓)，從最後一個方塊開始回傳值(得到回傳值後才會繼續往下)
                foreach (GameObject block in descendingBlocks)
                {
                    arrayListGO.Add(block.gameObject);//與自己連接的方塊
                }
            }
        }

        return arrayListGO;
    }

    /// <summary>
    /// 測試用///////////////////////////////////////////////////////////////////////////
    /// </summary>
    /// <returns></returns>
    public List<GameObject> DescendingBlocksForStartBlock_GameObj()
    {
        List<GameObject> arrayListGO = new List<GameObject>();

        //後面其他的方塊(除去自己)
        Connection connection = this.connections[0] as Connection;//第一個磁吸點

        if (connection.GetConnectedBlock() != null && connection.GetConnectedBlock().Equals(this) == false)//不是連接到自己
        {
            List<GameObject> descendingBlocks = connection.GetConnectedBlock().DescendingBlocks_GameObj();//抓到連接的方塊後，連接的方塊繼續往下抓(自動往下抓)，從最後一個方塊開始回傳值(得到回傳值後才會繼續往下)
            foreach (GameObject block in descendingBlocks)
            {
                arrayListGO.Add(block.gameObject);//與自己連接的方塊
            }
        }
        return arrayListGO;
    }


    /// <summary>
    /// 自己+與自己連接的方塊
    /// </summary>
    /// <returns></returns>
    public ArrayList DescendingBlocks()
    {
        ArrayList arrayList = new ArrayList();
        //自己的方塊
        arrayList.Add(this);

        //其他的方塊
        for (int i = 1; i < this.connections.Count; ++i)//connections.Count方塊磁吸點數量(不計算第一個磁吸點，因為有先+自己了)
        {
            Connection connection = this.connections[i] as Connection;
            if (connection.GetConnectedBlock() != null && connection.GetConnectedBlock().Equals(this) == false)//不是連接到自己
            {
                ArrayList descendingBlocks = connection.GetConnectedBlock().DescendingBlocks();//抓到連接的方塊後，連接的方塊繼續往下抓(自動往下抓)，從最後一個方塊開始回傳值(得到回傳值後才會繼續往下)
                foreach (Block block in descendingBlocks)
                {
                    arrayList.Add(block);//與自己連接的方塊
                }
            }
        }

        return arrayList;
    }

    /// <summary>
    /// Start方塊抓取後面接的方塊(在陣列中除去自己)
    /// </summary>
    /// <returns></returns>
    public ArrayList DescendingBlocksForStartBlock()
    {
        ArrayList arrayList = new ArrayList();

        //後面其他的方塊(除去自己)
        if (this.connections.Count > 0)
        {
            Connection connection = this.connections[0] as Connection;//第一個磁吸點

            if (connection.GetConnectedBlock() != null && connection.GetConnectedBlock().Equals(this) == false)
            {
                ArrayList descendingBlocks = connection.GetConnectedBlock().DescendingBlocks();//抓到連接的方塊後，連接的方塊繼續往下抓
                                                                                               //Debug.Log(descendingBlocks.Count);
                foreach (Block block in descendingBlocks)
                {
                    arrayList.Add(block);//與自己連接的方塊
                }
            }
        }
        return arrayList;
    }


    /// <summary>
    /// 生成預覽連接物件
    /// </summary>
    /// <param name="beConnectBlock"></param>
    void InstancePerviewGO(Block beConnectBlock)
    {
        if (perviewGo == null)
        {
            //實例化預覽物件
            perviewGo = Instantiate(PreContect);
            perviewGo.transform.SetParent(beConnectBlock.transform);
            lastPerConnectBlock = beConnectBlock;

            //判斷連接正負極位置(正在被被拖曳物件的正負極)
            if (m_beConnectionType == Connection.ConnectionType.ConnectionTypeFemale)
            {
                perviewGo.GetComponent<RectTransform>().anchoredPosition = new Vector3(45, 0, 0);
            }
            if (m_beConnectionType == Connection.ConnectionType.ConnectionTypeMale)
            {
                perviewGo.GetComponent<RectTransform>().anchoredPosition = new Vector3(-45, 0, 0);
            }
        }
    }

    /// <summary>
    /// 刪除預覽連接物件
    /// </summary>
    /// <param name="beConnectBlock"></param>
    void DestroyPerviewGO()
    {
        Destroy(perviewGo);
    }

 

    ControlBlockUIComp controlBlockUIComp = null;
    int nowBlockAmount = 0;
    GameObject lastBlockGo = null;
    public const int limitBlockNum = 40;

    Vector3 oriDragPos = Vector3.zero;
    Vector3 onDragPos = Vector3.zero;
    Vector3 endDragPos = Vector3.zero;
    bool isPointMove = false;
    bool isStartTimer = true;
    float timer = 0;
    float blockMvoeDistance = 0;

    const float canDragDistance = 5f;
    const float canDragTime = 0.065f;
    const float blockTopPosLimit = 110;
    const float blockDownPosLimit = 12;
    const float blockInContentTopY = 190;
    const float blockInContentDownY = 110;
    Vector2 scrollObjPosOri = new Vector2(0, -186.7f);

    void BeginDrag()
    {
        if (staticBlock == true || isCanMoveBlock == false) return;
        
        //找到所有的block物件
        AllBlockGOs = GameObject.FindGameObjectsWithTag("Block");

        IsDragingBlock(true);

        controlBlockUIComp = MainGameManager.Instance.ControlBlockUICanvases.GetComponentInChildren<ControlBlockUIComp>();
        int contentBlockAmount = controlBlockUIComp.Content.transform.childCount;

        if (AllBlockGOs.Length - contentBlockAmount >= limitBlockNum )
        {
            Debug.Log("停止生成方塊");
            if (this.leaveClone == true) MainGameManager.Instance.InstantiateWarningBlockOverAmountCanvas();
            this.Detach();
            return;
        }

        //音效
        AudioManager.Instance.GetComponent<GetAudioSource>().PlayDragSound();

        if (this.leaveClone == true)
        {
            //點擊生成物件
            GameObject go = Instantiate(this.gameObject);
            BlockChangeColor(Color.white, go.gameObject);

            go.GetComponent<Block>().leaveClone = true;
            go.GetComponent<Block>().IsDragingBlock(false);

            go.transform.SetParent(this.transform.parent, false);
            go.transform.SetSiblingIndex(this.transform.GetSiblingIndex());

            this.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
            this.leaveClone = false;

            go.GetComponent<RectTransform>().anchoredPosition = this.GetComponent<RectTransform>().anchoredPosition;
            go.GetComponent<RectTransform>().sizeDelta = this.GetComponent<RectTransform>().sizeDelta;
            go.GetComponent<RectTransform>().anchorMin = this.GetComponent<RectTransform>().anchorMin;
            go.GetComponent<RectTransform>().anchorMax = this.GetComponent<RectTransform>().anchorMax;

        }

        this.Detach();

        if (isPointMove == true)
        {
            //controlBlockUIComp = MainGameManager.Instance.ControlBlockUICanvases.GetComponentInChildren<ControlBlockUIComp>();
            List<GameObject> descendingBlocksForStartBlock_GameObj = controlBlockUIComp.StartBlock.GetComponent<Block>().DescendingBlocksForStartBlock_GameObj();
            nowBlockAmount = descendingBlocksForStartBlock_GameObj.Count;
            if (nowBlockAmount > 0) lastBlockGo = descendingBlocksForStartBlock_GameObj[nowBlockAmount - 1];
        }
    }
    void Draging()
    {
        if (staticBlock == true || isCanMoveBlock == false || leaveClone == true) return;

        onDragPos = this.transform.position;
        blockMvoeDistance = Vector2.Distance(oriDragPos, onDragPos);
        if(!(blockMvoeDistance < canDragDistance))
        {
            BlockChangeColor(Color.white, this.gameObject);
            timer = 0;
        }

        if (lastMousePosition == Vector3.zero)//剛開始生成出來
        {
            lastMousePosition = Input.mousePosition;
        }
        else
        {
            this.ApplyDelta(Input.mousePosition - lastMousePosition); //連接在下方的方塊
            lastMousePosition = Input.mousePosition;
        }

        //產生預覽連接物件，還要做一開始拖曳時不能生物件
        foreach (GameObject allBlockGO in AllBlockGOs)
        {
            Block block = allBlockGO.GetComponent<Block>() as Block;

            //在方塊放置區域的方塊不會產生預覽物件
            if (allBlockGO.transform.parent.tag == "CodeContent" || allBlockGO.transform.parent.tag == "Canvas")
            {
                //判斷有無符合連接條件的方塊
                if (this.TryAttachInSomeConnectionWithBlock(block, false))
                {
                    if (block != lastPerConnectBlock)
                    {//與上個連接物件不相同時
                        DestroyPerviewGO();
                        InstancePerviewGO(block);
                    }
                    else
                    {
                        InstancePerviewGO(block);
                    }
                    break;
                }
                else
                {
                    //必須要是原本產生預覽連接的方塊的判斷，才能刪除預覽物件
                    if (block == lastPerConnectBlock) DestroyPerviewGO();
                }
            }
        }
    }
    void EndDrag()
    {
        if (staticBlock == true || isCanMoveBlock == false || leaveClone == true) return;

        IsDragingBlock(false);

        //設回初始值
        blockMvoeDistance = 0;
        //音效
        AudioManager.Instance.GetComponent<GetAudioSource>().PlayDragEndSound();
        //玩家排列拖曳區
        GameObject codeContentGO = GameObject.FindWithTag("CodeContent");
        RectTransform rect = codeContentGO.GetComponent<RectTransform>();
        Vector2 selfPos = this.transform.position;//new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        //刪除在場上的連接預覽物件
        DestroyPerviewGO();

        if (transform.parent.gameObject.Equals(codeContentGO) == false)
        {//母物件不是codeContentGO(方塊拖曳編輯區域)

            if (isPointMove == true)
            {
                PointMoveBlock(codeContentGO);
            }
            else
            {
                //滑鼠座標有在rect內
                if (RectTransformUtility.RectangleContainsScreenPoint(rect, selfPos))
                {
                    Vector3 previousPosition = this.transform.position;
                    this.transform.SetParent(codeContentGO.transform, false);
                    this.transform.position = previousPosition;
                }
                else
                {
                    Destroy(this.gameObject);
                    return;
                }
            }
        }
        else
        {//母物件是codeContentGO
            if (isPointMove == true)
            {
                //先藏起來功能^^
                //PointMoveBlock(codeContentGO);
            }

            float blockAnchoredPositionY = this.GetComponent<RectTransform>().anchoredPosition.y;
            // 滑鼠座標沒在rect內或超出規定的座標
            if (!RectTransformUtility.RectangleContainsScreenPoint(rect, selfPos) || blockAnchoredPositionY > blockTopPosLimit || blockAnchoredPositionY < blockDownPosLimit)
            {
                //Destroy(this.gameObject);
                List<GameObject> desendBlock = DescendingBlocks_GameObj();
                for (int i = 0; i < desendBlock.Count; i++)
                {
                    Debug.Log("刪除" + desendBlock[i].name);

                    Destroy(desendBlock[i]);
                }
                //停止往下判斷連接其他block
                return;
            }
        }

        lastMousePosition = Vector3.zero;

        //GameObject[] GOs = GameObject.FindGameObjectsWithTag("Block");
        foreach (GameObject GO in AllBlockGOs)
        {
            Block block = GO.GetComponent<Block>();

            if (isPointMove == true && nowBlockAmount > 0)
            {//限定可以連接的方塊           
                if (block.name == lastBlockGo.name)
                {
                    if (this.TryAttachInSomeConnectionWithBlock(block, true))
                    {
                        break;
                    }
                }
            }
            else if (this.TryAttachInSomeConnectionWithBlock(block, true))
            {
                break;
            }
        }
    }

    void PointMoveBlock(GameObject codeContentGO)
    {
        Vector3 oriMovePosition = new Vector3(128.92f, 60, 0);
        this.transform.SetParent(codeContentGO.transform, false);
        //連接的方塊一起移動
        float anchoredPositionX = this.GetComponent<RectTransform>().anchoredPosition.x;
        float anchoredPositionY = this.GetComponent<RectTransform>().anchoredPosition.y;
        Vector3 targetPos = (oriMovePosition + new Vector3(nowBlockAmount * 84, 0));
        Vector3 selfPos = new Vector3(anchoredPositionX, anchoredPositionY, 0);
        this.ApplyDelta(targetPos - selfPos);

        //if (nowBlockAmount > 0)
        //{
        //    Debug.Log(controlBlockUIComp.StartBlock.GetComponent<Block>().DescendingBlocksForStartBlock()[nowBlockAmount - 1]);
        //    lastBlockGo.GetComponent<Image>().color = Color.red;
        //}
    }

    IEnumerator IE_TouchBlockTimer()
    {
        while (isPointMove == false && isStartTimer == true)
        {
            //Debug.Log((timer.ToString()+ "====================")[2]);
            timer += 1 * Time.deltaTime;
            yield return 0;
        }
    }

    void BlockChangeColor(Color color,GameObject go)
    {
        go.GetComponent<Image>().color = color;

    }

    #region Drag
    public void OnBeginDrag(PointerEventData eventData)
    {
        BeginDrag();
    }

    Vector3 lastMousePosition = Vector3.zero;
    public void OnDrag(PointerEventData eventData)
    {
        Draging();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        EndDrag();
    }
    #endregion Drag

    #region Point
    public void OnPointerDown(PointerEventData eventData)
    {
        if (isCanMoveBlock == false) return;
        isStartTimer = true;
        BlockChangeColor(new Color32(130, 130, 130, 255), this.gameObject);
        oriDragPos = this.transform.position;
        isPointMove = false;
        StartCoroutine(IE_TouchBlockTimer());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isCanMoveBlock == false) return;
        isStartTimer = false;
        BlockChangeColor(Color.white, this.gameObject);
        endDragPos = this.transform.position;
        GameObject codeContentGO = GameObject.FindWithTag("CodeContent");////////////////////////////

        ControlBlockUIComp controlBlockUIComp = MainGameManager.Instance.ControlBlockUICanvases.GetComponentInChildren<ControlBlockUIComp>();
        Vector2 scrollObjPos = controlBlockUIComp.ScrollObj.GetComponent<RectTransform>().anchoredPosition;
        float scrollMoveDistanceY = Vector2.Distance(scrollObjPosOri, scrollObjPos);

        if (endDragPos.y < blockInContentTopY- scrollMoveDistanceY && endDragPos.y > blockInContentDownY- scrollMoveDistanceY)
        {
            if (transform.parent.gameObject.Equals(codeContentGO) == false)
            {
                isPointMove = true;
                BeginDrag();
                Draging();
                EndDrag();
            }
        }
        else
        {
            if (blockMvoeDistance < canDragDistance && blockMvoeDistance >= 0)
            {
                if (timer > canDragTime)
                {
                    isPointMove = true;
                    BeginDrag();
                    Draging();
                    EndDrag();
                }
                timer = 0;
            }
            else
            {
                if (transform.parent.gameObject.Equals(codeContentGO) == false)
                {
                    Destroy(this.gameObject);
                }
            }
        }
        StopCoroutine(IE_TouchBlockTimer());
    }
    #endregion Point

}
