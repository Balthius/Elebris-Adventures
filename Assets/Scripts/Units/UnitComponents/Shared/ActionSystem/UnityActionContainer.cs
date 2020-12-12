
using Assets.Scripts.Actions.Attacks;
using Elebris.Core.Library.Components;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Units
{
    public class CharacterActionContainer : MonoBehaviour
    {
        public Dictionary<BindableActions, HotbarBoundAction> CharacterActions = new Dictionary<BindableActions, HotbarBoundAction>();
        //retrieve hash(value) associated with the key, create and execute the skill

        private void Update()
        {
            foreach (var action in CharacterActions.Values)
            {
                if (action.cooldown.CurrentValue < action.cooldown.MaxValue) action.cooldown.CurrentValue += Time.deltaTime;
                if (action.cooldown.CurrentValue > action.cooldown.MaxValue) action.cooldown.CurrentValue = action.cooldown.MaxValue;
            }
        }


        public void StartAction(Entity parent, BindableActions selectedAction)
        {
            if (!CharacterActions[selectedAction].CanCast(parent.unitData.ValueContainer)) return;
            IActionBuilder builder = CharacterActions[selectedAction].data;
            BaseActionData data = builder.ReturnActionData();

            HotbarActionBehaviour actionBehaviour = new HotbarActionBehaviour();    
            builder.Modifybehaviour(ref actionBehaviour, parent.unitData.ValueContainer);

            Vector3 actionDirection = new Vector3(parent.facingDirection.x, parent.facingDirection.y, 0); //set direction

            StartCoroutine(parent.LockDuringAction(actionBehaviour.actionLock)); //time spent "casting" the skill (locked in place) whereas action duration is how long it sticks around

            GameObject action = Instantiate(data.Obj);

            if (!action.GetComponent<PreparedAction>())
            {
                action.AddComponent<PreparedAction>();
            }

            PreparedAction defaultAction = action.GetComponent<PreparedAction>();
            defaultAction.Initialize(parent, actionDirection); //get components and set info from Players passives
            builder.ModifyAction(ref defaultAction, parent.unitData.ValueContainer);


        }
        public void CancelAction(bool resetCurrentSkill = true)
        {  // reset cast time, otherwise if a buff has a 10s cast time and we
           // cancel the cast after 1s, then we would have to wait 9 more seconds
           // before we can attempt to cast it again.
           // -> we cancel it in any case. players will have to wait for 'casttime'
           //    when attempting another cast anyway.
        }
        public void FinishAction()
        {
            // * check if we can currently cast a skill (enough mana etc.)
            // * check if we can cast THAT skill on THAT target
            // note: we don't check the distance again. the skill will be cast even
            //   if the target walked a bit while we casted it (it's simply better
            //   gameplay and less frustrating)
        }


    }
}
