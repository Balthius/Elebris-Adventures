using Assets.Scripts.Actions.Attacks;
using Assets.Scripts.Units;
using Elebris.Rpg.Library.Actions.Core;

namespace Assets.Scripts.Input
{
    public class BaseChargeState : IChargingState
    {
        protected Unit _parent;
        protected BundledUnityAction _action;
        public virtual void Enter(Unit parent)
        {

            _parent = parent;

        }

        public virtual void Exit()
        {
        }
        public virtual void UpdateState()
        {
        }
    }
}
