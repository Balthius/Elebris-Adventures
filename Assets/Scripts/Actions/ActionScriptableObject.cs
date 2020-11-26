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

        [SerializeField]IActionComponent[] components;
        public virtual void GenerateComponents(ref BoundAction action, Unit owner)
        {
            action.ActionInfo = model;
            foreach (var item in components)
            {
                item.Initialize(owner.Character);
                action.ActionPacket.AddComponent(item);
                //the actions in skill/attackcontainer need to be able to request an initialization from the scriptableobject
            }
        }

    }


}