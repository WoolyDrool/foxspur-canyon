using System.IO;
using UnityEngine;

namespace Project.Runtime.Serialization
{
    public static class SerializationManager
    {
        private static string directory = "/SaveData/";
        private static string fileName = "save.txt";

        public static void Save(PlayerProfile profile)
        {
            string dir = Application.persistentDataPath + directory;

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            string json = JsonUtility.ToJson(profile);
            File.WriteAllText(dir + fileName, json);
        }

        public static PlayerProfile Load()
        {
            string fullPath = Application.persistentDataPath + directory + fileName;
            PlayerProfile profile = new PlayerProfile();
            
            if (File.Exists(fullPath))
            {
                string json = File.ReadAllText(fullPath);
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
            string fullPath = Application.persistentDataPath + directory + fileName;
            if (File.Exists(fullPath))
            {
                File.Delete(fileName);
                File.Delete(profile.ToString());
            }
        }
    }

}