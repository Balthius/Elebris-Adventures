using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Assets.Scripts.AI.EnemyStates
{
    public class AttackState : IState
    {

        private MinionMovement owner;

        private float attackCooldown = 3;

        private float extraRange = .1f;

        public void Enter(MinionMovement minion)
        {
            owner = minion;
        }

        public void Update()
        {
            if (owner.myAttackTime >= attackCooldown && !owner.IsAttacking)
            {
                owner.myAttackTime = 0;

                owner.StartCoroutine(Attack());
            }


            if (owner.MyTarget != null)
            {
                float distance = Vector2.Distance(owner.MyTarget.position, owner.transform.position);

                if (distance >= owner.MyAttackRange + extraRange && !owner.IsAttacking)
                {
                    owner.ChangeState(new FollowState());
                }
            }

            else
            {
                owner.ChangeState(new IdleState());
            }
        }

        public void Exit()
        {

        }

        public IEnumerator Attack()
        {
            owner.IsAttacking = true;
            owner.Anim.SetTrigger("Attack");
            yield return new WaitForSeconds(owner.animator.GetCurrentAnimatorStateInfo(0).length);

            owner.IsAttacking = false;
        }
    }
}