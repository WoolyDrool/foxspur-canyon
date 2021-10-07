using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Runtime.AI
{
    
    public class RuntimeAIAudioManager : MonoBehaviour
    {
        private RuntimeAIManager _manager;

        public RuntimeAISoundCue soundSpawner;
        
        private void Awake()
        {
            _manager = GetComponent<RuntimeAIManager>();
        }

        public void SpawnAudioNearPlayer(float minDistance, float maxDistance)
        {
            float distance = Random.Range(minDistance, maxDistance);
            float angle = Random.Range(-Mathf.PI, Mathf.PI);
            Vector3 knownPosition = _manager._playerTracker.currentPlayerPosition;

            knownPosition += new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * distance;
            //knownPosition.x = Mathf.Clamp(knownPosition.x, minDistance, maxDistance);
            knownPosition.y = knownPosition.y;
           // knownPosition.z = Mathf.Clamp(knownPosition.z, minDistance, maxDistance);

            GameObject clone;

            clone = Instantiate(soundSpawner.gameObject, knownPosition, transform.rotation);
        }
    }
}