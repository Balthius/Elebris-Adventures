using Assets.DapperEvents.GameEvents;
using Assets.Scripts.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMasterController : MonoBehaviour
{
    //set all values from this centralized class attached to the canvas
    public Hero _player;

    public List<UIBaseController> uIBaseControllers;

    public void UpdateActivePlayer(Hero player)
    {
        //connected up to an "activeplayer" dapperevent
        _player = player;
        TestSkillSet();
        foreach (var controller in uIBaseControllers)
        {
            controller.OnPlayerUpdated();
        }
    }
    public void Subscribe(UIBaseController controller)
    {
        uIBaseControllers.Add(controller);
    }
    public void EmptySubscriptions()
    {
        uIBaseControllers = new List<UIBaseController>();
    }

    /// <summary>
    /// Below is purely for testing
    /// </summary>

    [SerializeField] ActionContainerScriptableObject container;
    public void TestSkillSet()
    {
        _player.AttackContainer.LightAttack = container.lightAttack;
        _player.AttackContainer.HeavyAttack = container.heavyAttack;
        _player.SkillContainer.SkillOne = container.skillOne;
        _player.SkillContainer.SkillTwo = container.skillTwo;
        _player.SkillContainer.SkillThree = container.skillThree;
        _player.SkillContainer.SkillFour = container.skillFour;
    }

}
