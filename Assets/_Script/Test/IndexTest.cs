using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndexTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log(this.name.IndexOf("aa"));
        //true=0 false=-1
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
