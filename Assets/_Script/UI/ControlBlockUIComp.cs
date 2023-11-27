using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlBlockUIComp : MonoBehaviour {


    [SerializeField]
    public GameObject StartBlock;

    [SerializeField]
    [Header("放方塊的content")]
    public GameObject Content;

    [SerializeField]
    [Header("開始讀取方塊")]
    public Button StartCodeBtn;

    [SerializeField]
    [Header("停止方塊進程")]
    public Button StopCodeBtn;

    [SerializeField]
    [Header("暫停遊戲按鈕")]
    public Button PauseGameBtn;

    [SerializeField]
    [Header("拖曳Bar")]
    public GameObject Scrollbar;

    [SerializeField]
    [Header("編輯區域")]
    public GameObject EditorAera;

    [SerializeField]
    [Header("拖曳整個UI的bar")]
    Scrollbar ScrollbarMoveUI;

    [SerializeField]
    [Header("被拖曳的物件")]
    public GameObject ScrollObj;

    float scrollObjOriY;
    const float scrollObjMoveDistnace = 200;

    void Start () {
        StartCodeBtn.onClick.AddListener(delegate { if(GameEventSystem.Instance.OnPushStartCodeBtn!=null)GameEventSystem.Instance.OnPushStartCodeBtn(); OnPushStartCodeBtn(); });

        StopCodeBtn.onClick.AddListener(delegate { if (GameEventSystem.Instance.OnPushStopCodeBtn != null) GameEventSystem.Instance.OnPushStopCodeBtn(); });

        PauseGameBtn.onClick.AddListener(delegate { if(GameEventSystem.Instance.OnPauseGameBtn != null) GameEventSystem.Instance.OnPauseGameBtn(); OnPushPauseGameBtn(); } );

        scrollObjOriY = ScrollObj.GetComponent<RectTransform>().anchoredPosition.y;
        ScrollbarMoveUI.onValueChanged.AddListener(ChangeObjPos);
    }

    void OnPushStartCodeBtn()
    {
        AudioManager.Instance.GetComponent<GetAudioSource>().PlayButtonSound();
    }

    void OnPushPauseGameBtn()
    {
        MainGameManager.Instance.InstantiatePauseGameCanvas();
    }

    void ChangeObjPos(float changeValue)
    {
        float anchoredPositionX = ScrollObj.GetComponent<RectTransform>().anchoredPosition.x;
        ScrollObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(anchoredPositionX, scrollObjOriY - changeValue * scrollObjMoveDistnace);
    }
}
