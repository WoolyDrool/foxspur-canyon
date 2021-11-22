using System.Collections;
using System.Collections.Generic;
using Project.Runtime.Gameplay.Interactables;
using Project.Runtime.UI.Elements;
using UnityEngine;

public class InteractableTrailExit : MonoBehaviour
{
    public InteractableTrailScoreManager trailManager;

    [Header("Payouts")] 
    public int thoroughnessPayout;
    public int secretsPayout;
    public int survivalPayout;
    public int optionalPayout;
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

    public void DetermineTotalPayout()
    {
        DetermineThoroughnessPayout();
        DetermineSecretsPayout();
        DetermineSurvivalPayout();
        DetermineOptionalPayout();
    }

    void DetermineThoroughnessPayout()
    {
        int totalTrailItems = trailManager.trailData.Items;
        int pickedUp = trailManager.itemsCollected;
        int notPickedUp = totalTrailItems - pickedUp;

        thoroughnessPayout = (pickedUp - notPickedUp) * 2;

    }

    void DetermineSecretsPayout()
    {
        int secrets = trailManager.trailData.Secrets;
        secretsPayout = secrets * 10;
    }

    void DetermineSurvivalPayout()
    {
        
    }
    
    void DetermineOptionalPayout(){}
}
