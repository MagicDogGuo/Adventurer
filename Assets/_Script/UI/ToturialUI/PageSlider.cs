using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制教學UI的左右移動
/// </summary>
public class PageSlider : MonoBehaviour {

    public PageToggleGroup pageToggle;

    public bool Finished;
    private bool CanDrag = false;
    private float touchBeginX;
    private float touchDelta;
    private float TouchDelta = 200f;
    
    

    public void Show(bool IsFirst = false)
    {
        Finished = false;
        pageToggle.OnOpen();

        if (IsFirst)
        {
            Invoke("SetDrag", 0.5f);
        }
        else
        {
            CanDrag = true;

        }
    }

    public void Hide()
    {
        Finished = true;
        CanDrag = false;
    }

    private void Update()
    {

        if (Finished)
            return;

        if (!CanDrag)
            return;

        if (Input.GetMouseButtonDown(0))
        {

            touchBeginX = Input.mousePosition.x;
           
        }


        if (Input.GetMouseButtonUp(0))
        {
            touchBeginX = Input.mousePosition.x;
            
            if (touchDelta > TouchDelta)
            {
                //pageToggle.Left.onClick.Invoke();
                pageToggle.ToLeft();
            }
            else if( touchDelta < -TouchDelta)
            {
                //pageToggle.Right.onClick.Invoke();
                pageToggle.ToRight();
            }
        }

        if (Input.GetMouseButton(0))
        {
            touchDelta = Input.mousePosition.x - touchBeginX;
        }

    }

    

    private void SetDrag()
    {
        CanDrag = true;
    }


}
