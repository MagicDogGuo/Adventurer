using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningComp : MonoBehaviour {

    [SerializeField]
    Button CloseWarningBtn;

    [SerializeField]
    GameObject WarningCanvas;

    void Start () {
        CloseWarningBtn.onClick.AddListener(delegate { OnPushCloseWarningBtn(); if (GameEventSystem.Instance.OnPushWarningBtn != null) GameEventSystem.Instance.OnPushWarningBtn(); });
    }
	
    void OnPushCloseWarningBtn()
    {
        Destroy(WarningCanvas);
    }
}
