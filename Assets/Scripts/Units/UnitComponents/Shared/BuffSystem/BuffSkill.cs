using Assets.Scripts.Units;
using Elebris.Rpg.Library.Utils;
using System.Text;
using UnityEngine;

public abstract class BuffSkill : BaseActionBuilder
{
    public float buffTime =60;
    [Tooltip("Some buffs should remain after death, e.g. exp scrolls.")]
    public bool remainAfterDeath;
    public BuffSkillEffect effect;
    public Sprite image;

    // helper function to spawn the skill effect on someone
    // (used by all the buff implementations and to load them after saving)
    public void SpawnEffect(Entity caster, Entity spawnTarget)
    {
        if (effect != null)
        {
            GameObject go = Instantiate(effect.gameObject, spawnTarget.transform.position, Quaternion.identity);
            BuffSkillEffect effectComponent = go.GetComponent<BuffSkillEffect>();// tie this back in to my effect system?
            effectComponent.caster = caster;
            effectComponent.target = spawnTarget;
            effectComponent.buffName = name;
        }
    }

    // tooltip
    public string ToolTip()
    {
        StringBuilder tip = new StringBuilder();

        //add relevant data?
        tip.Replace("{BUFFTIME}", InterfaceUtils.PrettySeconds(buffTime));
        return tip.ToString();
    }
}
