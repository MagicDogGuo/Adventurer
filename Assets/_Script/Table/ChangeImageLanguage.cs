using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[RequireComponent(typeof(Sprite))]
public class ChangeImageLanguage : MonoBehaviour {

    [SerializeField]
    [Header("對應多國語言String_ID")]
    string String_ID_Key;

    [SerializeField]
    bool isOpenChageImg = false;

    Image contentImage;

    void Start()
    {
        contentImage = this.GetComponent<Image>();       

        if (contentImage != null && String_ID_Key != string.Empty)
        {
            contentImage.sprite = String_ChangeImgLang(String_ID_Key);

            if (isOpenChageImg == true)
            {
                switch (CurrLanguage.currLanguage)
                {
                    case SystemLanguage.ChineseSimplified:
                        contentImage.SetNativeSize();

                        break;
                    case SystemLanguage.Chinese:
                        contentImage.SetNativeSize();

                        break;
                    case SystemLanguage.Japanese:
                        contentImage.SetNativeSize();
                        break;
                    case SystemLanguage.English:
                        contentImage.SetNativeSize();
                        break;
                }
            }
        }
        else
        {
            Debug.LogWarning("未抓到Comp或未填String_ID");
        }
    }

    public Sprite String_ChangeImgLang(string String_ID_Key)
    {
        GameContentImageRow GameImageToAdd = DatabaseManager.Instance.FetchFromSrting_ID_GameContentImageRow(String_ID_Key);

        return GameImageToAdd.Sprite_Content;
    }
}
