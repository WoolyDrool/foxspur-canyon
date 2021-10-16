using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventEnableObject : BaseEventTrigger
{
    public GameObject objectToEnable;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public override void Event()
    {
        objectToEnable.SetActive(true);
    }
}
