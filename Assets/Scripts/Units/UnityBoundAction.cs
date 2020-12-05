
using Elebris.Rpg.Library.ActionManager;
using Elebris.Rpg.Library.CharacterValues;
using UnityEngine;

namespace Assets.Scripts.Units
{
    public class UnityBoundAction : MonoBehaviour
    {
        public ActionType allowedType;
        public ActionBehaviour actionBehaviour;

        private ActivatableAction action;
        public UnityBoundAction(ActionType allowedType)
        {
            this.allowedType = allowedType;
        }

        public void AddAction(UnityActionReference reference)
        {
            reference.Subscribe(this);
            //create the action Model from the references behaviour
        }
        public void DereferenceAction()
        {
            action = null;
        }
        public ActivatableAction ReturnAction(CharacterValueContainer parent)
        {
            action.containedAction.DefineAction(parent);
            return action;
        }
    }
}