using System.Collections;
using System.Collections.Generic;
using Project.Runtime.Gameplay.Inventory;
using Project.Runtime.UI.Elements;

using UnityEngine;

public class InteractableCaveDoor : MonoBehaviour
{
    public Transform start;
    public Transform destination;
    public bool locked;
    public Item key;

    public WorldLightingPreset cavePreset;
    private WorldLightingPreset defaultPreset;
    private WorldLightingManager _lightingManager;

    public bool reverseOrder = false;

    [SerializeField] private Transform _playerTransform;
    private PlayerInventory _inventory;
    void Start()
    {
        _lightingManager = GameManager.instance.lightingManager;
        defaultPreset = _lightingManager.preset;
        _inventory = GameManager.instance.playerInventory;
        _playerTransform = GameManager.instance.playerManager.playerTransform;
    }

    void Update()
    {
        
    }
    
    public void InteractWithDoor()
    {
        if (!locked)
        {
            
            Teleport();
        }
        else
        {
            if (_inventory.inventoryItems.Contains(key))
            {
                Teleport();
            }
            else
            {
                UIStatusUpdate.update.AddStatusMessage(UpdateType.GENERALUPDATE, "Need " + key.itemName);
            }
        }
            
    }

    void Teleport()
    {
        _playerTransform.GetComponent<PlayerMovement>().enabled = false;
        _playerTransform.GetComponent<InterpolatedTransformUpdater>().enabled = false;
        _playerTransform.SetPositionAndRotation(destination.localPosition, destination.rotation);
        _lightingManager.preset = cavePreset;
        
        /*if (!reverseOrder)
        {
            
        }
        else
        {
            _lightingManager.preset = defaultPreset;
        }*/
    }
}
