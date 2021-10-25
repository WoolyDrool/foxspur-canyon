using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Runtime.UI.Elements
{
    public enum AlertType
    {
        GENERAL,
        NOSPACE,
        NOBAGS,
        TRASHFULL
    }

    public class UIElementsAlertMessage : MonoBehaviour
    {
        public AlertType alertType;
        public Image iconContainer;
        public Sprite generalIcon;
        public Sprite noSpaceIcon;
        public Sprite noBagsIcon;
        public Sprite trashFullIcon;
        public TextMeshProUGUI alertText;

        void Start()
        {

        }

        void Update()
        {

        }

        public void CreateAlert(AlertType type, string message)
        {
            switch (type)
            {
                case AlertType.GENERAL:
                {
                    iconContainer.sprite = generalIcon;
                    break;
                }
                case AlertType.NOSPACE:
                {
                    iconContainer.sprite = noSpaceIcon;
                    break;
                }
                case AlertType.NOBAGS:
                {
                    iconContainer.sprite = noBagsIcon;
                    break;
                }
                case AlertType.TRASHFULL:
                {
                    iconContainer.sprite = trashFullIcon;
                    break;
                }
            }
            
            alertText.text = message;
            
        }
    }

}