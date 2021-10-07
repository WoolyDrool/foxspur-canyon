using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Runtime.UI.Elements
{
    public class UIElementsCompass : MonoBehaviour
    {
        public Transform playerTransform;
        public RectTransform compass;
        private Vector3 dir;

        
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            dir.z = playerTransform.eulerAngles.y;
            compass.localEulerAngles = dir;
        }

        public void ChangeDirectionLayer()
        {

        }
    }
}
