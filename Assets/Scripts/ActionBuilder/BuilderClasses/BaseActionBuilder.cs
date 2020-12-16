using Assets.Helpers;
using Assets.Scripts.Actions.Attacks;
using Elebris.Core.Library.CharacterValues.Mutable;
using Elebris.Core.Library.Enums;
using Elebris.Rpg.Library.Actions.ActionBehaviour;
using Elebris.Rpg.Library.CharacterValues;
using Elebris.Rpg.Library.Utils;
using Elebris.UnitCreation.Library.StatGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public abstract class ScriptableObjectNonAlloc : ScriptableObject
{
    // .name allocates and we call it a lot. let's cache it to avoid GC.
    // (4.1KB/frame for skillbar items before, 0KB now)
    string cachedName;
    public new string name
    {
        get
        {
            if (string.IsNullOrWhiteSpace(cachedName))
                cachedName = base.name;
            return cachedName;
        }
        // set: not needed, we don't change ScriptableObject names at runtime.
    }


}
public class BaseActionBuilder : ScriptableObjectNonAlloc, IActionBuilder
{
   
    protected BaseActionData _baseActionData;
    protected HotbarActionBehaviour _hotBarBehaviour;
    protected PreparedActionBehaviour _preparedbehaviour;
    protected AbilityEffects[] _abilityEffects; //currently only onUse, eventually OnUse, OnTravel, OnHit 
    protected TwinStickEffectPlacement _placement;
    //ChargeBehaviour which controls what happens at each level of charge
    [Serializable]
    public class AbilityEffects
    {
        public ActionEffectBehaviour actionEffect;
        //foreach (var item in abilityEffects)
        //{
        //    item.actionEffect.PlayEffect();
        //}
    }

    [SerializeField, TextArea(1, 30)] protected string toolTip; // not public, use ToolTip()
    public virtual string ToolTip(CharacterValueContainer parent, int level)
    {
        StringBuilder tip = new StringBuilder(toolTip);
        tip.Replace("{NAME}", name);
        tip.Replace("{LEVEL}", level.ToString());
        //tip.Replace("{ACTIONLOCK}", InterfaceUtils.PrettySeconds(ModifedValue(_hotBarBehaviour.actionLock, Stats.ActionLockScale.ToString(), parent).TotalValue));
        tip.Replace("{ACTIONLOCK}", InterfaceUtils.PrettySeconds(_hotBarBehaviour.actionLock));
        //tip.Replace("{COOLDOWN}", InterfaceUtils.PrettySeconds(ModifedValue(_hotBarBehaviour.actionCooldown, Stats.CurrentCooldownReduction.ToString(), parent).TotalValue));        //tip.Replace("{ACTIONLOCK}", InterfaceUtils.PrettySeconds(ModifedValue(_hotBarBehaviour.actionLock, Stats.ActionLockScale.ToString(), parent).TotalValue));
        tip.Replace("{COOLDOWN}", InterfaceUtils.PrettySeconds(_hotBarBehaviour.actionCooldown));
        //tip.Replace("{CASTRANGE}", castRange.Get(level).ToString());
        foreach (var cost in _hotBarBehaviour.ResourceCosts)
        {
            tip.Replace("{COST}", cost.Amount.ToString());
            tip.Replace("{TYPE}", cost.type.ToString());
            tip.Replace("{RESOURCE}", cost.resource.ToString());
        }

        return tip.ToString();
    }

    public bool CanCast(CharacterValueContainer parent)
    {
        foreach (var item in _hotBarBehaviour.ResourceCosts)
        {
            float val = 0;
            var cur = parent.DataHandler.RetrieveResourceData(item.resource);
            switch (item.type)
            {
                case CostType.PercentCurrent:
                    val = (item.Amount / 100) * cur.CurrentValue;
                    break;
                case CostType.PercentMaximum:
                    val = (item.Amount / 100) * cur.MaxValue;
                    break;
                case CostType.PercentMissing:
                    val = (item.Amount / 100) * cur.MissingValue;
                    break;
                case CostType.Flat:
                    val = item.Amount;
                    break;
                default:
                    break;
            }
            if (val > cur.CurrentValue)
            {
                return false;
            }
        }
        return true;

        //return true only if the user can afford to cast the skill and its off cooldown
    }
    protected StatValue ModifedValue(float value, string stat, CharacterValueContainer parent)
    {
        StatValue val = parent.DataHandler.CopyStat(stat);
        val.AddModifier(new ValueModifier(value, ValueModEnum.Flat));
        return val;
    }
  

    public BaseActionData ReturnActionData()
    {
        return _baseActionData;
    }

    public virtual HotbarActionBehaviour ReturnHotbarBehaviour(CharacterValueContainer container)
    {
        throw new NotImplementedException();
    }

    public virtual PreparedAction ReturnPreparedAction(CharacterValueContainer container)
    {
        throw new NotImplementedException();
    }

    protected static Dictionary<int, BaseActionBuilder> cache;
    public static Dictionary<int, BaseActionBuilder> dict
    {
        get
        {
            // not loaded yet?
            if (cache == null)
            {
                // get all ScriptableActions in resources
                BaseActionBuilder[] skills = Resources.LoadAll<BaseActionBuilder>("");

                // check for duplicates, then add to cache
                List<string> duplicates = skills.ToList().FindDuplicates(skill => skill.name);
                if (duplicates.Count == 0)
                {
                    cache = skills.ToDictionary(skill => skill.name.GetStableHashCode(), skill => skill);
                }
                else
                {
                    foreach (string duplicate in duplicates)
                        Debug.LogError("Resources folder contains multiple ScriptableSkills with the name " + duplicate + ". If you are using subfolders like 'Warrior/NormalAttack' and 'Archer/NormalAttack', then rename them to 'Warrior/(Warrior)NormalAttack' and 'Archer/(Archer)NormalAttack' instead.");
                }
            }
            return cache;
        }
    }

}
