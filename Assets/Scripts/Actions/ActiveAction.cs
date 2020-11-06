using Assets.Scripts.Units;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Actions.Attacks
{


    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    [Serializable]
    public class ActiveAction : MonoBehaviour
    {
        //this behaviour will need to be moved to a CircleAction so that activeaction can remain usable elsewhere
        //These will remain on the Active action
        private Rigidbody2D actionRB;
        private CircleCollider2D actionCollider;
        private Unit parentPlayer;
        private Vector2 directionUsed; //if the skill travels at all
        private float durationDone;//til it needs to destroy itself
        private float travelSpeed;
        public int maxCharges, currentCharge;

        //target as an optional variable if the skill  moves towards/doesnt function off collision with something.


        //Anything like this, that is used in the calculation once it hits something will go into a struct that contains the items as CharacterValues
        //private Element elementType;
        //private float elementDamage;
        private float actionDamage;

        //this stores the values inherent to ActiveAction
        public void Initialize(Unit owner, int currentCharge, float duration, Vector2 directionUsed, float travelSpeed = 0f)
        {
            actionRB = GetComponent<Rigidbody2D>();
            actionCollider = transform.GetComponent<CircleCollider2D>();
            parentPlayer = owner;
            durationDone = duration;
            StartCoroutine(DestroySelf(durationDone));
            this.directionUsed = directionUsed;
            this.travelSpeed = travelSpeed;
            Debug.Log(actionRB);
            for (int i = 0; i < currentCharge; i++)
            {
                //this is a hack job, radius is only one of many possible effects available to you.
                //should pass in a "chargeTrigger" that can take a list of modifiers from the player/skill as parameters.
                IncreaseRadius();
            }
        }
        private void FixedUpdate()
        {
            //Debug.Log(actionRB.position + "current action Position" + "speed"  + travelSpeed + "direction used " + directionUsed + "and duration " + durationDone);
            actionRB.MovePosition(actionRB.position + directionUsed * travelSpeed * Time.fixedDeltaTime);
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