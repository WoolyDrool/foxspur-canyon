using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Runtime.Serialization;

namespace Project.Runtime.UI.Buttons
{
    public class UIButtonCreateSave : MonoBehaviour
    {
        public int saveSlot = 0;
        private string _newSaveName;
        private Pronouns _newSavePronouns;
        private Voice _newSaveVoice;
        private Color _newSaveSkinColor;
        private RuntimeProfileInterpreter runtimeProfile;
        public GenderChoice maleVoice;
        public GenderChoice femaleVoice;
        void Start()
        {
            runtimeProfile = FindObjectOfType<RuntimeProfileInterpreter>();
        }

        void Update()
        {

        }

        public void SetName(string name)
        {
            _newSaveName = name;
        }

        public void SetPronouns(int value)
        {
            switch (value)
            {
                case 0:
                {
                    _newSavePronouns = Pronouns.MALE;
                    break;
                }
                case 1:
                {
                    _newSavePronouns = Pronouns.FEMALE;
                    break;
                }
                case 2:
                {
                    _newSavePronouns = Pronouns.NEUTRAL;
                    break;
                }
            }
        }

        public void SetVoice(int value)
        {
            switch (value)
            {
                case 0:
                {
                    _newSaveVoice = Voice.MALE;
                    
                    break;
                }
                case 1:
                {
                    _newSaveVoice = Voice.FEMALE;
                    break;
                }
                case 2:
                {
                    _newSaveVoice = Voice.RANDOM;
                    break;
                }
            }
            
        }

        public void SetSkinColor(Color choice)
        {
            _newSaveSkinColor = choice;
        }

        public void CreateSave()
        {
            Debug.Log(_newSaveName + _newSavePronouns.ToString() + _newSaveVoice.ToString() + _newSaveSkinColor.ToString());
            PlayerProfile profile = new PlayerProfile();
            profile.saveSlot = saveSlot;
            profile.playerName = _newSaveName;
            profile.skinColor = _newSaveSkinColor;
            profile.pronounSelection = _newSavePronouns;
            profile.voiceSelection = _newSaveVoice;

            if (_newSaveVoice == Voice.MALE)
                profile.choice = maleVoice;
            else if (_newSaveVoice == Voice.FEMALE)
                profile.choice = femaleVoice;
            else if (_newSaveVoice == Voice.RANDOM)
            {
                int rand = UnityEngine.Random.Range(1, 2);
                if (rand == 2)
                {
                    profile.choice = maleVoice;
                }
                else
                {
                    profile.choice = femaleVoice;
                }
                
            }
            SerializationManager.Save(profile);
            runtimeProfile.InjectProfile(profile);
        }
    }

}