using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EventHandler : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IDragHandler
{

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerId == -1)
        {
            Debug.Log("Left Mouse Clicked.");
            this.gameObject.GetComponentInChildren<Text>().text = "Left Mouse Clicked";
        }
        else if (eventData.pointerId == -2)
        {
            Debug.Log("Right Mouse Clicked.");
            this.gameObject.GetComponentInChildren<Text>().text = "Right Mouse Clicked";

        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer Enter..");
        this.gameObject.GetComponentInChildren<Text>().text = "Pointer Enter";


    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Pointer Exit..");
        this.gameObject.GetComponentInChildren<Text>().text = "Pointer Exit";
        Time.timeScale = 1;

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pointer Down..");
        this.gameObject.GetComponentInChildren<Text>().text = "Pointer Down";
        Time.timeScale = 3;

    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragged..");
        this.gameObject.GetComponentInChildren<Text>().text = "Dragged";

    }

}
