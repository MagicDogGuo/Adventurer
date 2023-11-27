using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTSCtrl : MonoBehaviour {

    private static TTSCtrl _instance;

    public static TTSCtrl Instance
    {
        get
        {
            if (_instance == null)
            {
                if (_instance == null)
                {
                    GameObject singleton = new GameObject();

                    _instance = singleton.AddComponent<TTSCtrl>();
                    singleton.name = "[Singleton] " + typeof(TTSCtrl).ToString();

                    DontDestroyOnLoad(singleton);

                    Debug.Log("[Singleton] An instance of " + typeof(TTSCtrl) +
                              " is needed in the scene, so '" + singleton +
                              "' was created");
                }
                else
                {
                    Debug.Log("[Singleton] Using instance already created: " +
                              _instance.gameObject.name);
                }
            }

            return _instance;
        }
    }


    private void OnDisable()
    {
        _instance = null;
    }



    bool isTTSComplete = false;


    public bool TTSComplete
    {
        get { return isTTSComplete; }
    }

    public void StartTTS(string tts)
    {
        StopTTS();

        if (tts == "")
            return;

        Mibo.onTTSComplete += onTTSComplete;
#if UNITY_EDITOR
        StartCoroutine(FakeTTS(tts));
#else
        Mibo.startTTS(tts);
#endif  
    }

    

    public void StopTTS()
    {
        isTTSComplete = false;
        Mibo.stopTTS();
    }



    void onTTSComplete(bool isError)
    {
        
        if (!isError)
        {
            isTTSComplete = true;
        }
        Mibo.onTTSComplete -= onTTSComplete;
    }

    IEnumerator FakeTTS(string tts)
    {
        Debug.Log("小丹說 : " +tts);
        yield return new WaitForSecondsRealtime(1f);
        onTTSComplete(false);
    }
}
