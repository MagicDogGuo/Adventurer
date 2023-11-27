using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Page的頁面開關
/// </summary>
public class PageToggleGroup : MonoBehaviour {


    private List<PageButton> pageBtns = new List<PageButton>();
    private int Index = 0;

    public Button Left;
    public Button Right;

    private void OnEnable()
    {
        Left.onClick.AddListener(ToLeft);
        Right.onClick.AddListener(ToRight);
    }
    private void OnDisable()
    {
        Left.onClick.RemoveAllListeners();
        Right.onClick.RemoveAllListeners();
    }

    public void AddPageButton(PageButton pageBtn)
    {
        pageBtns.Add(pageBtn);

    }

    public void OnOpen()
    {
        Index = 0;
        ShowPage(Index);
    }

    public void ToLeft() {
        Index--;

        if (Index < 0)
            Index = pageBtns.Count-1;


        ShowPage(Index);
    }

    public void ToRight() {
        Index++;

        if (Index >= pageBtns.Count)
            Index = 0;

        ShowPage(Index);
    }


    private void ShowPage(int pageIndex)
    {
        AudioManager.Instance.GetComponent<GetAudioSource>().PlayButtonSound();

        Debug.Log("Show PageIndex :  "+ pageIndex);
        //TODO 優化成上關下開
        for (int i = 0; i < pageBtns.Count; i++)
        {
            if (i == pageIndex)
            {
                //PageImg.sprite = pageBtns[i].SelectedShow;
                //pageBtns[i].Page.gameObject.SetActive(true);
                pageBtns[i].SetState(PageButtonState.Selected);

            }
            else
            {
                //pageBtns[i].Page.gameObject.SetActive(false);
                pageBtns[i].SetState(PageButtonState.Normal);

            }
        }
    }
}
