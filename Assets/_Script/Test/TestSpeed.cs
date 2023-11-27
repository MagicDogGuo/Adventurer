using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSpeed : MonoBehaviour
{

    static public float BlockPushSpeed;
    public InputField InputField;

    void Start()
    {
        InputField.text = "0.1";
    }

    void Update()
    {
        //BlockPushSpeed = float.Parse(InputField.text);

    }

}
