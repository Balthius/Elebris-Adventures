using UnityEngine;
using UnityEngine.Events; 
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Units;

namespace Assets.DapperEvents.GameEvents
{
    [CreateAssetMenu(fileName = "NewActiveHeroEvent", menuName = "Game Events/ActiveHeroEvent")]
    public class ActiveHeroEvent : BaseGameEvent <Hero> 
    {

    }
}
