using System;
using UnityEngine;

namespace Assets.Scripts.AI.EnemyStates
{
    public class MinionMovement : MonoBehaviour
    {
        internal int myAttackTime;

        public bool IsAttacking { get; internal set; }
        public Transform MyTarget { get; internal set; }
        public float MyAttackRange { get; internal set; }
        public Animation Anim { get; internal set; }

        internal void ChangeState(IdleState idleState)
        {
            throw new NotImplementedException();
        }
    }
}