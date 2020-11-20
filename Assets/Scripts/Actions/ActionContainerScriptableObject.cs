using Assets.Scripts.Units;
using UnityEngine;


[CreateAssetMenu(menuName = "Container/Actions")]
public class ActionContainerScriptableObject : ScriptableObject
{
    public ActionScriptableObject lightAttack;
    public ActionScriptableObject heavyAttack;

    public ActionScriptableObject skillOne;
    public ActionScriptableObject skillTwo;
    public ActionScriptableObject skillThree;
    public ActionScriptableObject skillFour;
}
