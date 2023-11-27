using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMoveMapper : MonoBehaviour {

    int rotateZ = 0;

    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            this.transform.position += Vector3.right;

            rotateZ += 90;
            transform.Rotate(this.transform.rotation.x, this.transform.rotation.y, rotateZ);
            Debug.Log(rotateZ);    
        }

	}
}
