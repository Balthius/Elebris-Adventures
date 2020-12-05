using Assets.Scripts.Units;
using Elebris.Actions.Library.Actions.Core;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Actions.Attacks
{


    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    [Serializable]
    public class ExecutedAction : MonoBehaviour
    {
        private Rigidbody2D actionRB;
        private CircleCollider2D actionCollider;
        private Vector2 directionUsed; //if the skill travels at all
        public int currentCharge;
        BundledUnityAction activeAction;
        public void Initialize(Unit owner, int currentCharge, Vector2 directionUsed, BundledUnityAction storedAction)
        {
            activeAction = storedAction;
            actionRB = GetComponent<Rigidbody2D>();
            actionCollider = transform.GetComponent<CircleCollider2D>();

            StartCoroutine(DestroySelf(activeAction.activeBehaviour.actionDuration));
            this.directionUsed = directionUsed;
            for (int i = 0; i < currentCharge; i++)
            {
                //this is a hack job, radius is only one of many possible effects available to you.
                //should pass in a "chargeTrigger" that can take a list of modifiers from the player/skill as parameters.
                ChargeSkill();
            }
        }
        private void FixedUpdate()
        {
            actionRB.MovePosition(actionRB.position + directionUsed * activeAction.activeBehaviour.actionSpeed * Time.fixedDeltaTime);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if(col.GetComponent<Unit>().container != null)
            activeAction.action.containedAction.Execute(col.GetComponent<Unit>().container);
            //if target != null you can have that as a condition for hitting,
            Debug.Log($"You hit {col.name}");
            //if (Idamagable?)
            //{
            //    Character c = col.GetComponentInParent<Character>();
            //    c.TakeDamage(physDamage, source);
            //    DestroySelf(.01);
            //}
        }
        public void ChargeSkill()
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