
using Elebris.Actions.Library.Actions.Core;
using Elebris.Core.Library.CharacterValues.Mutable;
using Elebris.Rpg.Library.ActionManager;
using Elebris.Rpg.Library.CharacterValues;
using UnityEngine;

namespace Assets.Scripts.Units
{
    public class UnityBoundAction : MonoBehaviour
    {

        public ResourceBarValue cooldown { get; set; }
        public ActionType allowedType;
        public IActionBehaviour actionBehaviour;
        public ICoreAction action;
        
        public UnityBoundAction(ActionType allowedType)
        {
            this.allowedType = allowedType;
        }

        public void AddAction(UnityActionReference reference)
        {
            reference.Subscribe(this);
            actionBehaviour = reference.behaviour;
            //create the action Model from the references behaviour 
            //DO THIS TODAY
        }
        public void DereferenceAction()
        {
            action = null;
        }
        public ICoreAction ReturnAction(CharacterValueContainer parent)
        {
            action.DefineAction(parent);
            return action;
        }
    }
}