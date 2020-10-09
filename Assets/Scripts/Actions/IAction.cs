using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Actions
{
    public interface IAction
    {
        void Implement();

        int ChargeMax(); //return the highest youre allowed to charge this skill
    }

}