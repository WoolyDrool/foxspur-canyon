using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Runtime.UI.Elements
{
    public class UIAlertUpdate : MonoBehaviour
    {
        public UIElementsAlertMessage alertMessagePrefab;
        public static UIAlertUpdate alert;
        
        void Awake()
        {
            alert = this;
        }

        public void AddAlertMessage(AlertType type, string message)
        {
            if (transform.childCount < 1)
            {
                GameObject clone;

                clone = Instantiate(alertMessagePrefab.gameObject, transform.position, transform.rotation);
                clone.transform.SetParent(transform, false);
                clone.GetComponent<UIElementsAlertMessage>().CreateAlert(type, message);
                Destroy(clone, 3f);
            }
            else
            {
                Debug.LogError("An alert message is already active!");
            }
            
        }
    }

}