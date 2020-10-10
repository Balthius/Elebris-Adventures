using System;
using UnityEngine;

namespace Assets.Scripts.Units
{

    [Serializable]
    public class ActionScriptableObject : ScriptableObject
    {

        [SerializeField] protected Sprite actionIcon;
        [SerializeField] protected string actionName;
        [SerializeField] protected float animationLength;
        [SerializeField] protected float baseChargeTime;
        [SerializeField] protected int chargeMax;

        [SerializeField] protected GameObject actionPrefab;


    }


}