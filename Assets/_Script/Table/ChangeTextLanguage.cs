using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ChangeTextLanguage : MonoBehaviour {

    [SerializeField]
    [Header("對應多國語言String_ID")]
    string String_ID_Key;

    TextMeshProUGUI contentText;

	void Start () {
        contentText = this.GetComponent<TextMeshProUGUI>();
        if (contentText != null && String_ID_Key != string.Empty)
        {
            contentText.text = String_ChangeTextLang( String_ID_Key);
        }
        else
        {
            Debug.LogWarning("未抓到Comp或未填String_ID");
        }
    }

    public static string String_ChangeTextLang(string String_ID_Key)
    {
        GameContentTextRow GameContentToAdd = DatabaseManager.Instance.FetchFromSrting_ID_GameContentTextRow(String_ID_Key);

        return GameContentToAdd.Content;
    }
}
