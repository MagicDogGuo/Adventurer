using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class TestMouseMove : MonoBehaviour {

    private Vector3 offset;

    void OnMouseDown()
    {
        offset = gameObject.transform.position -
            Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
    }

    void OnMouseDrag()
    {
        Vector3 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
        float X = Camera.main.ScreenToWorldPoint(newPosition).x + offset.x;
        float Y = Camera.main.ScreenToWorldPoint(newPosition).y + offset.y;
        transform.position = new Vector3(X, Y,this.transform.position.z);
    }
}

