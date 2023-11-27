using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnkaEditor.IAttribute;
using UnityEngine.UI;

public class PageCreator : MonoBehaviour {


    public RectTransform PageToggleParent;
    public RectTransform PageListParent;

    public PageButton pageButtonPrefab;
    public Sprite SelectedSprite;
    public Sprite NormalSprite;
    public PageToggleGroup pageToggleGroup;
    public ToggleGroup group;

    [SerializeField]
    public ToturialPageClip[] toturialPage;
    public string ToturialPagePath = "";

    private Dictionary<string, ToturialPageClip> ToturialPageDic = new Dictionary<string, ToturialPageClip>();


    //[Button("更新頁面")]
    //public void UpdateToturialPage()
    //{
    //    toturialPage = PageIO.GetAtPath<ToturialPageClip>(ToturialPagePath);
    //}

    private void InitToturialPage()
    {
        for (int i = 0; i < toturialPage.Length; i++)
        {
            if (!ToturialPageDic.ContainsKey(toturialPage[i].Key))
            {
                ToturialPageDic.Add(toturialPage[i].Key, toturialPage[i]);
            }
            else
            {
                Debug.LogError("ToturialPageDic exist key : "+ toturialPage[i].Key);
            }
        }
    }

    public void CreateToturialPage(string level)
    {
        InitToturialPage();

        if (ToturialPageDic.ContainsKey(level))
        {
            ToturialPageClip clip = ToturialPageDic[level];
            for (int i = 0; i < clip.pageGif.Count; i++)
            {
                Animator obj = Instantiate(clip.pageGif[i], PageListParent);
                PageButton btn = Instantiate(pageButtonPrefab, PageToggleParent);
                btn.Init(SelectedSprite, NormalSprite, obj, clip.pageTTS[i]);
                btn.GetComponent<Toggle>().group = group;
                pageToggleGroup.AddPageButton(btn);
            }
            //pageToggleGroup.ToLeft();
        }


    }


}
