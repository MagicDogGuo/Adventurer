using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
    Vector3 ToPos;
    // Use this for initialization
    void Start () {
        ToPos = new Vector3(this.transform.position.x, this.transform.position.y + 2, this.transform.position.z);

    }

    // Update is called once per frame
    void Update () {
        this.transform.position = Vector3.Lerp(this.transform.position, ToPos, 0.5f*Time.deltaTime);
	}

}
