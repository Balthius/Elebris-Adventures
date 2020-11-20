using Assets.Scripts.Units;
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



}
