using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.AI.EnemyStates
{
    public class FollowState : IState
    {
        private MinionMovement owner;

        public void Enter(MinionMovement minion)
        {
            owner = minion;
        }
        public void Update()
        {

            if (owner.MyTarget != null)
            {
                float distance = Vector2.Distance(owner.transform.position, myTarget.transform.position);
                if (distance <= myAttackRange)
                {
                    ChangeState(new AttackState());
                }
            }
            if (!InRange)
            {
                ChangeState(new EvadeState());
            }
            else
            {

            }
        }

        public void Exit()
        {
            Debug.Log("exit");
        }

        public void Enter(Enemy parent)
        {
            throw new NotImplementedException();
        }
    }
}