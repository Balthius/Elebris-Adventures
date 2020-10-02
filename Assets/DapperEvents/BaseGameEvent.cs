using Assets.DapperEvents.GameEvents.Listeners;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.DapperEvents
{
    public abstract class BaseGameEvent<T> : ScriptableObject
    {
        //readonly can be affected in the constructor, not another method
        private readonly List<IGameEventListener<T>> eventListeners = new List<IGameEventListener<T>>();

        public void Raise(T item)
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
            {
                eventListeners[i].OnEventRaised(item);
            }
        }

        public void RegisterListener(IGameEventListener<T> listener)
        {
            if (!eventListeners.Contains(listener))
            {
                eventListeners.Add(listener);
            }
        }
        public void UnregisterListener(IGameEventListener<T> listener)
        {

            if (eventListeners.Contains(listener))
            {
                eventListeners.Remove(listener);
            }
        }
    }
}
