using System;
using System.Collections;
using System.Collections.Generic;
using Project.Runtime.Global;
using UnityEngine;
using Project.Runtime.UI.Elements;

public class ProgressSceneLoader : MonoBehaviour
{
    public bool autoLoadScene;
    public string sceneToAudoLoad;
    private SceneLoadingManager _loadingManager;

    private void Awake()
    {
        _loadingManager = GetComponent<SceneLoadingManager>();
        DontDestroyOnLoad(this);

        
        if (autoLoadScene)
        {
            Debug.Log("Loading Scene");
            _loadingManager.LoadScene(sceneToAudoLoad);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
