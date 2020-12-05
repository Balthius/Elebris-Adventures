using Elebris.Rpg.Library.ActionManager;

namespace Assets.Scripts.Units
{
    public class BundledUnityAction
    {
        public ActivatableAction action;
        public ActionBuilder activeBehaviour;
        public float actionLock;
        public BundledUnityAction(ActivatableAction action)
        {
            this.action = action;
            activeBehaviour = (ActionBuilder)action.behaviour.ReturnBehaviour();
        }
    }

}