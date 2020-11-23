using Assets.Scripts.Units;

namespace Assets.Scripts.Input
{
    public class ActionExecuting : BaseChargeState
    {
        public override void Enter(Unit parent)
        {

            _parent.Animator.SetTrigger("ActionUsed");
            _action = _parent.ActionBase;
            base.Enter(parent);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void UpdateState()
        {
            base.UpdateState();
        }
    }
}
