using Assets.DapperEvents.GameEvents.Events;
using Assets.DapperEvents.GameEvents.UnityEvents;

namespace Assets.DapperEvents.GameEvents.Listeners
{
    public class IntListener : BaseGameEventListener<int, IntEvent, UnityIntEvent> { }
}