using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AI.EnemyStates
{
    public interface IState
    {
        //Prepare the state
        void Enter(Enemy parent);

        void Update();

        void Exit();
    }
}