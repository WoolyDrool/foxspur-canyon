using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace Project.Runtime.UI.Buttons
{
    public class UIClickNotifier : MonoBehaviour, IPointerClickHandler
    {
        public UnityEvent onLeft;
        public UnityEvent onRight;
        public UnityEvent onMiddle;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                onLeft.Invoke();
            }
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                onRight.Invoke();
            }
            if (eventData.button == PointerEventData.InputButton.Middle)
            {
                onMiddle.Invoke();
            }
        }
    }
}