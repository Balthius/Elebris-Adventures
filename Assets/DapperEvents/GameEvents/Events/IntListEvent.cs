using System.Collections.Generic;
using UnityEngine;

namespace Assets.DapperEvents.GameEvents.Events
{
    [CreateAssetMenu(fileName = "IntEvent", menuName = "Game Events/ IntListEvent")]
    public class IntListEvent : BaseGameEvent<List<int>>
    {
    }
}
