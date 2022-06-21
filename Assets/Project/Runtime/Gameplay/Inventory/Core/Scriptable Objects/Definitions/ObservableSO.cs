using System.Collections.Generic;
using UnityEngine;

namespace Project.Runtime.Gameplay.Inventory
{
    public abstract class ObservableSO : ScriptableObject
    {
        private List<IObserver> subscribers = new List<IObserver>();

        public void Subscribe(IObserver obs)
        {
            subscribers.Add(obs);
        }

        public void Unsubscribe(IObserver obs)
        {
            subscribers.Remove(obs);
        }

        public void Notify()
        {
            foreach (IObserver subscriber in subscribers)
            {
                subscriber.Notify();
            }
        }
    }
}