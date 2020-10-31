
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/// <summary>
/// This acts as the "gamepad" for the AI. cotaining any button the Unit would need to press
/// Also handles the sorting of actions so they can choose what they want to press next
/// </summary>
namespace Assets.Scripts.Units
{
    //This should be moved to the DLL
    public class AIActionContainer
    {
        //I plan on having these be overriden in editor
        public AIAction lightAttack = new AIAction(ActionSlot.lightAttack);
        public AIAction heavyAttack = new AIAction(ActionSlot.heavyAttack);
        public AIAction actionOne = new AIAction(ActionSlot.skillOne);
        public AIAction actionTwo = new AIAction(ActionSlot.skillTwo);
        public AIAction actionThree = new AIAction(ActionSlot.skillThree);
        public AIAction actionFour = new AIAction(ActionSlot.skillFour);
        public AIAction maneuver = new AIAction(ActionSlot.maneuver);

        public List<AIAction> sortedActions = new List<AIAction>(), actionList = new List<AIAction>();
        
        public void GroupActions()
        {
            //Debug.Log("Grouping Actions");
            //Execute once
            actionList.Add(lightAttack);
            actionList.Add(heavyAttack);
            actionList.Add(actionOne);
            actionList.Add(actionTwo);
            actionList.Add(actionThree);
            actionList.Add(actionFour);
            actionList.Add(maneuver);
            //Debug.Log("actionlist count" + actionList.Count);
            OrderActions();
            //Debug.Log("SortedList count" + sortedActions.Count);
        }
        public AIAction CheckActions(float distance)
        {
            OrderActions();
            foreach (AIAction action in sortedActions)
            {
                if (action.CheckAction(distance))
                {
                    return action;
                }
            }
            return null;
        }
        private void OrderActions()
        {
            //Debug.Log("Ordering Actions");
            sortedActions = actionList.OrderBy(x => x.currentWeight).ToList();
        }
    }
}
