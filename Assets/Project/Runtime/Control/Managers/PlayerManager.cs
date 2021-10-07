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

    public string currentLocation = "World";

    public delegate void ChangeCurrentLocation();

    public static event ChangeCurrentLocation OnLocationChanged;
    
    private void Awake()
    {
        _playerController = playerTransform.GetComponent<PlayerController>();
        _playerInput = playerTransform.GetComponent<PlayerInput>();
        _playerMovement = playerTransform.GetComponent<PlayerMovement>();
        _playerCharacterController = playerTransform.GetComponent<CharacterController>();
    }

    public void ChangeLocation(string newLocation)
    {
        currentLocation = newLocation;

        if (OnLocationChanged != null)
            OnLocationChanged();
    }
}
