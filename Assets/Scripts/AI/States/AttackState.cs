
using Assets.Scripts.Units;
using UnityEngine;

public class AttackState : BaseState
{

    public override void Enter(CpuInputController parent)
    {
        base.Enter(parent);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override  void UpdateState()
    {
        base.UpdateState();
        if(_parent.Target != null)
        {
            float distance = Vector2.Distance(_parent.Target.position, _parent.transform.position);

            if (distance > _parent.AttackRange)
            {
                _parent.ChangeState(new FollowState());
            }
            //check range and attack
        }
        else
        {
            _parent.ChangeState(new IdleState());
        }
    }
}

 