
using System.Collections.Generic;
using System.Linq;
/// <summary>
/// This class is responsible for passing Input from the player to the Active character class. IUnitController is the only public portion of 
/// this class so you should only call this via the interface
/// Other classes to control AI input need to implement the IUnitcontroller separately
/// </summary>
namespace Assets.Scripts.Units
{
    //This should be moved to the DLL
    public class AIActionContainer
    {
        public AIAction lightAttack = new AIAction();
        public AIAction heavyAttack = new AIAction();
        public AIAction actionOne = new AIAction();
        public AIAction actionTwo = new AIAction();
        public AIAction actionThree = new AIAction();
        public AIAction actionFour = new AIAction();
        public AIAction maneuver = new AIAction();

        public List<AIAction> sortedActions, actionList;
        
        public void SubscribeAllActions()
        {
            sortedActions = new List<AIAction>();
            actionList = new List<AIAction>();

            lightAttack.SubscribeItem(this, ActionSlot.lightAttack);
            heavyAttack.SubscribeItem(this, ActionSlot.heavyAttack);
            actionOne.SubscribeItem(this, ActionSlot.skillOne);
            actionTwo.SubscribeItem(this, ActionSlot.skillTwo);
            actionThree.SubscribeItem(this, ActionSlot.skillThree);
            actionFour.SubscribeItem(this,ActionSlot.skillFour);
            maneuver.SubscribeItem(this, ActionSlot.maneuver);
        }
        public void SubscribeAction(AIAction action)
        {
            actionList.Add(action);
        }
        public AIAction CheckActions(float distance)
        {
            OrderActions();
            foreach (AIAction action in sortedActions)
            {
                if (action.CheckAction(distance) && action != null)
                { 
                    return action;
                }
            }
        OrderActions();
            return null;
        }
        private void OrderActions()
        {
            sortedActions = actionList.OrderBy(x => x.currentWeight).ToList();
            foreach (var item in actionList)
            {
                item.ChangeWeight();//lowers everythings weight every time the lsit is sorted
            }
        }
    }
}
