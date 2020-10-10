using Assets.Scripts.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Container/Actions")]
public class ActionContainerScriptableObject : ScriptableObject
{
    public AttackScriptableObject lightAttack;
    public AttackScriptableObject heavyAttack;

    public SkillScriptableObject skillOne;
    public SkillScriptableObject skillTwo;
    public SkillScriptableObject skillThree;
    public SkillScriptableObject skillFour;
}
