using System;
using System.Collections;
using Project.Runtime.Serialization;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Runtime.Control.Managers.Streaming
{
    public class SceneInstanceManager : MonoBehaviour
    {
        private static SceneInstanceManager _sceneInstanceManager;
        public Transform loadInPos;
        public Vector3 loadInCoordinates;
        public PlayerMovement playerMovement;
        public Light sceneSun;
        public WorldLightingPreset scenePreset;
        public GameObject playerObject;
        public String sceneName;
        
        public static SceneInstanceManager instance
        {
            get
            {
                if (!_sceneInstanceManager)
                {
                    _sceneInstanceManager = FindObjectOfType(typeof(SceneInstanceManager)) as SceneInstanceManager;

                    if (!_sceneInstanceManager)
                    {
                        Debug.LogError("No SceneInstanceManager!");
                    }
                    else
                    {
                        _sceneInstanceManager.Init();
                    }
                }
                
                return _sceneInstanceManager;
            }
        }

        private void OnEnable()
        {
            loadInCoordinates = loadInPos.localPosition;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void Awake()
        {
            _sceneInstanceManager = this;
        }

        void Init()
        {
            
        }
        
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            playerObject = null;
        }
        
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));

            try
            {
                playerObject = GameManager.instance.playerManager.playerTransform.gameObject;
            }
            catch (Exception e)
            {
                Debug.LogWarning("No PlayerController found. Console: " + e);
                PlayerInitialization(Resources.Load("PPC") as GameObject);
                throw;
            }
            
            if (!playerObject)
            {
                
            }
            
            
            else
            {
                Debug.LogAssertion("Initializing");
                
                playerMovement = GameManager.instance.playerManager._playerMovement;
                Debug.LogWarning("Waiting to teleport...");
            }
        }

        void PlayerInitialization(GameObject po)
        {
            playerObject = po;
            Instantiate(playerObject, loadInPos);
            Debug.LogWarning("Spawned PPC");

            try
            {
                playerMovement = GameManager.instance.playerManager._playerMovement;
            }
            catch (Exception e)
            {
                playerMovement = playerObject.GetComponentInChildren<PlayerMovement>();
                Console.WriteLine(e);
                throw;
            }
            
            StartCoroutine(WaitToTeleportPlayer());
        }
        
        IEnumerator WaitToTeleportPlayer()
        {
            yield return PersistentPlayerCharacter.MetaState.AWAKE;
            playerObject.transform.SetParent(null);
            playerMovement.ForceNewPosition(loadInCoordinates);
            Debug.Log("Teleported");
        }
    }
}