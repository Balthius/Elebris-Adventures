using System;

namespace Assets.Scripts.Units
{
    public class UnityActionReference
    {
        public IActionBehaviour behaviour;
        public Action Unequipped;

        public UnityActionReference(IActionBehaviour behaviour)
        {
            this.behaviour = behaviour;
        }

        public void Subscribe(UnityBoundAction action)
        {
            Unequipped = action.DereferenceAction;
        }

        public void Unsubscribe()
        {
            Unequipped();
        }

    }
}