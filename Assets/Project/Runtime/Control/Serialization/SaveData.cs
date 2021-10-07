using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Runtime.Serialization
{
    [Serializable]
    public class SaveData : MonoBehaviour
    {
        private static SaveData _current;

        public static SaveData current
        {
            get
            {
                if (!_current)
                {
                    _current = new SaveData();
                }

                return _current;
            }
        }

        public PlayerProfile profile;

    }

}