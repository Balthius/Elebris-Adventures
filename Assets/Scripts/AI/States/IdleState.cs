
using Assets.Scripts.Units;
using UnityEngine;

public class IdleState : BaseState
{

    public override void Enter(CpuInputController parent)
    {
        Debug.Log("Idling");
        base.Enter(parent);
    }
    public override void UpdateState()
    {
        base.UpdateState();
        //going to need to work on selectively locking out movement here as well for when the unit is attacking
        if (_parent.Target != null)
        {
            _parent.ChangeState(new FollowState());
        }
        else
        {
            _parent.CurrentMovement = Vector2.zero;
        }
    }
    public override void Exit()
    {
        base.Exit();
    }

}

 