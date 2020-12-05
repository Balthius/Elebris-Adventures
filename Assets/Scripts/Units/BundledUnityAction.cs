using Elebris.Rpg.Library.ActionManager;

namespace Assets.Scripts.Units
{
    public class BundledUnityAction
    {
        public ActivatableAction action;
        public ActionBehaviour activeBehaviour;
        public float actionLock;
        public BundledUnityAction(ActivatableAction action)
        {
            this.action = action;
            activeBehaviour = (ActionBehaviour)action.behaviour.ReturnBehaviour();
        }
    }

}