using Project.Runtime.Gameplay.Inventory;
using Project.Runtime.Gameplay.Player;
using Project.Runtime.Global;
using Project.Runtime.UI.Elements;
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
        
        [Header("UI")]
        public GameObject progressBarContainer;
        public Image progressBar;
        public Image inner;
        public Image outer;
        public Color completeColor;
        public Gradient fillGradient;
        public Color defaultCirclesColor;

        [Header("Interactions")]
        
        public AudioClip swingSound;
        public AudioClip skewerSound;
        public AudioClip reloadSound;
        public AudioClip tieSound;
        

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
            progressBarContainer.SetActive(true);
        }

        void Update()
        {
            if (_input.shooting && canUse)
            {
                _source.PlayOneShot(swingSound);
                objectAnimator.SetTrigger(PICK_TRIGGER);
                ProcessCooldown();
            }

            if (_input.reload)
            {
                if (_inventory.currentBags > 0)
                {
                    if (currentBag.currentCapacity > 0)
                    {
                        _source.PlayOneShot(tieSound);
                        progressBar.fillAmount = 0;
                        _inventory.currentBags-=1;
                        objectAnimator.SetTrigger(DROP_TRIGGER);
                        EventManager.TriggerEvent("FreeLook", null);
                    }
                }
                else
                {
                    UIAlertUpdate.alert.AddAlertMessage(AlertType.NOBAGS, "No bags!");
                }
            }
        }

        void UpdateProgressBar()
        {
            progressBar.fillAmount += 0.1f;
            progressBar.color = fillGradient.Evaluate(progressBar.fillAmount);

            if (progressBar.fillAmount == 1)
            {
                inner.color = completeColor;
                outer.color = completeColor;
            }
            else
            {
                inner.color = defaultCirclesColor;
                outer.color = defaultCirclesColor;
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
                else
                {
                    UIAlertUpdate.alert.AddAlertMessage(AlertType.GENERAL, "Bag full!");
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
                inner.color = defaultCirclesColor;
                outer.color = defaultCirclesColor;
            }
        }

        public void OnDisable()
        {
            progressBarContainer.SetActive(false);        }
    }
}
