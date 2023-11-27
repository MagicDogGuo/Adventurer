using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectUtilityTest : MonoBehaviour {

    RectTransform rect;

    void Start () {
        rect = this.GetComponent<RectTransform>();
    }

    void Update()
    {
        Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Debug.Log(RectTransformUtility.RectangleContainsScreenPoint(rect, mousePos));
    }
}
