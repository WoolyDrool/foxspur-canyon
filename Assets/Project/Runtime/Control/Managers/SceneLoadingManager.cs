using System.Collections;
using System.Collections.Generic;
using Project.Runtime.Serialization;
using TMPro;
using UnityEditor;
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
            //DontDestroyOnLoad(gameObject);
        }

        public void LoadMainWorld()
        {
            PersistentPlayerCharacter.ppc.metaState = PersistentPlayerCharacter.MetaState.LOAD;
            if(OnLoad != null)
                OnLoad();
            StartCoroutine(ILoadMainWorld());
        }
        
        private IEnumerator ILoadMainWorld()
        {
            Debug.Log("Loading Main World");
            operation = SceneManager.LoadSceneAsync("Scenes/Levels/MainGameWorld");
        
            while (!operation.isDone)
            {
                progress = operation.progress;
                Debug.Log(progress.ToString());
                yield return null;
            }
            
            LoadSceneAdditive("WuliKupTrail");

            operation = null;
            if (OnComplete != null)
                OnComplete();

        }
        

        public void LoadScene(string sceneName)
        {
            PersistentPlayerCharacter.ppc.metaState = PersistentPlayerCharacter.MetaState.LOAD;
            if(OnLoad != null)
                OnLoad();
            StartCoroutine(BeginLoad(sceneName));
        }

        public void LoadSceneAdditive(string sceneName)
        {
            PersistentPlayerCharacter.ppc.metaState = PersistentPlayerCharacter.MetaState.LOAD;
            if(OnLoad != null)
                OnLoad();
            StartCoroutine(BeginAdditiveLoad(sceneName));
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
                Debug.Log(progress.ToString());
                yield return null;
            }

           //PersistentPlayerCharacter.ppc.Profile.lastVisitedScene = SceneManager.GetActiveScene();
            operation = null;
            PersistentPlayerCharacter.ppc.metaState = PersistentPlayerCharacter.MetaState.AWAKE;
            if (OnComplete != null)
                OnComplete();
        }
        
        private IEnumerator BeginAdditiveLoad(string sceneName)
        {
            Scene prevScene = SceneManager.GetActiveScene();
            SceneManager.UnloadSceneAsync(prevScene);
            Debug.Log("Loading " + sceneName);
            operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        
            //PersistentPlayerCharacter.ppc.Profile.lastVisitedScene = SceneManager.GetActiveScene();
            while (!operation.isDone)
            {
                progress = operation.progress; 
                Debug.Log("LoadProgress: " + progress.ToString());
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