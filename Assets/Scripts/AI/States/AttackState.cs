
using Assets.Scripts.Units;
using Elebris.Rpg.Library.Units.AI;
using System.Collections;
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

        if (_parent.Target != null)
        {

            if (_parent.UsingAction) return;
            if (_parent.AttemptAttackRange > _parent.DistanceFromTarget)
            {
                _parent.currentAction = _parent.ActionContainer.CheckActions(_parent.DistanceFromTarget);
                UseAction(_parent.currentAction);
            }
            else if (_parent.AttemptAttackRange < _parent.DistanceFromTarget)
            {
                _parent.ChangeState(new FollowState());
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
            case ActionSlot.select:

            default:
                _parent.ChargingSelect = true;
                break;
        }
        //Can also use GetCurrentAnimatorStateInfo(0).Length if we'd like
        _parent.StartCoroutine(ChargeAction(action.maxActionDelay));
        Debug.Log("using" + action);
    }

    public IEnumerator ChargeAction(float duration)
    {
        //ensures actions arent being checked for while another is executing
        _parent.UsingAction = true;
        yield return new WaitForSeconds(duration);
        _parent.EndAction();
    }



}
