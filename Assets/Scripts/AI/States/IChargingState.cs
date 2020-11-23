using Assets.Scripts.Units;

public interface IChargingState
{
    void Enter(Unit parent);

    void UpdateState();

    void Exit();
}

