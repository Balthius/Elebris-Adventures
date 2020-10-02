namespace Assets.DapperEvents.GameEvents.Listeners
{
    public interface IGameEventListener<T>
    {
        void OnEventRaised(T item);
    }
}