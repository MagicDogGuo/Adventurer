using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSave : MonoBehaviour {

    [SerializeField]
    Button AllPassBtn;

    [SerializeField]
    Button OriPassBtn;

    int inAssetMapAmount;

    void Start () {
       
        //Debug.Log(DatabaseManager.Instance.FetchFromID_LevelPassRow(3).Level+" "+ DatabaseManager.Instance.FetchFromID_LevelPassRow(3).IsPass);

        AllPassBtn.enabled = false;
        OriPassBtn.enabled = false;

        AllPassBtn.gameObject.SetActive(false);
        OriPassBtn.gameObject.SetActive(false);

        ////讀取asset路徑裡的map
        inAssetMapAmount = LoadAssetFile.Instance.MapsInResouces.Count;
    }

    public void Save()
    {
        DatabaseManager.Instance.LevelPassToJsonSava( 3, true);
        Debug.Log(DatabaseManager.Instance.FetchFromID_LevelPassRow(3).Level + " " + DatabaseManager.Instance.FetchFromID_LevelPassRow(3).IsPass);

    }


    public void AllPassSave()
    {
        StartCoroutine(IE_DalayAll());

    }

    IEnumerator IE_DalayAll()
    {
        for (int i = 1; i <= inAssetMapAmount; i++)
        {
            DatabaseManager.Instance.LevelPassToJsonSava(i, true);
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();

        }
    }

    public void AllOriPassSave()
    {
        StartCoroutine(IE_DalayOri());
    }


    IEnumerator IE_DalayOri()
    {

        DatabaseManager.Instance.LevelPassToJsonSava(1, true);
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        for (int i = 2; i <= inAssetMapAmount; i++)
        {
            DatabaseManager.Instance.LevelPassToJsonSava(i, false);
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();

        }
    }

    int count = 0;
    public void TestBtn()
    {
        count++;
        if (count > 30)
        {

            AllPassBtn.enabled = true;
            OriPassBtn.enabled = true;

            AllPassBtn.gameObject.SetActive(true);
            OriPassBtn.gameObject.SetActive(true);

        }
    }

}
