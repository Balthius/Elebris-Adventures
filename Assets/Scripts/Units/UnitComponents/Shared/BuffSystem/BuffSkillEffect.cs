using Assets.Scripts.Buffs;
using UnityEngine;

public class BuffSkillEffect : SkillEffect
{
    float lastRemainingTime = Mathf.Infinity;
    [HideInInspector] public string buffName;

    void Update()
    {
        // only while target still exists, buff still active and hasn't been
        // recasted
        if (target != null)
        {
            int index = target.buffStatusManager.GetBuffIndexByName(buffName);
            if (index != -1)
            {
                Buff buff = target.buffStatusManager.Buffs[index];
                if (lastRemainingTime >= buff.BuffTimeRemaining())
                {
                    transform.position = target.transform.position;
                    lastRemainingTime = buff.BuffTimeRemaining();
                    return;
                }
            }
        }

        //// if we got here then something wasn't good, let's destroy self
        //if (isServer) NetworkServer.Destroy(gameObject);
    }
}