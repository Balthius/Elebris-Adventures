
using Assets.Scripts.Units;

public class BaseState : IState
{
    protected CpuInputController _parent;
    public virtual void Enter(CpuInputController parent)
    {
        _parent = parent;

    }

    public virtual void Exit()
    {
    }
    public virtual void UpdateState()
    {
        //Debug.Log("Using action is: " + _parent.UsingAction);
        if (_parent.UsingAction) return;
        //short-circuit all behaviour while carrying out an action
    }

}

