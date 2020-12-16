using Assets.Scripts.Actions.Attacks;
using Assets.Scripts.Units;

namespace Assets.Scripts.Input
{
    public class ActionExecuting : BaseChargeState
    {
        public override void Enter(Entity parent)
        {
            base.Enter(parent);
            _parent.Animator.SetTrigger("ActionUsed");
            _parent.unitData.ActionContainer.BoundAction = null;
        }

        public override void Exit()
        {
            base.Exit();
            _parent.unitData.ActionContainer.BoundAction = null;
        }

        public override void UpdateState()
        {
            base.UpdateState();
            if(_parent.unitData.ActionContainer.BoundAction == null)
            {
                _parent.ChangeState(new ActionWaiting());
            }
        }
    }
}
