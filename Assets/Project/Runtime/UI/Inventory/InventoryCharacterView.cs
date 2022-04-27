using System.Collections;
using System.Collections.Generic;
using Project.Runtime.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCharacterView : MonoBehaviour
{
    public TextMeshProUGUI playerNameText;
    private string _loadedPlayerName;
    public RuntimeProfileInterpreter interpreter;
    public Image characterPortrait;
    public Sprite maleCharacter;
    public Sprite femaleCharacter;
    void Start()
    {
        //interpreter = FindObjectOfType<RuntimeProfileInterpreter>();

        if (interpreter != null)
        {
            _loadedPlayerName = interpreter.runtimeProfile.playerName;
            switch (interpreter.runtimeProfile.pronounSelection)
            {
                case Pronouns.MALE:
                {
                    characterPortrait.sprite = maleCharacter;
                    break;
                }
                case Pronouns.FEMALE:
                {
                    characterPortrait.sprite = femaleCharacter;
                    break;
                }
            }

            characterPortrait.color = interpreter.runtimeProfile.skinColor;
        }
        else
        {
            _loadedPlayerName = "DebugPlayer";
            characterPortrait.sprite = femaleCharacter;
            characterPortrait.color = Color.white;
        }


        playerNameText.text = _loadedPlayerName;
    }

    void Update()
    {
        
    }
}
