using UnityEngine;

namespace Project.Runtime.Gameplay.Interactables
{
    [CreateAssetMenu(menuName = "Trails/Create TrailData")]
    public class TrailData : ScriptableObject
    {
        public string TrailName;
        public int Items;
        public int Secrets;
        public float minimumCompletionPercentage;
        public bool Completed;
    }
}