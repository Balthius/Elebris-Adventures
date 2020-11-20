using Assets.Scripts.Actions.Attacks;
using Assets.Scripts.Units;

namespace Assets.Scripts.Input
{
    public class BaseChargeState : IChargingState
    {
        protected MonoUnit _parent;
        protected ActionBase _action;
        public virtual void Enter(MonoUnit parent)
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
