using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    
    public Transform playerTransform;
    internal PlayerController _playerController;
    internal PlayerInputManager PlayerInputManager;
    internal PlayerMovement _playerMovement;
    internal CharacterController _playerCharacterController;
    
    private void Awake()
    {
        _playerController = playerTransform.GetComponent<PlayerController>();
        PlayerInputManager = playerTransform.GetComponent<PlayerInputManager>();
        _playerMovement = playerTransform.GetComponent<PlayerMovement>();
        _playerCharacterController = playerTransform.GetComponent<CharacterController>();
    }

}
