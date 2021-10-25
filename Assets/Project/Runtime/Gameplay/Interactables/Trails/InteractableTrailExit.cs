using System.Collections;
using System.Collections.Generic;
using Project.Runtime.Gameplay.Interactables;
using Project.Runtime.UI.Elements;
using UnityEngine;

public class InteractableTrailExit : MonoBehaviour
{
    public InteractableTrailScoreManager trailManager;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnInteract()
    {
        if (trailManager.status >= TrailStatus.MINIMUM)
        {
            //Do exfill code
        }
        else
        {
            UIAlertUpdate.alert.AddAlertMessage(AlertType.GENERAL, "Trail not finished!");
        }
    }
}
