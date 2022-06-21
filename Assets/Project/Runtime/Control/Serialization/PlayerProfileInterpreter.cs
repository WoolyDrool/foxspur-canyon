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
        public string playerName = "Scotty";
        public Pronouns playerPronouns;
        public Voice playerVoice;
        public Color playerSkinColor;
        public string lastSceneVisited;

        [Header("Datasets")] public Material playerSkinMaterial;

        void Awake()
        {
            if (!interpreter)
            {
                interpreter = this.gameObject.GetComponent<RuntimeProfileInterpreter>();
                //playerSkinMaterial.color = new Color(178, 38, 98, 255);
               //Debug.LogWarning("No runtime interpreter found. Game running in no save mode");
                //return;
            }
            
            //string newName = ("(" + interpreter.runtimeProfile.playerName + ")" + gameObject.name);
            //gameObject.name = newName;
            gender = interpreter.runtimeProfile.choice;
            //playerSkinMaterial.color = interpreter.runtimeProfile.skinColor;
            playerSkinMaterial.color = playerSkinColor;

            

            //lastSceneVisited = interpreter.runtimeProfile.lastVisitedScene;
        }

        void Update()
        {

        }
    }

}