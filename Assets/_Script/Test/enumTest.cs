using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enumTest : MonoBehaviour {

    [System.Flags]
    public enum EActions
    {
        //這邊我使用 2 的冪次，整數表示，
        //也可使用 16 進制表示，如：0x01、0x08...         
        None = 0,
        Stop = 1,
        Sleep = 2,
        Eat = 4,
        Run = 8,
        All = None | Stop | Sleep | Eat | Run
    };

    // Use this for initialization
    void Start () {
        EActions c = EActions.Eat | EActions.Run;
        //bool s = ( c & EActions.Eat) == EActions.Eat;
        Debug.Log(c & EActions.Run);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
