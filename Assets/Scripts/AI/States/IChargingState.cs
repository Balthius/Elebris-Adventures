using Assets.Scripts.Units;

public interface IChargingState
{
    void Enter(MonoUnit parent);

    void UpdateState();

    void Exit();
}

