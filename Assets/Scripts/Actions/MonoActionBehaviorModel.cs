using Elebris.Core.Library.CharacterValues.Mutable;
using UnityEngine;

namespace Assets.Scripts.Units
{

    public struct MonoPassiveBehaviorModel
    {
        public Sprite actionIcon;
        public string actionName;
        public GameObject actionPrefab;
    }
    public struct MonoActionBehaviorModel
    {
        //This is the info that needs to be calculated before an action is sent towards its target
        //all of these values are inherent to the whole action, not the individual components
        public Sprite actionIcon;
        public string actionName;
        public GameObject actionPrefab;

        public CharacterValue animationLength;

        public CharacterValue baseChargeTime;
        public CharacterValue actionDuration;
        public CharacterValue actionSpeed;


        public CharacterValue actionCooldown;
        public bool canCharge;
        public bool destroyOnContact; //some skills will stay their whole duration, or pierce etc

        public MonoActionBehaviorModel(Sprite actionIcon, string actionName, GameObject actionPrefab, CharacterValue animationLength, CharacterValue baseChargeTime, CharacterValue actionDuration, CharacterValue actionSpeed, CharacterValue actionCooldown, bool canCharge, bool destroyOnContact)
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