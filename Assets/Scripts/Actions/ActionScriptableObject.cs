using Assets.Scripts.Actions.Attacks;
using Elebris.Actions.Library.Actions.Component;
using Elebris.Actions.Library.Actions.Core;
using Elebris.Core.Library.CharacterValues.Mutable;
using System;
using UnityEngine;

namespace Assets.Scripts.Units
{
    [CreateAssetMenu(menuName = "Actions/New Basic Action")]
    [Serializable]
    public class ActionScriptableObject : ScriptableObject
    {
        public UnitActionBehaviourModel model;

        [SerializeField]ActionComponent[] components;
        public virtual void GenerateComponents(IActionComponent action, Unit owner)
        {
            action.ActionInfo = model;
            foreach (var item in components)
            {
                item.Initialize(action.ActionPacket, owner.Character);
                //the actions in skill/attackcontainer need to be able to request an initialization from the scriptableobject
            }
        }

    }


}