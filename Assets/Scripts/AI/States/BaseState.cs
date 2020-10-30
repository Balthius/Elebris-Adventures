
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
        AIAction checkAction = _parent.ActionContainer.CheckActions(_parent.DistanceFromTarget); 
        if(checkAction != null)
        {
            _parent.currentAction = checkAction;
        }
    }

}

 