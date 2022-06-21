using System;
using System.Collections;
using System.Collections.Generic;
using Project.Runtime.Global;
using Project.Runtime.Serialization;
using UnityEditor;
using UnityEngine;

public class PersistentPlayerCharacter : MonoBehaviour
{
    public PlayerProfile Profile;
    public static PersistentPlayerCharacter ppc;
    public GameObject playerController;
    public MetaState metaState = MetaState.AWAKE;
    public enum MetaState
    {
        AWAKE,
        LOAD,
        VOID
    };
    private void Awake()
    {
        ppc = this;
        //DontDestroyOnLoad(this);
        //SceneLoadingManager.loader.LoadSceneAdditive(Profile.lastVisitedScene);
    }

    void Start()
    {
        //transform.SetParent(null);
    }

    void Update()
    {
        
    }
}
