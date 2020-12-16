using Assets.Scripts.ActionBuilder.BuilderComponents;
using Assets.Scripts.Actions.Attacks;
using Assets.Scripts.Units;
using Elebris.Actions.Library.Actions.Component;
using Elebris.Actions.Library.Actions.Core;
using Elebris.Core.Library.CharacterValues.Mutable;
using Elebris.Core.Library.Enums;
using Elebris.Core.Library.Enums.Tags;
using Elebris.Rpg.Library.Actions.ActionValues;
using Elebris.Rpg.Library.Actions.Core;
using Elebris.Rpg.Library.CharacterValues;
using Elebris.UnitCreation.Library.StatGeneration;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/DamageAction")]
public class DamageActionBuilder : BaseActionBuilder
{
    public ActionTargetType damageType;
    public ActionDamageType mainType;
    public ActionSubtype subType;
    public Elements element;
    public float baseCritChance = 5f;

    public float flat = 100;
    public float scale = 50;
    public Stats scaleStat = Stats.PhysicalDamage; //attribute? stat enum?)


    //set cooldown
    //set resourcecosts


    //add in use, travel and hit FX
    //add in components (with values calculated from passed in users stats

   
    //conceptually, these are good methods, but the implementation of retrieving stats is sloppy and verbose, needs to be smoother
    public override HotbarActionBehaviour ReturnHotbarBehaviour(CharacterValueContainer container)
    {
        HotbarActionBehaviour behaviour = new HotbarActionBehaviour();
        behaviour.baseChargeTime = ModifedValue( _hotBarBehaviour.baseChargeTime, Stats.ChargeReduction.ToString(), container).TotalValue;

        behaviour.actionLock = ModifedValue( _hotBarBehaviour.actionLock, Stats.ActionLockScale.ToString(), container).TotalValue;

        behaviour.actionCooldown = ModifedValue( _hotBarBehaviour.actionCooldown, Stats.CurrentCooldownReduction.ToString(), container).TotalValue;
        behaviour.canCharge = _hotBarBehaviour.canCharge;
        behaviour.chargeMoveSpeed = ModifedValue( _hotBarBehaviour.chargeMoveSpeed, Stats.ChargeMoveSpeedIncrease.ToString(), container).TotalValue;
         
        behaviour.ResourceCosts = _hotBarBehaviour.ResourceCosts;
        foreach (var item in behaviour.ResourceCosts)
        {
            item.Amount = ModifedValue( item.Amount, Stats.CostReduction.ToString(), container).TotalValue;
        }
        return behaviour;

    }
    public override PreparedAction ReturnPreparedAction(CharacterValueContainer container)
    {
        float actionDuration = ModifedValue( _preparedbehaviour.actionDuration, Stats.SkillDuration.ToString(), container).TotalValue;
        float actionSpeed = ModifedValue( _preparedbehaviour.actionSpeed, Stats.ProjectileSpeed.ToString(), container).TotalValue;
        NumAffectedTargets numTargets = _preparedbehaviour.targetsHitToDestroy;
        PreparedActionBehaviour behaviour = new PreparedActionBehaviour(actionDuration, actionSpeed, numTargets);
        PreparedAction action = new PreparedAction(behaviour);
        DamageAction dam= CreateDamageAction();
        action.AddAction(dam);
        return action;
        
    }

    private DamageAction CreateDamageAction()
    {
        return new DamageAction(ReturnDamageScale(), mainType, subType, element, baseCritChance);
    }

    private ActionScaleModel ReturnDamageScale()
    {
        return new ActionScaleModel("Damage", scaleStat.ToString(), flat, scale);
    }
    //check that the skill ios an allowed type, otherwise cancel this and set to null?
    //add cooldown
    //add resourcecosts
    //


    //pass over behaviour


    //add in use, travel and hit FX
    //add in components (with values calculated from passed in users stats

}

