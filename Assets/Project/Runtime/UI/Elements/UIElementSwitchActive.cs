using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Runtime.UI.Elements
{
    public class UIElementSwitchActive : MonoBehaviour
    {
        public float waitTime = 5;
        public GameObject nextUI;

        void Start()
        {
            if (nextUI.activeSelf)
                nextUI.SetActive(false);


            StartCoroutine(Wait());
        }

        void Update()
        {

        }

        IEnumerator Wait()
        {
            yield return new WaitForSeconds(waitTime);
            gameObject.SetActive(false);
            nextUI.SetActive(true);
        }
    }
}
