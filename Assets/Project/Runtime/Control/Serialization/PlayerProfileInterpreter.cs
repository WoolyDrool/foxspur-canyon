using System.Collections;
using System.Collections.Generic;
using Project.Runtime.Serialization;
using UnityEngine;

namespace Project.Runtime.Serialization
{
    public class PlayerProfileInterpreter : MonoBehaviour
    {
        [Header("Loaded Variables")] public RuntimeProfileInterpreter interpreter;
        public GenderChoice gender;
        public string playerName;
        public Pronouns playerPronouns;
        public Voice playerVoice;
        public Color playerSkinColor;

        [Header("Datasets")] public Material playerSkinMaterial;

        void Start()
        {
            interpreter = FindObjectOfType<RuntimeProfileInterpreter>();

            if (interpreter == null)
            {
                Debug.LogWarning("No runtime interpreter found. Game running in no save mode");
                return;
            }

            gender = interpreter.runtimeProfile.choice;
            playerSkinMaterial.color = interpreter.runtimeProfile.skinColor;
        }

        void Update()
        {

        }
    }

}