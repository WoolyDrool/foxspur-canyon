using System;
using System.Collections;
using System.Collections.Generic;
using Project.Runtime.Global;
using UnityEngine;
using UnityEngine.Audio;

namespace Project.Runtime.UI.Buttons
{
    public class UIButtonPlay : MonoBehaviour
    {
        public Animator fadeAnimator;
        public Animator musicFadeOut;

        public SceneLoadingManager sceneLoader;

        void OnEnable()
        {
            if (!fadeAnimator.gameObject.activeSelf)
                fadeAnimator.gameObject.SetActive(true);
            Debug.Log("Called");
            fadeAnimator.Play("anim_Main_FadeIn");
        }

        public void OnClick()
        {
            StartCoroutine(TransitionToGame());
            Debug.Log("Clicked");
        }

        private void Start()
        {

        }

        private IEnumerator TransitionToGame()
        {
            fadeAnimator.Play("anim_Main_FadeOut");
            musicFadeOut.Play("anim_audio_menuMusicFadeOut");

            yield return new WaitForSeconds(6);

            sceneLoader.LoadScene("scn_Main");
        }
    }

}