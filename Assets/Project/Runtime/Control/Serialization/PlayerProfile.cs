using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum Pronouns
{
    MALE,
    FEMALE,
    NEUTRAL
}

public enum Voice
{
    MALE,
    FEMALE,
    RANDOM
}

namespace Project.Runtime.Serialization
{
    public class PlayerProfile
    {
        public int saveSlot = 0;
        public string playerName;
        public Pronouns pronounSelection;
        public Voice voiceSelection;
        public Color skinColor;
        public GenderChoice choice;
    }

}