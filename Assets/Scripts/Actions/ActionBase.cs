using Assets.Scripts.Units;
using Elebris.Actions.Library.Actions.Core;
using UnityEngine;

namespace Assets.Scripts.Actions.Attacks
{
    public class ActionBase : MonoBehaviour
    {
        public CharacterAction ActionPacket { get; set; }
        public UnitActionBehaviourModel ActionInfo { get; set; }
    }
}