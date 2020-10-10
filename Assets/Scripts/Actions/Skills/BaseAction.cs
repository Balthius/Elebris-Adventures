using UnityEngine;

namespace Assets.Scripts.Units
{
    public class BaseAction : MonoBehaviour
    {

        protected string actionName;
        protected Sprite actionIcon;
        protected float actionDamage;
        protected float animationLength;
        protected float baseChargeTime;
        protected int chargeMax;

        protected GameObject actionPrefab;

    }

}