
using Assets.Scripts.Units;

public interface IState
{
    void Enter(CpuInputController parent);

    void UpdateState();

    void Exit();
}

 