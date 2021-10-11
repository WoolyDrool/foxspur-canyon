using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Runtime.Gameplay.Interactables
{
    public class HudInteraction : MonoBehaviour
    {
        public InteractType currentInteractType;
        public static HudInteraction Controller;
        public RectTransform buttonImg;
        public Image buttonSprite;
        public TextMeshProUGUI buttonText;
        public TextMeshProUGUI interactText;
        public float addRatio = 0.375f;

        public Sprite generalInteractSprite;
        public Sprite hatchetInteractSprite;
        public Sprite shovelInteractSprite;

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
            interactText.gameObject.SetActive(selected);
            switch (currentInteractType)
            {
                case InteractType.GENERALINTERACT:
                {
                    buttonImg.gameObject.SetActive(selected);
                    buttonText.gameObject.SetActive(selected);
                    buttonSprite.sprite = generalInteractSprite;
                    break;
                }
                case InteractType.HATCHET:
                {
                    buttonImg.gameObject.SetActive(selected);
                    buttonText.gameObject.SetActive(false);
                    buttonSprite.sprite = hatchetInteractSprite;
                    break;
                }
                case InteractType.SHOVEL:
                {
                    buttonImg.gameObject.SetActive(selected);
                    buttonText.gameObject.SetActive(false);
                    buttonSprite.sprite = shovelInteractSprite;
                    break;
                }
            }
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