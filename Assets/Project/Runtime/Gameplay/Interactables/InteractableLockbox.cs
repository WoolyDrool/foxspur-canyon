using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableLockbox : MonoBehaviour
{
    public GameObject itemsContainer;
    private const string OPEN_TRIGGER = "Open";
    private Animator _animator;
    private Interactable _interactable;
    public BoxCollider interactableHitbox;
    private AudioSource _source;
    public AudioClip openSound;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _interactable = GetComponent<Interactable>();
        _source = GetComponent<AudioSource>();
        itemsContainer.SetActive(false);
    }

    void Update()
    {
        
    }

    public void OpenBox()
    {
        _animator.SetTrigger(OPEN_TRIGGER);
        _interactable.enabled = false;
        itemsContainer.SetActive(true);
        interactableHitbox.enabled = false;

        _source.clip = openSound;
        _source.Play();
    }
}
