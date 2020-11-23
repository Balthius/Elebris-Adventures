using Elebris.Core.Library.CharacterValues.Mutable;
using System;
using UnityEngine;

namespace Assets.Scripts.Units
{
    [Serializable]
    public struct UnitActionBehaviourModel
    {
        //inheret from non-unity behaviourmodel
        public string actionName;

        public CharacterValue baseChargeTime;
        public CharacterValue actionDuration;
        public CharacterValue actionSpeed;


        public CharacterValue actionCooldown;
        public bool canCharge;
        public bool destroyOnContact; //some skills will stay their whole duration, or pierce etc

        //This is the info that needs to be calculated before an action is sent towards its target
        //all of these values are inherent to the whole action, not the individual components
        public Sprite actionIcon;
        public GameObject actionPrefab;

        public CharacterValue animationLength;
        public UnitActionBehaviourModel(Sprite actionIcon, string actionName, GameObject actionPrefab, CharacterValue animationLength, CharacterValue baseChargeTime, CharacterValue actionDuration, CharacterValue actionSpeed, CharacterValue actionCooldown, bool canCharge, bool destroyOnContact)
        {
            this.actionIcon = actionIcon;
            this.actionName = actionName;
            this.actionPrefab = actionPrefab;
            this.animationLength = animationLength;
            this.baseChargeTime = baseChargeTime;
            this.actionDuration = actionDuration;
            this.actionSpeed = actionSpeed;
            this.actionCooldown = actionCooldown;
            this.canCharge = canCharge;
            this.destroyOnContact = destroyOnContact;
        }
    }

}