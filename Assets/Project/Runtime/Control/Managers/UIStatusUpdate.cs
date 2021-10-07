using System;
using UnityEngine;

namespace Project.Runtime.UI.Elements
{
    public class UIStatusUpdate : MonoBehaviour
    {
        public UIElementStatusMessage statusMessagePrefab;

        public static UIStatusUpdate update;

        public void Awake()
        {
            update = this;
        }

        public void AddStatusMessage(UpdateType type, string message)
        {
            GameObject clone;

            clone = Instantiate(statusMessagePrefab.gameObject, transform.position, transform.rotation);
            clone.transform.SetParent(transform, false);
            clone.GetComponent<UIElementStatusMessage>().CreateStatusMessage(type, message);
        }
    }
}