using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Project.Runtime.Global;

namespace Project.Runtime.UI.Elements
{
    [RequireComponent(typeof(SceneLoadingManager))]
    public class UIElementLoadingScreen : MonoBehaviour
    {
        [SerializeField] private SceneLoadingManager _sceneLoadingManager;
        [SerializeField] private TextMeshProUGUI progressText;
        [SerializeField] private Slider progressSlider;
        
        private Canvas canvas;

        private void OnEnable()
        {
            SceneLoadingManager.OnLoad += LoadScene;
            SceneLoadingManager.OnComplete += ClearProgressUI;
        }

        private void OnDisable()
        {
            SceneLoadingManager.OnLoad -= LoadScene;
            SceneLoadingManager.OnComplete -= ClearProgressUI;
        }

        void Awake()
        {
            _sceneLoadingManager = GetComponent<SceneLoadingManager>();
            canvas = GetComponentInChildren<Canvas>(true);
            DontDestroyOnLoad(gameObject);
        }

        public void LoadScene()
        {
            UpdateProgressUI();
            canvas.gameObject.SetActive(true);

            StartCoroutine(BeginLoad());
        }

        private IEnumerator BeginLoad()
        {
            while (!_sceneLoadingManager.operation.isDone)
            {
                UpdateProgressUI();
                yield return null;
            }

            ClearProgressUI();
        }

        void UpdateProgressUI()
        {
            //progressSlider.value = _sceneLoadingManager.operation.progress;
            //progressText.text = (_sceneLoadingManager.operation.progress * 100) + "%";
        }

        void ClearProgressUI()
        {
            UpdateProgressUI();
            canvas.gameObject.SetActive(false);

            if (gameObject.scene.IsValid())
            {
                Destroy(this.gameObject);
            }
        }
    }

}