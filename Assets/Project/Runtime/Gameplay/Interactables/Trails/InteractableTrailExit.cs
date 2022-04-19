using System.Collections;
using System.Collections.Generic;
using Project.Runtime.Gameplay.Interactables;
using Project.Runtime.Global;
using Project.Runtime.UI.Elements;
using UnityEngine;

public class InteractableTrailExit : MonoBehaviour
{
    public RuntimeTrailManager trailManager;

    [Header("Payouts")] 
    public int thoroughnessPayout;
    public int secretsPayout;
    public int survivalPayout;
    public int optionalPayout;
    public string sceneToReturnTo;
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
            DetermineTotalPayout();
            SceneLoadingManager.loader.LoadScene(sceneToReturnTo);
        }
        else
        {
            UIAlertUpdate.alert.AddAlertMessage(AlertType.GENERAL, "Trail not finished!");
        }
    }

    void DetermineTotalPayout()
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
        //Will add code later...
    }

    void DetermineOptionalPayout()
    {
        //Will add code later
    }
}
