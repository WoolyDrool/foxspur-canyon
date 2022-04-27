using System;
using System.IO;
using UnityEngine;

namespace Project.Runtime.Serialization
{
    public static class SerializationManager
    {
        private static string directory = "/SaveData/";
        private static string fileName = "_save.txt";

        public static void Save(PlayerProfile profile)
        {
            string dir = Application.persistentDataPath + directory;

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            string json = JsonUtility.ToJson(profile);
            File.WriteAllText(dir + profile.playerName+fileName, json);
        }

        public static PlayerProfile Load()
        {
            string fullPath = Application.persistentDataPath + directory;
            PlayerProfile profile = new PlayerProfile();
            string compFullPath = fullPath + (profile.playerName + fileName);
            
            if (File.Exists(compFullPath))
            {
                string json = File.ReadAllText(compFullPath);
                profile = JsonUtility.FromJson<PlayerProfile>(json);
            }
            else
            {
                Debug.Log("Save does not exist");
            }

            return profile;
        }

        public static void Delete(PlayerProfile profile)
        {
            string fullPath = Application.persistentDataPath + directory + profile.playerName+fileName;
            if (File.Exists(fullPath))
            {
                File.Delete(fileName);
                File.Delete(profile.ToString());
            }
        }

        public static bool TryGetProfile()
        {
            PlayerProfile attemptProfile = Load();
            string fullPath = Application.persistentDataPath + directory + attemptProfile.playerName+fileName;
            if (File.Exists(fullPath))
            {
                return true;
            }

            return false;
        }
    }

}