using UnityEngine;

namespace Project.Runtime.UI.Elements
{
    public class UIStatusManager : MonoBehaviour
    {
        public delegate void StatusChange();

        public static StatusChange OnChange;
    }
}