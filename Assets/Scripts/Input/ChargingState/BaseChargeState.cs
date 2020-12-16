using Assets.Scripts.Actions.Attacks;
using Assets.Scripts.Units;

namespace Assets.Scripts.Input
{
    public class BaseChargeState : IChargingState
    {
        protected Entity _parent;
        public virtual void Enter(Entity parent)
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
