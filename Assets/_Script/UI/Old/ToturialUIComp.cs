using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 控制面板的開啟或關閉
/// </summary>
public class ToturialUIComp : MonoBehaviour {


    public PageSlider Panel_ToturialUI;
    public PageCreator pageCreator;
    public Button EndGIFBtn;
    
   
    GameObject ControlBlockUICanvas;
    private void Start()
    {
        
        GetComponent<Canvas>().worldCamera = Camera.main ;
        

        if (EndGIFBtn == null)
            Debug.LogError("Can't find EndGIF Button");
        else
        {
           
            EndGIFBtn.onClick.AddListener(CloseToturial);
        }

       
        if (MainGameManager.Instance.CurrentState != MainGameStateControl.GameFlowState.Toturial)
        {
            Debug.Log("Not Toturial .. Find !!!!!!!!!!!!!!!!!!!!!!!!!!!");
            ControlBlockUICanvas = GameObject.Find("ControlBlockUICanvas(Clone)");
            if (ControlBlockUICanvas != null)
                ControlBlockUICanvas.SetActive(false);
        }
        OpenToturial();
        
    }

    private void OnDisable()
    {
        if (ControlBlockUICanvas != null)
            ControlBlockUICanvas.SetActive(true);

        EndGIFBtn.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// 一開始就默認打開
    /// </summary>
    private void OpenToturial()
    {
        Debug.Log("Open Toturial ");
      
        SelectPage();
    }
    /// <summary>
    /// 加上聲音觸發，只供按鈕使用
    /// </summary>
    private void CloseToturial()
    {
        TTSCtrl.Instance.StopTTS();////////////////////////////////////////////////////////
        AudioManager.Instance.GetComponent<GetAudioSource>().PlayButtonSound();

        Debug.Log("Close Toturial");
        Panel_ToturialUI.Hide();
       

        if (GameEventSystem.Instance.OnPushToturailCloseBtn != null)
            GameEventSystem.Instance.OnPushToturailCloseBtn();

        Destroy(transform.gameObject);

        Time.timeScale = 1;
    }

    private void SelectPage()
    {
        
        int level = MainGameManager.NowLevel;
        int bigLevel = level / 3;
        int littleLevel = level % 3;
        string convertLevelStr = (bigLevel+1) + "-" + littleLevel;
        pageCreator.CreateToturialPage(convertLevelStr);
        Panel_ToturialUI.Show();
        Debug.Log("Create Toturial Page : " + convertLevelStr);
    }



    

}

