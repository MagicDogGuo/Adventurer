using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIComp : MonoBehaviour {

    [SerializeField]
    Button BackTitleBtn;

    [SerializeField]
    Button[] SceneButtons;

    [SerializeField]
    GameObject LoadingUIObj;

    private void Start()
    {
        ////讀取asset路徑裡的map
        //LoadAssetFile mloadAssetFile = new LoadAssetFile();
        //int inAssetMapAmount = mloadAssetFile.LoadPrefabInAsset(Application.dataPath + "/_Prefab/MapGrid/").Count;

        BackTitleBtn.onClick.AddListener(delegate { if(GameEventSystem.Instance.OnPushBackTitleBtn!= null) GameEventSystem.Instance.OnPushBackTitleBtn(); });

        for (int i = 0; i < SceneButtons.Length; i++)
        {
            int level = i + 1;
            SceneButtons[i].onClick.AddListener(delegate { ShowPanel(); });
            SceneButtons[i].onClick.AddListener(delegate { StartCoroutine(IE_DelayLoadScene(level)); });

            //關卡進度
            //SceneButtons[i].interactable = SaveLoadLevelData.Instance.FetchLevelPassDataFromLevelNo(i + 1).isPass;

            SceneButtons[i].interactable = DatabaseManager.Instance.FetchFromID_LevelPassRow(i + 1).IsPass;

        }

        TTSCtrl.Instance.StartTTS(DatabaseManager.Instance.FetchFromString_ID_GameContentTTSRow("GameTitle_01").Content);

        ////如果製作的地圖數量高於現在Menu按鈕的數量
        //if(SceneButtons.Length < inAssetMapAmount)
        //{
        //    Instantiate(SceneButtons[SceneButtons.Length-1], SceneButtons[SceneButtons.Length - 1].transform.parent);
        //}

    }

    void ShowPanel()
    {
        Instantiate(LoadingUIObj);
    }

    IEnumerator IE_DelayLoadScene(int level)
    {
        TTSCtrl.Instance.StopTTS();
        AudioManager.Instance.GetComponent<GetAudioSource>().PlayButtonSound();
        yield return new WaitForSeconds(1f);
        GameEventSystem.Instance.OnPushSceneBtn(level);
    }

    private void OnDestroy()
    {
        BackTitleBtn.onClick.RemoveAllListeners();

        for (int i = 0; i < SceneButtons.Length; i++)
        {
            //int level = i + 1;
            SceneButtons[i].onClick.RemoveAllListeners();
        }
    }

}
