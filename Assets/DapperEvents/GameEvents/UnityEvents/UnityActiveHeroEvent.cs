using Assets.Scripts.Units;
using UnityEngine.Events;

namespace Assets.DapperEvents.GameEvents
{
    [System.Serializable]
    public class UnityActiveHeroEvent : UnityEvent<Hero>
    {
    }
}
