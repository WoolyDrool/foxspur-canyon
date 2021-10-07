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
    private RuntimeProfileInterpreter _interpreter;
    public Image characterPortrait;
    public Sprite maleCharacter;
    public Sprite femaleCharacter;
    void Start()
    {
        _interpreter = FindObjectOfType<RuntimeProfileInterpreter>();

        if (_interpreter != null)
        {
            _loadedPlayerName = _interpreter.runtimeProfile.playerName;
            switch (_interpreter.runtimeProfile.pronounSelection)
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

            characterPortrait.color = _interpreter.runtimeProfile.skinColor;
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
