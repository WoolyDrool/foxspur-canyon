using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    
    public Transform playerTransform;
    internal PlayerController _playerController;
    internal PlayerInput _playerInput;
    internal PlayerMovement _playerMovement;
    internal CharacterController _playerCharacterController;
    
    private void Awake()
    {
        _playerController = playerTransform.GetComponent<PlayerController>();
        _playerInput = playerTransform.GetComponent<PlayerInput>();
        _playerMovement = playerTransform.GetComponent<PlayerMovement>();
        _playerCharacterController = playerTransform.GetComponent<CharacterController>();
    }

}
