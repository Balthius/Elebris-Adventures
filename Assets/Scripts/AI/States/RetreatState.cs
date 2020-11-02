
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
            if (_parent.AttemptAttackRange > _parent.DistanceFromTarget)
            {
                //and needs to be outside min retreat range.

                //need to find a way to let creatures react if the player stays close to them.
                _parent.ChangeState(new AttackState());
            }
        }
        else
        {
            _parent.ChangeState(new IdleState());
        }
    }
}