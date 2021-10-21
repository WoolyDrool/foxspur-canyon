using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Project.Runtime.UI.Elements;

namespace Project.Runtime.Gameplay.Player
{
    public class BaseVital
    {
        public string vitalName;

        public float minimumValue;
        public float maximumValue;
        public float currentValue;

        public List<StatModifier> modifiers = new List<StatModifier>();

        public BaseVital(string _identifier, float _minimumValue, float _maximumValue, float _startingValue)
        {
            vitalName = _identifier;
            minimumValue = _minimumValue;
            maximumValue = _maximumValue;
            SetBaseValue(_startingValue);
        }

        public void SetBaseValue(float value)
        {
            currentValue = value;
        }

        public float CheckCurrentValue(bool roundToInt)
        {
            float output;
            if (!roundToInt)
            {
                output = currentValue;
            }
            else
            {
                output = Mathf.RoundToInt(currentValue);
            }

            return output;
        }

        public void RemoveValue(float value)
        {
            if(value == 0)
                return;
            
            if (currentValue - value >= minimumValue)
            {
                Debug.Log("Received " + value.ToString());
                currentValue -= value;
            }
            else if(currentValue - value < minimumValue)
            {
                float defecit = minimumValue + currentValue;
                Debug.Log(defecit.ToString());
                currentValue -= defecit;
            }
            UIStatusUpdate.update.AddStatusMessage(UpdateType.NEGATIVESTAT,value.ToString() + " " + vitalName);
            Mathf.Clamp(currentValue, minimumValue, maximumValue);
        }

        public void AddValue(float value)
        {
            if(value == 0)
                return;
            
            if (currentValue + value <= maximumValue)
            {
                Debug.Log("Received " + value.ToString());
                currentValue += value;
            }
            else if(currentValue + value > maximumValue)
            {
                float defecit = maximumValue - currentValue;
                Debug.Log(defecit.ToString());
                currentValue += defecit;
            }
            UIStatusUpdate.update.AddStatusMessage(UpdateType.PLUSSTAT,value.ToString() + " " + vitalName);
            Mathf.Clamp(currentValue, minimumValue, maximumValue);
        }

        public float ProcessModifiers()
        {
            foreach (var s in modifiers.ToList())
            {
                s.ApplyModifier(currentValue);
                if (currentValue > minimumValue || currentValue < maximumValue)
                    currentValue = s._output;
                else
                {
                    return currentValue;
                }
                    

                Mathf.Clamp(currentValue, minimumValue, maximumValue);
                if (!s.isInfinite)
                {
                    s.currentTime -= 1 * Time.deltaTime;
                    if (s.currentTime < 0)
                    {
                        Debug.Log("Removing " + s);
                        modifiers.Remove(s);
                    }
                }
            }

            return currentValue;
        }

        public void AddModifier(StatModifier modifier)
        {
            modifiers.Add(modifier);
        }

        public void AdjustTickValue(float newAdjusted)
        {
            foreach (StatModifier s in modifiers)
            {
                s.UpdateModifierAmount(newAdjusted);
            }
        }

        public void ReturnTickValue()
        {
            foreach (StatModifier s in modifiers)
            {
                s.ReturnModifierAmount();
            }
        }

        public void RemoveAllModifiers()
        {
            modifiers.Clear();
        }

        public void RemoveModifier(StatModifier modifier)
        {
            for (int i = 0; i < modifiers.Count; i++)
            {
                if (modifiers[i].Equals(modifier))
                {
                    modifiers.Remove(modifier);
                }
            }
        }

    }

}