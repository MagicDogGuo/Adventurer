using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Version : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Text>().text = Application.version; 
	}
	

}
