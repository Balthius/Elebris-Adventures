using Assets.Scripts.Units;

public interface IChargingState
{
    void Enter(Entity parent);

    void UpdateState();

    void Exit();
}

