using System.Collections.Generic;
using UnityEngine;

namespace Assets.DapperEvents.GameEvents.Events
{
    [CreateAssetMenu(fileName = "StringEvent", menuName = "Game Events/ StringListEvent")]
    public class StringListEvent : BaseGameEvent<List<string>>
    {
    }
}
