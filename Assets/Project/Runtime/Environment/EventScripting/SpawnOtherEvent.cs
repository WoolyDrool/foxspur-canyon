using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOtherEvent : BaseEventTrigger
{
    public BaseEventTrigger eventToTrigger;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public override void Event()
    {
        eventToTrigger.gameObject.SetActive(true);
    }
}
