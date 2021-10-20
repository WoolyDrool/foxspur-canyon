using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Project.Runtime.UI;

namespace Project.Runtime.UI.Elements
{
    public enum UpdateType
    {
        ITEMADD,
        ITEMREMOVE,
        PLUSSTAT,
        NEGATIVESTAT,
        GENERALUPDATE,
        SECRET,
        ITEMDROP
    }
    public class UIElementStatusMessage : MonoBehaviour
    {
        public UpdateType statusType;
        public Sprite plusIcon;
        public Sprite negativeIcon;
        public Sprite generalIcon;
        public Sprite secretsIcon;
        public Sprite dropIcon;
        public TextMeshProUGUI statusText;
        public Image iconContainer;

        public void OnEnable()
        {
            
        }

        public void CreateStatusMessage(UpdateType type, string message)
        {
            switch (type)
            {
                case UpdateType.ITEMADD:
                {
                    iconContainer.sprite = plusIcon;
                    break;
                }
                case UpdateType.ITEMREMOVE:
                {
                    iconContainer.sprite = negativeIcon;
                    break;
                }
                case UpdateType.PLUSSTAT:
                {
                    iconContainer.sprite = plusIcon;
                    break;
                }
                case UpdateType.NEGATIVESTAT:
                {
                    iconContainer.sprite = negativeIcon;
                    break;
                }
                case UpdateType.GENERALUPDATE:
                {
                    iconContainer.sprite = generalIcon;
                    break;
                }
                case UpdateType.SECRET:
                {
                    iconContainer.sprite = secretsIcon;
                    break;
                }
                case UpdateType.ITEMDROP:
                {
                    iconContainer.sprite = dropIcon;
                    break;
                }
            }

            statusText.text = message;
            Destroy(gameObject, 5f);
        }
    }
}