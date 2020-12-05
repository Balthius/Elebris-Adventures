using Elebris.Actions.Library.Actions.Component;
using Elebris.Actions.Library.Actions.Core;
using Elebris.Core.Library.Enums;
using Elebris.Core.Library.Enums.Tags;
using Elebris.Rpg.Library.Actions.ActionValues;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/DamageAction")]
public class DamageActionBuilder : ActionBuilder
{
    
    public ResourceCost[] costs;
    public ActionTargetType damageType;
    public ActionDamageType mainType;
    public ActionSubtype subType;
    public Elements element;
    public float baseCritChance;

    public float flat;
    public float scale;
    public string scaleStat; //attribute? stat enum?)

    public StatScaleModel ReturnDamageScale()
    {
        return new ActionBehaviourValueCalculator("Damage", scaleStat, flat, scale);
    }

    public ICoreAction CreateAction()
    {
        return new DamageAction(ReturnDamageScale(), mainType, subType, element, baseCritChance);
    }
}

