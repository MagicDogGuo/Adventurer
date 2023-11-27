using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "教學頁面")]
public class ToturialPageClip : ScriptableObject {

    public string Key;

    [SerializeField]
    public List<Animator> pageGif;

    [SerializeField]
    public List<string> pageTTS;
}
