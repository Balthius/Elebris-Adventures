
using Assets.Scripts.Units;
using UnityEngine;

public class AttackState : BaseState
{

    public override void Enter(CpuInputController parent)
    {
        Debug.Log("Attacking");
        base.Enter(parent);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(_parent.Target != null)
        {
            if(_parent.currentAction.actionRange < _parent.DistanceFromTarget)
            {
                _parent.ChangeState(new FollowState());
            }
            else
            {
                UseAction(_parent.currentAction);
            }
        }
        else
        {
            _parent.ChangeState(new IdleState());
        }
    }

    public void UseAction(AIAction action)
    {
        switch (action.slot)
        {
            case ActionSlot.lightAttack:
                _parent.ChargingLightAttack = true;
                break;
            case ActionSlot.heavyAttack:
                _parent.ChargingHeavyAttack = true;
                break;
            case ActionSlot.skillOne:
                _parent.ChargingSkillOne = true;
                break;
            case ActionSlot.skillTwo:
                _parent.ChargingSkillTwo = true;
                break;
            case ActionSlot.skillThree:
                _parent.ChargingSkillThree = true;
                break;
            case ActionSlot.skillFour:
                _parent.ChargingSkillFour = true;
                break;
            case ActionSlot.maneuver:
                _parent.ChargingManeuver = true;
                break;

            default: _parent.ChargingSelect = true;
                break;
        }
        _parent.ChargeAction(action.chargeDuration);
    }
}

 