using System;
using System.Collections;
using System.Collections.Generic;
using Project.Runtime.Gameplay.Tools;
using UnityEngine;
using Project.Runtime.Global;

namespace Project.Runtime.Gameplay.Player
{
    public class PlayerVitals : MonoBehaviour
    {
        [Header("Vitals")] public float currentHealth;
        public float currentHunger;
        public float currentHydration;
        public float currentSleep;
        public float currentToolBattery;

        public BaseVital healthStat;
        public BaseVital hungerStat;
        public BaseVital hydrationStat;
        public BaseVital sleepStat;
        public List<BaseVital> vitals = new List<BaseVital>();

        public ToolsHeadlamp headlamp;

        [Header("Tick Rates")] public bool shouldTick = true;
        public float hungerTickRate = 0.1f;
        public float hydrationTickRate = 0.1f;
        public float sleepTickRate = 0.2f;

        #region Internal Variables

        private int maxHealth = 100;
        private int maxHunger = 100;
        private int maxHydration = 100;
        private int maxSleep = 100;

        private float startingSleepTickRate;
        private float sprintSleepTickRate = 0.5f;

        #endregion

        private void Awake()
        {
            //Stat init
            healthStat = new BaseVital("Health", 0, maxHealth, 100);
            hungerStat = new BaseVital("Hunger", 0, maxHunger, 100);
            hydrationStat = new BaseVital("Hydration", 0, maxHydration, 100);
            sleepStat = new BaseVital("Sleep", 0, maxSleep, 100);

            vitals.Add(healthStat);
            vitals.Add(hungerStat);
            vitals.Add(hydrationStat);
            vitals.Add(sleepStat);

            startingSleepTickRate = sleepTickRate;
            ToggleAmbientTick();
        }

        private void SetInitials()
        {
            currentHealth = healthStat.currentValue;
            currentHunger = hungerStat.currentValue;
            currentHydration = hydrationStat.currentValue;
            currentSleep = sleepStat.currentValue;
        }

        private void SaveInitials()
        {
            healthStat.currentValue = currentHealth;
            hungerStat.currentValue = currentHunger;
            hydrationStat.currentValue = currentHydration;
            sleepStat.currentValue = currentSleep;
        }

        public void ToggleAmbientTick()
        {
            if (!shouldTick)
            {
                shouldTick = true;
                StartCoroutine(AmbientTick());
                hungerStat.AddModifier(new StatModifier(hungerStat, ModifierType.BUFF, OperationType.SUBTRACT, hungerTickRate, 0));
                hydrationStat.AddModifier(new StatModifier(hydrationStat, ModifierType.BUFF, OperationType.SUBTRACT, hydrationTickRate, 0));
                sleepStat.AddModifier(new StatModifier(sleepStat, ModifierType.BUFF, OperationType.SUBTRACT, sleepTickRate, 0));
            }
            else
            {
                shouldTick = false;
                StopCoroutine(AmbientTick());
                foreach (BaseVital b in vitals)
                {
                    b.RemoveAllModifiers();
                }
            }
        }

        IEnumerator AmbientTick()
        {
            while (shouldTick)
            {
                currentHunger = hungerStat.currentValue;
                currentHydration = hydrationStat.currentValue;
                currentSleep = sleepStat.currentValue;

                yield return new WaitForSeconds(0.2f);
                foreach (BaseVital b in vitals)
                {
                    b.ProcessModifiers();
                }
                yield return null;
            }
        }

        void Update()
        {
            currentHealth = healthStat.currentValue;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                sleepStat.currentValue -= sprintSleepTickRate * Time.deltaTime;
            }
            else
            {
                sleepStat.currentValue = sleepStat.currentValue;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                sleepStat.currentValue -= 1;
            }
            
        }

        public void ModifyHunger(float positive, float negative)
        {
            hungerStat.AddValue(positive);
            hungerStat.RemoveValue(negative);
        }

        public void ModifyHydration(float positive, float negative)
        {
            hydrationStat.AddValue(positive);
            hydrationStat.RemoveValue(negative);
        }

        public void ModifySleep(float positive, float negative)
        {
            sleepStat.AddValue(positive);
            sleepStat.RemoveValue(negative);
        }

        public void ModifyHealth(float positive, float negative)
        {
            float positiveDefecit = currentHealth - positive;
            healthStat.currentValue += positive;

            healthStat.currentValue -= negative;
            Mathf.Clamp(currentHealth, 0, maxHealth);
        }

    }

}