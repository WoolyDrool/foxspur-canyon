using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum InteractType
{
    GENERALINTERACT,
    HATCHET,
    SHOVEL,
}

public class Interactable : MonoBehaviour
{
    public InteractType interact;
    
    [Range(1f, 10f)]
    public float interactRange = 3f;
    public string description;
    public UnityEvent onInteract;
    public virtual void Start()
    {
        
    }

    public virtual void Interact()
    {
        onInteract.Invoke();
    }
}