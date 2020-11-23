using Assets.Scripts.Units;
using UnityEngine;

namespace Assets.DapperEvents.GameEvents
{
    [CreateAssetMenu(fileName = "NewActiveHeroEvent", menuName = "Game Events/ActiveHeroEvent")]
    public class ActiveHeroEvent : BaseGameEvent<Hero>
    {

    }
}
