using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableNoUse : Interactable
{
    public override void Start()
    {
        base.Start();
    }

    void Update()
    {
        
    }

    public override void Interact()
    {
        base.Interact();
    }
}
