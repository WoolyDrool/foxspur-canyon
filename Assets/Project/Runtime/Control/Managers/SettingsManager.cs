using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Runtime
{
    public class SettingsManager : MonoBehaviour
    {
        public float playerPrefMouseSensitivity = 2;
        public float playerPrefGamepadSensitivityX = 2;
        public float playerPrefGamepadSensitivityY = 2;
        public float playerPrefFOV = 90;

        public float playerPrefMasterVolume;

        public float playerPrefMusicVolume;

        public float playerPrefSFXVolume;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void GetPlayerSettingPreferences()
        {
            playerPrefMouseSensitivity = PlayerPrefs.GetFloat("PREF_SENSITIVITY", playerPrefMouseSensitivity);
            playerPrefGamepadSensitivityX= PlayerPrefs.GetFloat("PREF_SENSITIVITY_X", playerPrefGamepadSensitivityX);
            playerPrefGamepadSensitivityY= PlayerPrefs.GetFloat("PREF_SENSITIVITY_Y", playerPrefGamepadSensitivityY);
            playerPrefFOV = PlayerPrefs.GetFloat("PREF_FOV", playerPrefFOV);
            playerPrefMasterVolume = PlayerPrefs.GetFloat("PREF_MASTERVOLUME", playerPrefMasterVolume);
            playerPrefMusicVolume = PlayerPrefs.GetFloat("PREF_MUSICVOLUME", playerPrefMusicVolume);
            playerPrefSFXVolume = PlayerPrefs.GetFloat("PREF_SFXVOLUME", playerPrefSFXVolume);
        }

        public void SavePlayerSettingPreferences()
        {
            PlayerPrefs.SetFloat("PREF_SENSITIVITY", playerPrefMouseSensitivity);
            PlayerPrefs.SetFloat("PREF_SENSITIVITY_X", playerPrefGamepadSensitivityX);
            PlayerPrefs.SetFloat("PREF_SENSITIVITY_Y", playerPrefGamepadSensitivityY);
            PlayerPrefs.SetFloat("PREF_FOV", playerPrefFOV);
            PlayerPrefs.SetFloat("PREF_MASTERVOLUME", playerPrefMasterVolume);
            PlayerPrefs.SetFloat("PREF_MUSICVOLUME", playerPrefMusicVolume);
            PlayerPrefs.SetFloat("PREF_SFXVOLUME", playerPrefSFXVolume);
            
            GameManager.instance.inputManager.Init();
        }
    }
}
