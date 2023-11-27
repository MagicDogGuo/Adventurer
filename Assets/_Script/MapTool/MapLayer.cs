using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLayer : MonoBehaviour {

    [SerializeField]
    public GameObject BackGroundLayer;
    [SerializeField]
    public GameObject DecorateLayer;
    [SerializeField]
    public GameObject EditLayer;
    [SerializeField]
    public GameObject ObjectLayer;
    [SerializeField]
    public GameObject CharacteLayer;
    [SerializeField]
    public GameObject ParticleLayer;

    [SerializeField]
    GameObject RoleObjects;
    [SerializeField]
    GameObject InterRoleObjects;
    [SerializeField]
    GameObject GetItemObjects;
    [SerializeField]
    GameObject WetObjects;


    private void Awake()
    {
        RoleObjects.SetActive(false);
        InterRoleObjects.SetActive(false);
        GetItemObjects.SetActive(false);
        WetObjects.SetActive(false);
    }
}
