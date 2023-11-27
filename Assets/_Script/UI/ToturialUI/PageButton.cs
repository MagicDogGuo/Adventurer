using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PageButton : MonoBehaviour {

    private Sprite m_selectedSprite;
    private Sprite m_normalSprite;

    public Animator Page;
    public string PageTTS_ID;


    private Toggle _toggle;
    private Image _img;
    public void Init(Sprite selectedSprite,Sprite normalSprite, Animator controlPage,string pageTTS_id)
    {
        m_selectedSprite = selectedSprite;
        m_normalSprite = normalSprite;
        Page = controlPage;
        PageTTS_ID = pageTTS_id;

        _toggle = GetComponent<Toggle>();
        _img = GetComponent<Image>();

    }

    public void SetState(PageButtonState pageState)
    {
        if (pageState == PageButtonState.Normal)
        {
            //TTSCtrl.Instance.StopTTS();/////////////////////////////////////////////////////////
            Page.gameObject.SetActive(false);
            _toggle.interactable = false;
            _img.sprite = m_normalSprite;
            _img.SetNativeSize();
        }
        else if (pageState == PageButtonState.Selected)
        {
            //string tts = DatabaseManager.Instance.FetchFromSrting_ID_TutorialTTSRow(PageTTS_ID).Content;
            //TTSCtrl.Instance.StartTTS(tts);
            Page.gameObject.SetActive(true);
            _toggle.interactable = true;
            _img.sprite = m_selectedSprite;
            _img.SetNativeSize();
        }
    }

}

public enum PageButtonState
{
    Selected,
    Normal
}
