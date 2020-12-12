using Assets.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
[Serializable]
public partial struct Buff 
{

    // hashcode used to reference the real ScriptableSkill (can't link to data
    // directly because synclist only supports simple types). and syncing a
    // string's hashcode instead of the string takes WAY less bandwidth.
    public int hash;

    public double buffTimeEnd; // server time. double for long term precision.
    // constructors
    public Buff(BuffSkill data, float time)
    {

        hash = data.name.GetStableHashCode();
        buffTimeEnd = Time.deltaTime + time;
    }

    // wrappers for easier access
    public BuffSkill data
    {
        get
        {
            // show a useful error message if the key can't be found
            // note: ScriptableSkill.OnValidate 'is in resource folder' check
            //       causes Unity SendMessage warnings and false positives.
            //       this solution is a lot better.
            if (!BaseActionBuilder.dict.ContainsKey(hash))
                throw new KeyNotFoundException("There is no ScriptableSkill with hash=" + hash + ". Make sure that all ScriptableSkills are in the Resources folder so they are loaded properly.");
            return (BuffSkill)BaseActionBuilder.dict[hash];
        }
    }
    public string name => data.name;
    public Sprite image => data.image;
    public float buffTime => data.buffTime;
    public bool remainAfterDeath => data.remainAfterDeath;
   
 
    // tooltip - runtime part
    public string ToolTip()
    {
        // we use a StringBuilder so that addons can modify tooltips later too
        // ('string' itself can't be passed as a mutable object)
        StringBuilder tip = new StringBuilder(data.ToolTip());

        

        return tip.ToString();
    }

    public float BuffTimeRemaining()
    {
        // how much time remaining until the buff ends? (using server time)
        return Time.deltaTime >= buffTimeEnd ? 0 : (float)(buffTimeEnd - Time.deltaTime);
    }
}
