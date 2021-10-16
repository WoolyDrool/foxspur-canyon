using Project.Runtime.Gameplay.Inventory;
using Project.Runtime.Gameplay.Player;
using Project.Runtime.Global;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Runtime.Gameplay.Tools
{
    public class ToolTrashPicker : EquippableObject
    {
        [Header("Pick Controls")]
        public float pickRange = 5;
        public LayerMask pickLayers;
        
        [Header("Bag Controls")]
        public GameObject emptyBag;
        public GameObject fullBag;
        public TrashPickerBag currentBag;
        
        [Header("Interactions")]
        public Image progressBar;
        public AudioClip swingSound;
        public AudioClip skewerSound;
        public AudioClip reloadSound;

        #region Internal Variables
        private const string PICK_TRIGGER = "Pick";
        private const string DROP_TRIGGER = "DropBag";
        
        private Transform _playerCamera;
        private RaycastHit _rayHit;
        private AudioSource _source;
        private PlayerInventory _inventory;
        #endregion
        
        void Start()
        {
            _playerCamera = FindObjectOfType<CameraMovement>().transform;
            _inventory = FindObjectOfType<PlayerInventory>();
            _source = GetComponent<AudioSource>();
            currentBag = GetComponent<TrashPickerBag>();
        }
        
        private void OnEnable()
        {
            progressBar.gameObject.SetActive(true);
        }

        void Update()
        {
            if (_input.shooting)
            {
                _source.PlayOneShot(swingSound);
                objectAnimator.SetTrigger(PICK_TRIGGER);
            }

            if (_input.reload)
            {
                if (_inventory.currentBags > 0)
                {
                    if (currentBag.currentCapacity > 0)
                    {
                        progressBar.fillAmount = 0;
                        _inventory.currentBags-=1;
                        objectAnimator.SetTrigger(DROP_TRIGGER);
                        EventManager.TriggerEvent("FreeLook", null);
                    }
                }
                else
                {
                    EventManager.TriggerEvent("noBags", null);
                }
            }
        }

        void UpdateProgressBar()
        {
            progressBar.fillAmount += 0.1f;
            
            if (progressBar.fillAmount == 1)
            {
                progressBar.color = Color.green;
            }
            else
            {
                progressBar.color = Color.white;
            }
        }
        
        public void PickTrash()
        {
            if (Physics.Raycast(_playerCamera.transform.position, _playerCamera.transform.TransformDirection(Vector3.forward), out _rayHit, pickRange, pickLayers))
            {
                HandlePick();
            }
            
            objectAnimator.SetTrigger(PICK_TRIGGER);
        }

        private void HandlePick()
        {
            if (_rayHit.collider.GetComponent<ItemPickup>().itemType == PickupType.TRASH)
            {
                ItemPickup component = _rayHit.collider.GetComponent<ItemPickup>();
                if (currentBag.canAddItem())
                {
                    //Disable the picked items visuals and destroy after delay
                    component.gameObject.SetActive(false);
                    component.hitboxCollider.enabled = false;
                    Destroy(component.gameObject, 2f);
                    
                    currentBag.AddItem();
                    
                    _source.PlayOneShot(skewerSound);
                    _source.PlayOneShot(component.pickupSound);
                    
                    if (currentBag.currentCapacity >= 10)
                    {
                        emptyBag.SetActive(false);
                        fullBag.SetActive(true);
                    }
                    
                    UpdateProgressBar();
                }
                component = null;
            }
            else
            {
                return;
            }
        }

        public void DropBag()
        {
            if (currentBag.currentCapacity > 0)
            {
                objectAnimator.SetTrigger(DROP_TRIGGER);
                EventManager.TriggerEvent("FreeLook", null);
                currentBag.DropBag();
                _source.PlayOneShot(reloadSound);
            }
        }

        public void OnDisable()
        {
            progressBar.gameObject.SetActive(false);
        }
    }
}
