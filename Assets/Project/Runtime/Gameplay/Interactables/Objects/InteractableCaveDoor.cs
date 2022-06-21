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

    public GameObject fadein;
    public GameObject fadeout;

    public WorldLightingPreset cavePreset;
    private WorldLightingPreset defaultPreset;
    private WorldLightingManager _lightingManager;
    private AudioSource _source;
    public AudioClip doorSound;
    public AudioClip lockedSound;

    public bool reverseOrder = false;

    [SerializeField] private Transform _playerTransform;
    private PlayerInventory _inventory;
    void Start()
    {
        _playerTransform = GameManager.instance.playerManager.playerTransform;
        _lightingManager = GameManager.instance.lightingManager;
        defaultPreset = _lightingManager.preset;
        _inventory = GameManager.instance.playerInventory;
        _source = GetComponent<AudioSource>();
       
    }

    void Update()
    {
        if (!defaultPreset)
        {
            defaultPreset = _lightingManager.preset;
        }
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
                _source.PlayOneShot(lockedSound);
                UIStatusUpdate.update.AddStatusMessage(UpdateType.GENERALUPDATE, "Need " + key.itemName);
            }
        }
            
    }

    void Teleport()
    {
        _source.PlayOneShot(doorSound);
        StartCoroutine(Transition());
        
        IEnumerator Transition()
        {
            //if (!fadein.activeSelf)
            //{
            //   fadein.SetActive(true);
            //}

            //yield return new WaitForSeconds(3);
            
            _playerTransform.GetComponent<PlayerMovement>().ForceNewPosition(destination.position);
            _playerTransform.SetPositionAndRotation(destination.position, destination.rotation);
            
            //fadein.SetActive(false);
            //fadeout.SetActive(true);

            if (!reverseOrder)
            {
                _lightingManager.preset = cavePreset;
            }
            else
            {
                _lightingManager.preset = defaultPreset;
            }
            
            yield break;
        }
    }
}
