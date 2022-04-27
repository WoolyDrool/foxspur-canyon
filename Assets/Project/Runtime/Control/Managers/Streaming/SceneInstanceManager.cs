using System;
using System.Collections;
using Project.Runtime.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Runtime.Control.Managers.Streaming
{
    public class SceneInstanceManager : MonoBehaviour
    {
        private static SceneInstanceManager _sceneInstanceManager;
        public Transform loadInPos;
        public Vector3 loadInCoordinates;
        public PlayerMovement playerController;
        public PersistentPlayerCharacter ppc;
        public Light sun;

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

        void Init()
        {
            if (!ppc)
            {
                ppc = FindObjectOfType<PersistentPlayerCharacter>();
            }
            loadInCoordinates = loadInPos.localPosition;
            //What an amazingly awful way to do this :-D
            playerController = GameManager.instance.playerManager._playerMovement;

            StartCoroutine(WaitToTeleportPlayer());
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            ppc.Profile.lastVisitedScene = scene.name;
            SerializationManager.Save(ppc.Profile);
        }

        IEnumerator WaitToTeleportPlayer()
        {
            yield return PersistentPlayerCharacter.MetaState.AWAKE;
            Debug.Log("Teleported player");
            playerController.ForceNewPosition(loadInCoordinates);
        }
    }
}