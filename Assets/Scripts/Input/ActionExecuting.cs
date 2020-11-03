using Assets.Scripts.Actions.Attacks;
using Assets.Scripts.Units;

namespace Assets.Scripts.Input
{
    public class ActionExecuting : BaseChargeState
    {
        public override void Enter(Unit parent)
        {
            _action = _parent.currentAction;
            base.Enter(parent);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void UpdateState()
        {
            base.UpdateState();
            _parent.ResetActionState();
        }
    }
}
