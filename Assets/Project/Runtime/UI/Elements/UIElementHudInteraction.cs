using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Runtime.UI.Elements
{
    public class UIElementHudInteraction : MonoBehaviour
    {
        public static UIElementHudInteraction Controller;
        public RectTransform buttonImg;
        public TextMeshProUGUI buttonText;
        public TextMeshProUGUI interactText;
        public float addRatio = 0.375f;

        RectTransform interactRect;

        float interactDefaultX;

        string code = "E";

        Vector2 defaultSize;

        private void Awake()
        {
            Controller = this;
        }

        private void Start()
        {
            interactRect = interactText.GetComponent<RectTransform>();
            interactDefaultX = interactRect.anchoredPosition.x;
            defaultSize = buttonImg.sizeDelta;

            buttonImg.gameObject.SetActive(false);
            interactText.gameObject.SetActive(false);
        }

        public void SetCode(string c)
        {
            code = c.ToUpper();
        }

        public void InteractableSelected(bool selected)
        {
            buttonImg.gameObject.SetActive(selected);
            interactText.gameObject.SetActive(selected);
        }

        public void UpdateInteract(string desc)
        {
            Vector2 size = defaultSize;
            float addToSize = (code.Length - 1) * (addRatio * defaultSize.x);
            size.x += addToSize;

            buttonImg.sizeDelta = size;
            buttonText.text = code;
            interactText.text = desc;

            interactRect.anchoredPosition = new Vector2(interactDefaultX + addToSize, interactRect.anchoredPosition.y);
        }

    }
}