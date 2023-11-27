using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTest : MonoBehaviour {

    public bool isColl;

    public string st;

	void Start () {
		
	}
	
	void Update () {
		
	}

    void OnTriggerStay2D(Collider2D other)
    {
        if (isColl&other.tag=="Canvas")
        {
            GameObject coObj = other.gameObject;
            Debug.Log(st + " "+coObj.name);
        }
       
    }

  }
