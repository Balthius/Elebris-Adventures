
using Assets.Scripts.Actions.Attacks;
using Elebris.Core.Library.Components;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Units
{
    public class CharacterActionContainer : MonoBehaviour
    {
        public Dictionary<BindableActions, HotbarBoundAction> CharacterActions { get; set; }
        //retrieve hash(value) associated with the key, create and execute the skill

        public BaseActionData currentData;
        public HotbarActionBehaviour currentBehaviour;
        public Vector3 currentDirection;
        public IActionBuilder builder;
        public HotbarBoundAction BoundAction{ get;set; }
        private void Awake()
        {
            CharacterActions = new Dictionary<BindableActions, HotbarBoundAction>();
        }
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
            //skills should only be cast from here
            if (!CharacterActions[selectedAction].CanCast(parent.unitData.ValueContainer)) return;
            builder = CharacterActions[selectedAction].builder;
            currentData= builder.ReturnActionData();

            currentBehaviour= builder.ReturnHotbarBehaviour(parent.unitData.ValueContainer);  
           
            currentDirection = new Vector3(parent.facingDirection.x, parent.facingDirection.y, 0); //set direction

           
        }
        public void CancelAction(bool resetCurrentSkill = true)
        {  // reset cast time, otherwise if a buff has a 10s cast time and we
           // cancel the cast after 1s, then we would have to wait 9 more seconds
           // before we can attempt to cast it again.
           // -> we cancel it in any case. players will have to wait for 'casttime'
           //    when attempting another cast anyway.
        }
        public void FinishAction(Entity parent)
        {
            // * check if we can currently cast a skill (enough mana etc.)
            // * check if we can cast THAT skill on THAT target
            // note: we don't check the distance again. the skill will be cast even
            //   if the target walked a bit while we casted it (it's simply better
            //   gameplay and less frustrating)

            GameObject action = Instantiate(currentData.Obj);

            if (!action.GetComponent<PreparedAction>())
            {
                action.AddComponent<PreparedAction>();
            }

            StartCoroutine(parent.LockDuringAction(currentBehaviour.actionLock)); //time spent "casting" the skill (locked in place) whereas action duration is how long it sticks around


            PreparedAction defaultAction = builder.ReturnPreparedAction(parent.unitData.ValueContainer);
            defaultAction.Initialize(parent, currentDirection); //get components and set info from Players passives
        }

        private void Cleanup()
        {
             currentData = null;
             currentBehaviour = new HotbarActionBehaviour(); ;
             currentDirection = Vector3.zero;
             builder = null;
        }


    }
}
