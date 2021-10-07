using System;
using System.Collections;
using System.Collections.Generic;
using Project.Runtime.Gameplay.Vehicles;
using UnityEngine;

namespace Project.Runtime.UI.Elements
{
    public enum UIAnimationTypes
    {
        Move,
        Scale,
        ScaleX,
        ScaleY,
        FadeIn,
        FadeOut,
    }

    public class UITweener : MonoBehaviour
    {
        public GameObject objectToAnimate;

        public UIAnimationTypes animationType;
        public LeanTweenType easeType;
        public float duration;
        public float delay;

        public bool loop;
        public bool pingpong;

        public bool startPositionOffset;
        public Vector2 from;
        public Vector2 to;

        private LTDescr _tweenObject;

        public bool showOnEnable;
        public bool workOnDisable;

        private void OnEnable()
        {
            if (showOnEnable)
            {
                Show();
            }
        }


        public void Show()
        {
            HandleTween();
        }

        public void Restart()
        {
            HandleTween();
            //Debug.Log("Restarting");
        }

        private void HandleTween()
        {
            if (objectToAnimate == null)
            {
                objectToAnimate = gameObject;
            }

            switch (animationType)
            {
                case UIAnimationTypes.FadeIn:
                {
                    FadeIn();
                    break;
                }
                case UIAnimationTypes.FadeOut:
                {
                    FadeOut();
                    break;
                }
                case UIAnimationTypes.Move:
                {
                    Move();
                    break;
                }
                case UIAnimationTypes.Scale:
                {
                    Scale();
                    break;
                }
                case UIAnimationTypes.ScaleX:
                {
                    ScaleX();
                    break;
                }
                case UIAnimationTypes.ScaleY:
                {
                    ScaleY();
                    break;
                }
            }

            _tweenObject.setDelay(delay);
            _tweenObject.setEase(easeType);

            if (loop)
            {
                _tweenObject.loopCount = int.MaxValue;
            }

            if (pingpong)
            {
                _tweenObject.setLoopPingPong();
            }
        }

        private void FadeIn()
        {
            if (gameObject.GetComponent<CanvasGroup>() == null)
            {
                gameObject.AddComponent<CanvasGroup>();
            }

            if (startPositionOffset)
            {
                objectToAnimate.GetComponent<CanvasGroup>().alpha = from.x;
            }

            _tweenObject = LeanTween.alphaCanvas(objectToAnimate.GetComponent<CanvasGroup>(), to.x, duration);
        }
        
        private void FadeOut()
        {
            if (gameObject.GetComponent<CanvasGroup>() == null)
            {
                gameObject.AddComponent<CanvasGroup>();
            }

            if (startPositionOffset)
            {
                objectToAnimate.GetComponent<CanvasGroup>().alpha = from.x;
            }

            _tweenObject = LeanTween.alphaCanvas(objectToAnimate.GetComponent<CanvasGroup>(), to.x, duration);
            StartCoroutine(DisableAfterDelay(delay + duration));
        }

        private void Move()
        {
            objectToAnimate.GetComponent<RectTransform>().anchoredPosition = from;

            _tweenObject = LeanTween.move(objectToAnimate.GetComponent<RectTransform>(), to, duration);
        }

        private void Scale()
        {
            if (startPositionOffset)
            {
                objectToAnimate.GetComponent<RectTransform>().localScale = @from;
            }

            _tweenObject = LeanTween.scale(objectToAnimate, to, duration);
        }

        private void ScaleX()
        {
            throw new NotImplementedException();
        }

        private void ScaleY()
        {
            throw new NotImplementedException();
        }

        void Update()
        {

        }

        IEnumerator DisableAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            objectToAnimate.SetActive(false);
            yield break;
        }
    }
}
