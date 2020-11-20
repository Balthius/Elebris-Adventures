using Assets.Scripts.Units;
using Elebris.Actions.Library.Actions.Core;
using Elebris.Core.Library.CharacterValues.Mutable;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Actions.Attacks
{
    public class ActionBase : MonoBehaviour
    {
        public CharacterAction ActionPacket { get; set; }
        public MonoActionBehaviorModel ActionInfo { get; set; }
    }

    public class StoredAction : ActionBase
    {
        ValueHolder Cooldown { get; set; }
    }

    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    [Serializable]
    public class ExecutedAction : ActionBase
    {
        private Rigidbody2D actionRB;
        private CircleCollider2D actionCollider;
        private Vector2 directionUsed; //if the skill travels at all
        public int currentCharge;

        public void Initialize(MonoUnit owner, int currentCharge, Vector2 directionUsed, ActionBase storedAction)
        {
            ActionPacket = storedAction.ActionPacket;
            ActionInfo = storedAction.ActionInfo;
            actionRB = GetComponent<Rigidbody2D>();
            actionCollider = transform.GetComponent<CircleCollider2D>();
            ActionPacket.User = owner.Character;
            StartCoroutine(DestroySelf(ActionInfo.actionDuration.ReturnValue));
            this.directionUsed = directionUsed;
            for (int i = 0; i < currentCharge; i++)
            {
                //this is a hack job, radius is only one of many possible effects available to you.
                //should pass in a "chargeTrigger" that can take a list of modifiers from the player/skill as parameters.
                IncreaseRadius();
            }
        }
        private void FixedUpdate()
        {
           
            actionRB.MovePosition(actionRB.position + directionUsed * ActionInfo.actionSpeed.ReturnValue * Time.fixedDeltaTime);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            
            //if target != null you can have that as a condition for hitting,
            Debug.Log($"You hit {col.name}");
            //if (Idamagable?)
            //{
            //    Character c = col.GetComponentInParent<Character>();
            //    c.TakeDamage(physDamage, source);
            //    DestroySelf(.01);
            //}
        }
        public void IncreaseRadius()
        {
            actionCollider.transform.localScale *= 1.3f;// Was .radius, wouldn't have affected visual, amy revert if I set up different animations
        }

        private IEnumerator DestroySelf(float time)
        {
            yield return new WaitForSeconds(time);
            Destroy(gameObject);

        }

        //public void AddCalculation()
        //RemoveCalculation()
        //ConvertCalculation()
    }
}