using System;
using System.Collections;
using System.Collections.Generic;
using Project.Runtime.Global;
using UnityEngine;
using UnityEngine.Video;
using Project.Runtime.UI.Elements;

namespace Project.Runtime.UI.Menus
{
    public class UIMenuMain : MonoBehaviour
    {
        public PersistentPlayerCharacter persplaychar;
        public GameObject mainUiPanel;
        public Animator fadeAnimator;
        public Animator musicFadeOut;
        public SceneLoadingManager sceneLoader;
        public AudioSource playSound;
        public GameObject sceneBlocker;
        
        public string sceneToLoad = "scn_Intro";
        public string sceneToLoad2;

        public float videoDuration = 9;
        public VideoPlayer introMovie;
        public GameObject volume;
        public bool skipIntro;

        void OnEnable()
        {
            //Sanity check after quitting from pause menu
            Time.timeScale = 1;
            volume.SetActive(false);
            persplaychar.metaState = PersistentPlayerCharacter.MetaState.VOID;
            StartCoroutine(PlayIntroMovie());
        }

        private void Update()
        {

        }

        public void OnClick()
        {
            StartCoroutine(TransitionToGame());
            Debug.Log("Clicked");
            playSound.Play();
        }

        public void ToggleIntro()
        {
            skipIntro = !skipIntro;
        }

        private IEnumerator PlayIntroMovie()
        {
            mainUiPanel.SetActive(false);
            introMovie.gameObject.SetActive(true);

            yield return new WaitForSeconds(videoDuration);
            
            introMovie.gameObject.SetActive(false);
            volume.SetActive(true);
            mainUiPanel.SetActive(true);
            sceneBlocker.SetActive(false);
        }
        
        private IEnumerator TransitionToMenu()
        {
            yield return new WaitForSeconds(2);
            Destroy(introMovie.gameObject);
            if (!fadeAnimator.gameObject.activeSelf)
                fadeAnimator.gameObject.SetActive(true);
            fadeAnimator.Play("anim_Main_FadeIn");
        }
        
        private IEnumerator TransitionToGame()
        {
            fadeAnimator.Play("anim_Main_FadeOut");
            musicFadeOut.Play("anim_audio_menuMusicFadeOut");

            yield return new WaitForSeconds(6);

            if (skipIntro)
            {
                sceneLoader.LoadMainWorld();
            }
            else
            {
                sceneLoader.LoadScene(sceneToLoad);
            }
        }
    }

}
