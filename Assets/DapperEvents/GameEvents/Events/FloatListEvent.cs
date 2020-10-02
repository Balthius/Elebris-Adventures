using System.Collections.Generic;
using UnityEngine;

namespace Assets.DapperEvents.GameEvents.Events
{
    [CreateAssetMenu(fileName = "FloatEvent", menuName = "Game Events/ FloatListEvent")]
    public class FloatListEvent : BaseGameEvent<List<float>>
    {
    }
}
