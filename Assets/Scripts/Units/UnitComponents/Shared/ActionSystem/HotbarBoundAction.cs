using Assets.Helpers;
using Assets.Scripts.Actions.Attacks;
using Assets.Scripts.Units;
using Elebris.Core.Library.CharacterValues.Mutable;
using Elebris.Rpg.Library.CharacterValues;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class HotbarBoundAction
{ 

    private int hash;
    public ResourceBarValue cooldown { get; set; }
    public HotbarBoundAction(BaseActionBuilder data)
    {
        hash = data.name.GetStableHashCode();

    }
    // wrappers for easier access
    public BaseActionData Data
    {
        get
        {

            //cache it?
            return builder.ReturnActionData();
        }
    }
    public HotbarActionBehaviour Behaviour(CharacterValueContainer container)
    {
        //cache it?
        return builder.ReturnHotbarBehaviour(container);
    }
    public PreparedAction Action(CharacterValueContainer container)
    {

        //cache it?
        return builder.ReturnPreparedAction(container);
    }
    public BaseActionBuilder builder
    {
        get
        {
            // show a useful error message if the key can't be found
            // note: ActionBuilder.OnValidate 'is in resource folder' check
            //       causes Unity SendMessage warnings and false positives.
            //       this solution is a lot better.
            if (!BaseActionBuilder.dict.ContainsKey(hash))
                throw new KeyNotFoundException("There is no ActionBuilder with hash=" + hash + ". Make sure that all ActionBuilder are in the Resources folder so they are loaded properly.");
            return BaseActionBuilder.dict[hash];
        }
    }
    public bool CanCast(CharacterValueContainer container)
    {
        if (cooldown.CurrentValue < cooldown.MaxValue) return false;
        builder.CanCast(container);
        return true;
    }

}