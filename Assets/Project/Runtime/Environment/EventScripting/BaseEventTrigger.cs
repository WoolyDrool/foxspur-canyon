using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEventTrigger : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Event();
        }
    }

    public virtual void Event()
    {
        
    }
}
