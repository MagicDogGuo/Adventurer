using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveByScroll : MonoBehaviour {
    [SerializeField]
    Scrollbar scrollbarMoveUI;

    [SerializeField]
    GameObject scrollObj;

    float oriY;
    const float moveDistnace = 200;

    private void Start()
    {
        oriY = scrollObj.GetComponent<RectTransform>().anchoredPosition.y;

    }

    private void Update()
    {
        float anchoredPositionX = scrollObj.GetComponent<RectTransform>().anchoredPosition.x;
        scrollObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(anchoredPositionX, oriY - scrollbarMoveUI.value* moveDistnace);
    }

}
