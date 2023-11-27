using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mvoe : MonoBehaviour {

    [SerializeField]
    dateRecord dateRecords;

    int i = 0;
    private void OnMouseDown()
    {
        i++;
        this.gameObject.transform.position=new Vector3(this.gameObject.transform.position.x,
            this.gameObject.transform.position.y,i);
    }

    public void Save()
    {
        foreach (var item in dateRecords.RecordDatas)
        {
            item.vector = Vector3.zero;
        }
        
    }
    public void Load()
    {
        foreach (var item in dateRecords.RecordDatas)
        {
            this.gameObject.transform.position = item.vector;
        }
        
    }
}
