
using Assets.Scripts.Units;
using UnityEngine;

public class RetreatState : BaseState
{
    public override void Enter(CpuInputController parent)
    {
        
        Debug.Log("Retreating");
        base.Enter(parent);
        _parent.UnitPanicked = true;
    }

    public override void Exit()
    {
        base.Exit();
        _parent.UnitPanicked = false;
    }

    public override void UpdateState()
    {
        //more or less working but needs to have a delay built in 
        base.UpdateState();
        if (_parent.Target != null)
        {
            _parent.CurrentMovement = _parent.transform.position - _parent.Target.position;
            //if Unit is within min and max ranges then switch to follow
            if (_parent.AttemptRetreatRange < _parent.DistanceFromTarget && _parent.AttemptAttackRange > _parent.DistanceFromTarget)
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