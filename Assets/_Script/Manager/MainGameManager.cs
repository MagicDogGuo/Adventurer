using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 傳參、抓取prefab(給各個State呼叫用)
/// </summary>
public class MainGameManager : MonoBehaviour {

    public static int NowLevel = 0;

    [HideInInspector]
    public bool IsReplayByStopCodeBtn = false;
    [HideInInspector]
    public bool IsHaveToturial = false;

    static MainGameManager m_Instance;
    public static MainGameManager Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = FindObjectOfType(typeof(MainGameManager)) as MainGameManager;

                if (m_Instance == null)
                {
                    var gameObject = new GameObject(typeof(MainGameManager).Name);
                    m_Instance = gameObject.AddComponent(typeof(MainGameManager)) as MainGameManager;
                }
            }
            return m_Instance;
        }
    }

    #region 初始物件
    [SerializeField]
    [Header("方塊拖曳UI")]
    GameObject ControlBlockUICanvas;

    [SerializeField]
    [Header("地圖")]
    List<GameObject> MapGridObj;

    [SerializeField]
    [Header("主角")]
    GameObject RoleObj;

    [SerializeField]
    [Header("互動角色")]
    GameObject InterRoleObject;

    [SerializeField]
    [Header("要拿取的物件")]
    GameObject GetItemObj;

    [SerializeField]
    [Header("沾濕物件")]
    GameObject WetObj;


    #endregion

    #region 開始Flow後才生成的物件
    [SerializeField]
    [Header("執行方塊動作物件")]
    GameObject ExecuteBlockObj;

    [SerializeField]
    [Header("顯示目前流程UI")]
    GameObject ShowNowFlowUICanvas;

    [SerializeField]
    [Header("結果UI")]
    GameObject ResultUICanvas;

    [SerializeField]
    [Header("教學UI")]
    GameObject ToturialUICanvas;

    [SerializeField]
    [Header("關卡目標UI")]
    GameObject LevelGoalUICanvas;

    [SerializeField]
    [Header("暫停遊戲UI")]
    GameObject PauseGameUICanvas;

    [SerializeField]
    [Header("警告未填方塊UI")]
    GameObject WarningBlockCanvas;

    [SerializeField]
    [Header("方塊超出數量")]
    GameObject WarningBlockOverAmountCanvas;

    #endregion

    public List<GameObject> MapGridObjArray
    {
        get { return MapGridObj; }
    }

    GameObject controlBlockUICanvas;
    public GameObject ControlBlockUICanvases
    {
        get { return controlBlockUICanvas; }
        set { controlBlockUICanvas = value; }
    }
    GameObject nowMapGridObjs;
    public GameObject NowMapGridObjs
    {
        get { return nowMapGridObjs; }
        set { nowMapGridObjs = value; }
    }
    GameObject roleObjs;
    public GameObject RoleObjs
    {
        get { return roleObjs; }
        set { roleObjs = value; }
    }
    GameObject interRoleObjects;
    public GameObject InterRoleObjects
    {
        get { return interRoleObjects; }
        set { interRoleObjects = value; }
    }
    List<GameObject> getItemObjs;
    public List<GameObject> GetItemObjs
    {
        get { return getItemObjs; }
        set { getItemObjs = value; }
    }
    List<GameObject> wetObjs;
    public List<GameObject> WetObjs
    {
        get { return wetObjs; }
        set { wetObjs = value; }
    }

    GameObject executeBlockObj;
    public GameObject ExecuteBlockObjs
    {
        get { return executeBlockObj; }
        set { executeBlockObj = value; }
    }
    GameObject showNowFlowUICanvas;
    public GameObject ShowNowFlowUICanvases
    {
        get { return showNowFlowUICanvas; }
        set { showNowFlowUICanvas = value; }
    }
    GameObject resultUICanvas;
    public GameObject ResultUICanvases
    {
        get { return resultUICanvas; }
        set { resultUICanvas = value; }
    }
    GameObject handToHandToturialUICanvas;
    public GameObject HandToHandToturialUICanvases
    {
        get { return handToHandToturialUICanvas; }
        set { handToHandToturialUICanvas = value; }
    }
    GameObject levelGoalUICanvas;
    public GameObject LevelGoalUICanvases
    {
        get { return handToHandToturialUICanvas; }
        set { handToHandToturialUICanvas = value; }
    }
    GameObject pauseGameUICanvas;
    public GameObject PauseGameUICanvases
    {
        get { return pauseGameUICanvas; }
        set { pauseGameUICanvas = value; }
    }

    GameObject warningBlockCanvas;
    public GameObject WarningBlockCanvases
    {
        get { return warningBlockCanvas; }
        set { warningBlockCanvas = value; }
    }

    GameObject warningBlockOverAmountCanvas;
    public GameObject WarningBlockOverAmountCanvases
    {
        get { return warningBlockOverAmountCanvas; }
        set { warningBlockOverAmountCanvas = value; }
    }

    /// <summary>
    /// 從StartBlock後面讀取到的方塊
    /// </summary>
    List<string> startBlockArray = null;
    public List<string> StartBlockArray
    {
        get { return startBlockArray; }
        set { startBlockArray = value; }
    }

    // 場景狀態
    MainGameStateControl m_MainGameStateController = new MainGameStateControl();
    // 獲取當前的狀態
    public MainGameStateControl.GameFlowState CurrentState { get { return m_MainGameStateController.GameState; } }

    public void MainGameBegin()
    {
        //讀取asset路徑裡的map
        MapGridObj = LoadAssetFile.Instance.MapsInResouces;//改成固定讀取不要每次進來都讀

        // 設定起始State
        m_MainGameStateController.SetState(MainGameStateControl.GameFlowState.Init, m_MainGameStateController);
    }

    public void MainGameUpdate()
    {
        m_MainGameStateController.StateUpdate();
    }

    /// <summary>
    /// 場景中所有的方塊
    /// </summary>
    /// <returns></returns>
    public GameObject[] AllblockInScene()
    {
        return GameObject.FindGameObjectsWithTag("Block");
    }

    /// <summary>
    /// 生成地圖、UI
    /// </summary>
    public void InstantiateInitObject(int level)
    {
        nowMapGridObjs = Instantiate(MapGridObj[level]);
    }

    public void InstantiateInitUI()
    {
        if (!IsReplayByStopCodeBtn && controlBlockUICanvas == null) { controlBlockUICanvas = Instantiate(ControlBlockUICanvas);  Debug.Log("========生成初始UI========"); }
    }

    /// <summary>
    /// 生成執行方塊動作物件
    /// </summary>
    public void InstantiateExecuteBlockObject()
    {
        if(executeBlockObj == null) executeBlockObj = Instantiate(ExecuteBlockObj);
    }

    /// <summary>
    /// 生成顯示目前流程UI
    /// </summary>
    public void InstantiateShowNowFlowUICanvas()
    {
        if (showNowFlowUICanvas == null) showNowFlowUICanvas = Instantiate(ShowNowFlowUICanvas);
    }

    /// <summary>
    /// 生成結果UI
    /// </summary>
    public void InstantiateResultObject()
    {
        if (resultUICanvas == null) resultUICanvas = Instantiate(ResultUICanvas);
    }

    /// <summary>
    /// 生成教學面板
    /// </summary>
    public void InstantiateHandtoHandToturialCanvas()
    {
        if (handToHandToturialUICanvas == null) handToHandToturialUICanvas = Instantiate(ToturialUICanvas);
    }


    /// <summary>
    /// 生成教學目標面板
    /// </summary>
    public void InstantiateLevelGoalCanvas()
    {
        if (levelGoalUICanvas == null) levelGoalUICanvas = Instantiate(LevelGoalUICanvas);
    }

    /// <summary>
    /// 生成遊戲暫停UI
    /// </summary>
    public void InstantiatePauseGameCanvas()
    {
        if (pauseGameUICanvas == null)pauseGameUICanvas = Instantiate(PauseGameUICanvas);
    }

    /// <summary>
    /// 生成未填方塊警告UI
    /// </summary>
    public void InstantiateWarningBlockCanvas()
    {
        if (warningBlockCanvas == null) warningBlockCanvas = Instantiate(WarningBlockCanvas);
    }

    /// <summary>
    /// 生成超出方塊警告UI
    /// </summary>
    public void InstantiateWarningBlockOverAmountCanvas()
    {
        if (warningBlockOverAmountCanvas == null)warningBlockOverAmountCanvas = Instantiate(WarningBlockOverAmountCanvas);
    }

    /// <summary>
    /// 設定初始物件參數
    /// </summary>
    public void SetInitMapObject()
    {
        //從地圖獲取是否生成、位置、圖片
        MapManager mapManager = null;
        if (nowMapGridObjs.GetComponent<MapManager>() != null) mapManager = nowMapGridObjs.GetComponent<MapManager>();
        else Debug.LogWarning("沒抓到MapManager");

        //設定角色、物件
        roleObjs = mapManager.SetRoleObject(RoleObj);
        interRoleObjects = mapManager.SetInterRoleObject(InterRoleObject);
        getItemObjs = mapManager.SetGetItemObject(GetItemObj);
        wetObjs = mapManager.SetWetObject(WetObj);
    }


    /// <summary>
    /// 設定執行方塊動作物件
    /// </summary>
    public void SetExecuteBlockObj() { }


    public void SetShowNowFloweUICanvas() { }

    /// <summary>
    /// 設定結果UI參數
    /// </summary>
    public void SetResultObject() { }


    /// <summary>
    /// 刪除初始物件
    /// </summary>
    public void DestroyInitObject()
    {
        if(nowMapGridObjs != null) Destroy(nowMapGridObjs);
    }

    public void DestroyInitUI()
    {
        //Debug.Log("ssssssssssssss===================="+ IsReplayByStopCodeBtn);

        if (controlBlockUICanvas != null && !IsReplayByStopCodeBtn) Destroy(controlBlockUICanvas);
    }

    public void DestroyInitMapObject()
    {
        if(roleObjs!=null) Destroy(roleObjs);
        if(interRoleObjects!=null) Destroy(interRoleObjects);

        if (getItemObjs != null)
        {
            foreach (var item in getItemObjs) Destroy(item);
            getItemObjs.Clear();
        }


        if (wetObjs != null)
        {
            foreach (var item in wetObjs) Destroy(item);
            wetObjs.Clear();
        }
    }

    /// <summary>
    /// 刪除執行方塊動作物件
    /// </summary>
    public void DestroyExecuteBlockObj()
    {
        Destroy(executeBlockObj);
    }

    /// <summary>
    /// 刪除顯示目前流程UI
    /// </summary>
    public void DestoryShowNowFlowUICanvas()
    {
        Destroy(showNowFlowUICanvas);
    }

    /// <summary>
    /// 刪除結果UI
    /// </summary>
    public void DestroyResultObject()
    {
        Destroy(resultUICanvas);
    }

    /// <summary>
    /// 刪除教學UI
    /// </summary>
    public void DestroyHandtoHandToturialCanvas()
    {
       if(handToHandToturialUICanvas!=null)Destroy(handToHandToturialUICanvas);
    }

    /// <summary>
    /// 刪除關卡目標
    /// </summary>
    public void DestroyLevelGoalCanvas()
    {
        if (levelGoalUICanvas != null) Destroy(levelGoalUICanvas);
    }

}
