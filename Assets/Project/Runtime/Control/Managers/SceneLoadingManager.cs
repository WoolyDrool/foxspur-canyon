using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Project.Runtime.Global
{
    public class SceneLoadingManager : MonoBehaviour
    {
        public static SceneLoadingManager loader;

        [HideInInspector] public AsyncOperation operation;

        public delegate void ChangeLoadingState();
        public static event ChangeLoadingState OnLoad;
        public static event ChangeLoadingState OnComplete;

        public float progress;

        void Awake()
        {
            loader = this;
            DontDestroyOnLoad(gameObject);
        }
        
        public void LoadScene(string sceneName)
        {
            if(OnLoad != null)
                OnLoad();
            StartCoroutine(BeginLoad(sceneName));
        }
        
        public void ReloadCurrentScene()
        {
            Scene curScene = SceneManager.GetActiveScene();
            int id = curScene.buildIndex;
            if(OnLoad != null)
                OnLoad();
            StartCoroutine(BeginReload(id));
        }
        
        private IEnumerator BeginLoad(string sceneName)
        {
            Debug.Log("Loading " + sceneName);
            operation = SceneManager.LoadSceneAsync(sceneName);
        
            while (!operation.isDone)
            {
                progress = operation.progress;
                yield return null;
            }

            operation = null;
            if (OnComplete != null)
                OnComplete();
        }
        
        private IEnumerator BeginReload(int scene)
        {
            operation = SceneManager.LoadSceneAsync(scene);
        
            while (!operation.isDone)
            {
                progress = operation.progress;
                yield return null;
            }

            progress = 100;
            operation = null;
            if (OnComplete != null)
                OnComplete();
        }
        
        
    }
}