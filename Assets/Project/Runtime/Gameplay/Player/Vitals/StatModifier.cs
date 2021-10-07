using System;
using System.Collections;
using System.Collections.Generic;
using Project.Runtime.Gameplay.Player;
using UnityEngine;

public enum ModifierType
{
    BUFF,
    DEBUFF
}

public enum OperationType
{
    ADD,
    SUBTRACT,
    MULTIPLY,
    FREEZE
}
public class StatModifier
{
    private ModifierType _buffType;
    private OperationType _operation;
    private BaseVital _vital;
    private float _currentValueBeingModified;
    public float _output;
    private float _modifier;
    public float _lifetime;
    public float currentTime;
    public bool isInfinite = false;
    public float oldModifier;

    public StatModifier(BaseVital vitalToModify, ModifierType modType, OperationType opType, float modifier, float duration)
    {
        _vital = vitalToModify;
        _buffType = modType;
        _operation = opType;
        _modifier = modifier;
        oldModifier = _modifier;
        _lifetime = duration;
        currentTime = _lifetime;
        if (duration == 0)
            isInfinite = true;
        return;
    }
    
    public void ApplyModifier(float value)
    {
        switch (_buffType)
        {
            case ModifierType.BUFF:
            {
                Buff(value);
                break;
            }
            case ModifierType.DEBUFF:
            {
                Debuff(value);
                break;
            }
        }
    }

    public void UpdateModifierAmount(float value)
    {
        oldModifier = _modifier;
        _modifier = value;
    }

    public void ReturnModifierAmount()
    {
        _modifier = oldModifier;
    }

    float Buff(float value)
    {
        switch (_operation)
        {
            case OperationType.ADD:
            {
                if (_vital.currentValue + _modifier <= _vital.maximumValue)
                {
                    value += _modifier; 
                }

                break;
            }
            case OperationType.SUBTRACT:
            {
                if (_vital.currentValue - _modifier >= _vital.minimumValue)
                {
                    value -= _modifier;
                }
                break;
            }
            case OperationType.MULTIPLY:
            {
                value *= _modifier;
                break;
            }
            case OperationType.FREEZE:
            {
                value = value;
                break;
            }
        }

        _output = value;
        return _output;
    }

    float Debuff(float value)
    {
        switch (_operation)
        {
            case OperationType.ADD:
            {
                break;
            }
            case OperationType.SUBTRACT:
            {
                break;
            }
            case OperationType.MULTIPLY:
            {
                break;
            }
            case OperationType.FREEZE:
            {
                break;
            }
        }

        _output = value;
        return _output;
    }
}
