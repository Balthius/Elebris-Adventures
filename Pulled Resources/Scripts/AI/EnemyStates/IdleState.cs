using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.AI.EnemyStates
{
    public class IdleState : IState
    {
        private Enemy parent;

        public void Enter(Enemy parent)
        {
            this.parent = parent;

            this.parent.Reset();
        }

        public void Update()
        {
            if (parent.MyTarget != null)
            {

                parent.ChangeState(new FollowState());
            }
        }

        public void Exit()
        {

        }

    }
}