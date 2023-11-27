using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphaButton : MonoBehaviour {

	void Start () {

        this.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.4f;
    }
	
}
