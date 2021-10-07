using Project.Runtime.Gameplay.Inventory;
using Project.Runtime.Gameplay.Tools;
using Project.Runtime.Global;
using UnityEngine;

namespace Project.Runtime.Gameplay.Interactables
{
    public class InteractableTrashDeposit : MonoBehaviour
    {
        [Header("Container Settings")]
        public bool isInfinite = false;
        public int itemsThatCanBeDeposited;
        public int maxItemDepositSize;
        public int currentItems;
        
        [Header("Interaction Settings")]
        public string interactableMessage;
        public AudioClip largeDepositSound;
        public AudioClip smallDepositSound;

        #region Internal Variables
        private PlayerInventory playerInventory;
        private Interactable _interactable;
        private InteractableTrailScoreManager _scoreManager;
        private AudioSource _source;
        private Coroutine depositRoutine;
        #endregion
        
        void Start()
        {
            //Get components
            playerInventory = GameManager.instance.playerInventory;
            _interactable = GetComponent<Interactable>();
            _scoreManager = FindObjectOfType<InteractableTrailScoreManager>();
            _source = GetComponent<AudioSource>();
            
            //Reset current items
            currentItems = 0;
            UpdateInteract();
        }
        
        void UpdateInteract()
        {
            if (isInfinite)
            {
                _interactable.description = interactableMessage + playerInventory.trashInInventory.ToString() + ")";
            }
            else
            {
                _interactable.description = interactableMessage + /*playerInventory.smallTrashinInventory.ToString()*/ currentItems.ToString() + "/10)";
            }
        }

        public void Update()
        {
            if (isInfinite)
            {
                _interactable.description = interactableMessage + playerInventory.trashInInventory.ToString() + ")";
            }
        }

        public void DetermineDeposit()
        {
            if (playerInventory.trashInInventory > 0)
            {
                if (isInfinite)
                {
                    FullDeposit();
                }
                else
                {
                    DepositSome();
                }
                UpdateInteract();
            }
        }
        
        public void DepositSome()
        {
            for (int i = 0; i < playerInventory.trashItems.Count; i++)
            {
                Item itemToRemove = playerInventory.trashItems[i];
                int itemSize = itemToRemove.size.x + itemToRemove.size.y;
                if (itemSize <= maxItemDepositSize)
                {
                    if (currentItems < itemsThatCanBeDeposited)
                    {
                        PlaySound(1);
                        currentItems++;
                        playerInventory.RemoveItem(itemToRemove);
                        _scoreManager.AddScore(1);
                        return;
                    }
                    
                    EventManager.TriggerEvent("cantAddItemToTrash", null);
                }
                Debug.Log("Cant deposit item, item is too big!");
            }

            _source.clip = null;
        }

        private void FullDeposit()
        {
            PlaySound(playerInventory.trashInInventory);
            
            _scoreManager.AddScore(playerInventory.trashItems.Count);
            playerInventory.ClearAllTrash();
            
            _source.clip = null;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out InteractableThrowableItem throwableItem))
            {
                DepositThrownItem(throwableItem);
            }
        }
        
        private void DepositThrownItem(InteractableThrowableItem throwableItem)
        {
            if (!throwableItem.isBeingHeld && !throwableItem.hasBeenDeposited)
            {
                switch (isInfinite)
                {
                    case true:
                    {
                        throwableItem.hasBeenDeposited = true;
                        _scoreManager.AddScore(throwableItem.amount);
                        Destroy(throwableItem.gameObject, 15f);
                        PlaySound(throwableItem.amount);
                        break;
                    }
                    case false:
                    {
                        if (currentItems != itemsThatCanBeDeposited)
                        {
                            currentItems += throwableItem.amount;
                            _scoreManager.AddScore(throwableItem.amount);
                            Destroy(throwableItem.gameObject);
                            PlaySound(throwableItem.amount);
                        }
                        else
                        {
                            EventManager.TriggerEvent("cantAddItemToTrash", null);
                        }
                        break;
                    }
                }
                
                UpdateInteract();
            }
        }
        
        private void PlaySound(int depositSize)
        {
            if(depositSize < 6)
            {
                _source.PlayOneShot(smallDepositSound);
            }
            else
            {
                _source.PlayOneShot(largeDepositSound);
            }
        }
        
    }

}