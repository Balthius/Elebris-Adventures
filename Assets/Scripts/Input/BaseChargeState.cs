using Assets.Scripts.Actions.Attacks;
using Assets.Scripts.Units;

namespace Assets.Scripts.Input
{
    public class BaseChargeState : IChargingState
    {
        protected IUnitController _unitController;
        protected Unit _parent;
        protected ActiveAction _action;
        public virtual void Enter(Unit parent)
        {
            _unitController = _parent._unitController;
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
