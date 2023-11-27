using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigholeTool : MonoBehaviour {

    [HideInInspector]
    public GameObject OnCollisionObj = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "GetItemObj") OnCollisionObj = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "GetItemObj") OnCollisionObj = null;
    }
}
