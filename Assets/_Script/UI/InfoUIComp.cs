using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoUIComp : MonoBehaviour {

    [SerializeField]
    Button CloseBtn;

	// Use this for initialization
	void Start () {
        CloseBtn.onClick.AddListener(delegate { Destroy(transform.parent.gameObject); });
	}
	


    
}
