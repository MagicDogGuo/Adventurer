using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {

    MapSource m_mapSource = null;
    MapLayer m_mapLayer = null;

    GetAudioSource.BGMKinds m_bGMKinds;
    RoleObject m_roleObject = null;
    InterRoleObject m_interRoleObject = null;
    GetItemObject[] m_getItemObject = null;
    WetObject[] m_wetObject = null;

    float GridZeroX = -5.28f;
    float GridZeroY = 4.32f;

    float GridUnit = 0.96f;

    void Awake() {
        m_mapSource = GetComponent<MapSource>();

        m_mapLayer = GetComponent<MapLayer>();

        m_bGMKinds = m_mapSource.BGM;
        m_roleObject = m_mapSource.RoleObjects;
        m_interRoleObject = m_mapSource.InterRoleObjects;
        m_getItemObject = m_mapSource.GetItemObjects;
        m_wetObject = m_mapSource.WetObjects;

        SetBGM(m_bGMKinds);
    }

    /// <summary>
    /// 設定BGM
    /// </summary>
    void SetBGM(GetAudioSource.BGMKinds bGMKinds)
    {
        GetAudioSource comp = AudioManager.Instance.GetComponent<GetAudioSource>();
        if (comp != null) comp.PlayBGM(bGMKinds);

        if(bGMKinds== GetAudioSource.BGMKinds.MainMenu) Debug.Log("====沒有設定背景音效!====");
    }


    /// <summary>
    /// 設定主角
    /// </summary>
    /// <param name="RoleObj"></param>
    /// <returns></returns>
    public GameObject SetRoleObject(GameObject RoleObj)
    {
        GameObject roleObjs = null;
        roleObjs = Instantiate(RoleObj, m_mapLayer.CharacteLayer.transform);
        roleObjs.transform.localPosition = new Vector2(m_roleObject.ObjPos.x * GridUnit + GridZeroX, m_roleObject.ObjPos.y * -GridUnit + GridZeroY); 
        return roleObjs;
    }
    /// <summary>
    /// 設定互動角色
    /// </summary>
    /// <param name="InterRoleObject"></param>
    /// <returns></returns>
    public GameObject SetInterRoleObject(GameObject InterRoleObject)
    {
        GameObject interRoleObjects = null;
        if (m_interRoleObject.IsAppear)
        {
            interRoleObjects = Instantiate(InterRoleObject, m_mapLayer.CharacteLayer.transform);
            if(interRoleObjects.transform.GetChild(0).GetComponent<SpriteRenderer>()!=null)interRoleObjects.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = m_interRoleObject.ObjSprite;
            //比主角下一層一些，z=0.1
            interRoleObjects.transform.localPosition = new Vector3( m_interRoleObject.ObjPos.x * GridUnit + GridZeroX, m_interRoleObject.ObjPos.y * -GridUnit + GridZeroY, .1f);
        }
        return interRoleObjects;
    }
    /// <summary>
    /// 設定拿取物件
    /// </summary>
    /// <param name="GetItemObj"></param>
    /// <returns></returns>
    public List<GameObject> SetGetItemObject(GameObject GetItemObj)
    {
        List<GameObject> getItemObjs = new List<GameObject>();
        if (m_getItemObject.Length == 0) return null;

        for (int i = 0; i < m_getItemObject.Length; i++)
        {           
            getItemObjs.Add( Instantiate(GetItemObj, m_mapLayer.ObjectLayer.transform));
            getItemObjs[i].GetComponent<GetItemObj>().IsKey = m_getItemObject[i].IsKey;
            getItemObjs[i].GetComponent<GetItemObj>().IsUnderGround = m_getItemObject[i].IsUnderGround;
            getItemObjs[i].GetComponent<GetItemObj>().GetItemType = m_getItemObject[i].GetItemType;
            getItemObjs[i].GetComponent<SpriteRenderer>().sprite = m_getItemObject[i].ObjSprite;
            if (m_getItemObject[i].IsUnderGround)
            {
                getItemObjs[i].transform.localPosition = new Vector3(m_getItemObject[i].ObjPos.x * GridUnit + GridZeroX, m_getItemObject[i].ObjPos.y * -GridUnit + GridZeroY, 2f);
            }
            else
            {
                getItemObjs[i].transform.localPosition = new Vector3(m_getItemObject[i].ObjPos.x * GridUnit + GridZeroX, m_getItemObject[i].ObjPos.y * -GridUnit + GridZeroY, .1f);
            }

            //if (m_getItemObject[i].IsUnderGround) getItemObjs[i].GetComponent<SpriteRenderer>().color = new Color32(171,171,171,255);
        }
        return getItemObjs;
    }
    /// <summary>
    /// 設定沾濕物件
    /// </summary>
    /// <param name="WetObj"></param>
    /// <returns></returns>
    public List<GameObject> SetWetObject(GameObject WetObj)
    {
        List<GameObject> wetObjs = new List<GameObject>();
        if (m_wetObject.Length == 0) return null;

        for (int i = 0; i < m_wetObject.Length; i++)
        {         
            wetObjs.Add(Instantiate(WetObj, m_mapLayer.ObjectLayer.transform));
            wetObjs[i].GetComponent<SpriteRenderer>().sprite = m_wetObject[i].ObjSprite;
            wetObjs[i].transform.localPosition = m_wetObject[i].ObjPos;
            wetObjs[i].transform.localPosition = new Vector2(m_wetObject[i].ObjPos.x * GridUnit + GridZeroX, m_wetObject[i].ObjPos.y * -GridUnit + GridZeroY);
        }
        return wetObjs;
    }

}
