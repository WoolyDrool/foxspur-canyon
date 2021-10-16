using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventEnableScript : BaseEventTrigger
{
    public MonoBehaviour scriptToEnable;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public override void Event()
    {
        scriptToEnable.enabled = true;
    }
}
