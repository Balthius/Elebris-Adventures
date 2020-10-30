
using Assets.Scripts.Units;
using UnityEngine;

public class FollowState : BaseState
{

    public override void Enter(CpuInputController parent)
    {
        Debug.Log("Following");
        base.Enter(parent);
    }

    public override void Exit()
    {
        base.Exit();
        _parent.CurrentMovement = Vector2.zero;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        //going to need to work on selectively locking out movement here as well for when the unit is attacking
       if(_parent.Target != null)
        {
            _parent.CurrentMovement = _parent.Target.position - _parent.transform.position;
            if (_parent.currentAction.actionRange > _parent.DistanceFromTarget)
            {
                _parent.ChangeState(new AttackState());
            }
        }
        else
        {
            _parent.ChangeState(new IdleState());
        }
    }
}

 