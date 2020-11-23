using Assets.Scripts.Actions.Attacks;
using Assets.Scripts.Units;

namespace Assets.Scripts.Input
{
    public class BaseChargeState : IChargingState
    {
        protected Unit _parent;
        protected ActionBase _action;
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
