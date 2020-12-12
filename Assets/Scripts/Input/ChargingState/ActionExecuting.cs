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
            boundAction.StartAction(parent);
            boundAction = null;
        }

        public override void Exit()
        {
            base.Exit();
            boundAction = null;
        }

        public override void UpdateState()
        {
            base.UpdateState();
            if(!boundAction)
            {
                _parent.ChangeState(new ActionWaiting());
            }
        }
    }
}
