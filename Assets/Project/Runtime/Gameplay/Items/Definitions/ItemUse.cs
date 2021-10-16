using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Runtime.Gameplay.Player;

[CreateAssetMenu(menuName = "Items/New Item Use")]
public class ItemUse : ScriptableObject
{
    public ItemType type;
    public enum ItemType {HUNGER, HYDRATION, EXHAUSTION, HEALTH}

    public float positiveAmount;
    public float negativeAmount;

    public bool isAlsoBuff;
    public ModifierType buffType;
    public OperationType operationType;
    public float buffDuration;
    public float buffValue;

    public PlayerVitals vitals;
    void Start()
    {
    }

    void Update()
    {
        
    }

    private void OnDisable()
    {
        vitals = null;
    }

    public void OnUse()
    {
        switch (type)
        {
            case ItemType.HUNGER:
            {
                vitals.ModifyHunger(positiveAmount, negativeAmount);
                if (isAlsoBuff)
                {
                    vitals.hungerStat.AddModifier(new StatModifier(vitals.hungerStat, buffType, operationType, buffValue, buffDuration));
                }
                break;
            }
            
            case ItemType.HYDRATION:
            {
                vitals.ModifyHydration(positiveAmount, negativeAmount);
                if (isAlsoBuff)
                {
                    Debug.Log("applying buff");
                    vitals.hydrationStat.AddModifier(new StatModifier(vitals.hydrationStat,buffType, operationType, buffValue, buffDuration));
                }
                break;
            }
            
            case ItemType.EXHAUSTION:
            {
                vitals.ModifySleep(positiveAmount, negativeAmount);
                if (isAlsoBuff)
                {
                    Debug.Log("applying buff");
                    vitals.sleepStat.AddModifier(new StatModifier(vitals.sleepStat, buffType, operationType, buffValue, buffDuration));
                }
                break;
            }
            
            case ItemType.HEALTH:
            {
                vitals.ModifyHealth(positiveAmount, negativeAmount);
                if (isAlsoBuff)
                {
                    Debug.Log("applying buff");
                    vitals.healthStat.AddModifier(new StatModifier(vitals.healthStat, buffType, operationType, buffValue, buffDuration));
                }
                break;
            }
        }
    }
}
