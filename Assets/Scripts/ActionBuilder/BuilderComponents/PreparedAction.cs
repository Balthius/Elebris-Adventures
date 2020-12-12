using Assets.Scripts.Units;
using Elebris.Actions.Library.Actions.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Actions.Attacks
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    [Serializable]
    public class PreparedAction: MonoBehaviour, IPreparedAction
    {
        private Rigidbody2D actionRB;
        private CircleCollider2D actionCollider;
        private Vector2 directionUsed; //if the skill travels at all
        public PreparedActionBehaviour _behaviour;

        private Entity Owner { get; set; }
        public ICoreAction activeAction;

        public PreparedAction(PreparedActionBehaviour behaviour)
        {
            _behaviour = behaviour;
        }


        public PreparedAction Initialize()
        {
            return this;
        }
        public void Initialize(Entity owner, Vector2 directionUsed)
        {
            Owner = owner;
            actionRB = GetComponent<Rigidbody2D>();
            actionCollider = GetComponent<CircleCollider2D>();
            
            StartCoroutine(DestroySelf(_behaviour.actionDuration));

            this.directionUsed = directionUsed;
           
        }
        public void AddAction(ICoreAction coreAction)
        {
            activeAction = coreAction;
        }
        private void FixedUpdate()
        {
            actionRB.MovePosition(actionRB.position + directionUsed * _behaviour.actionSpeed * Time.fixedDeltaTime);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if(col.GetComponent<UnitData>().ValueContainer != null)
            {
              
                    activeAction.Execute(col.GetComponent<UnitData>().ValueContainer);
                
            }
        }
        public void ChargeSkill()
        {
            Debug.Log("Charged Skill");
        }

        private IEnumerator DestroySelf(float time)
        {
            yield return new WaitForSeconds(time);
            Destroy(gameObject);

        }

        public virtual void OnCastStarted(Entity caster)
        {
            //if (caster.audioSource != null && castSound != null)
            //    caster.audioSource.PlayOneShot(castSound);
        }
        public virtual void OnCastFinished(Entity caster) { }

        public bool CheckTarget(Entity caster)
        {
            return true;
        }

        public void Apply(Entity caster)
        {
           
        }
    }

    public interface IPreparedAction
    {
        bool CheckTarget(Entity caster);
        void Apply(Entity caster);
        void OnCastFinished(Entity caster);
        void OnCastStarted(Entity caster);
    }

}