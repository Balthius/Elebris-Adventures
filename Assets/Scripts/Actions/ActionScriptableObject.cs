using System;
using UnityEngine;

namespace Assets.Scripts.Units
{
    [CreateAssetMenu(menuName = "Actions/New Action")]
    [Serializable]
    public class ActionScriptableObject : ScriptableObject
    {

        public Sprite actionIcon;
        public string actionName;
        public float animationLength;

        public int actionDamage;
        public float baseChargeTime;
        public int chargeMax;
        public float actionDuration;
        public float actionSpeed;

        public GameObject actionPrefab;
    }


}