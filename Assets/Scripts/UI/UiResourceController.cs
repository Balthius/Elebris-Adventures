using Assets.Scripts.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIResourceController : UIBaseController
{
    //reference to the resources this is the parent of for initialization
    [SerializeField] private UIResource _healthResource;
    //all resources are updated from here
    // Start is called before the first frame update



    public override void Start()
    {
        base.Start();
    }
    public override void OnPlayerUpdated()
    {
        SetHealthValues();
    }
    private void SetHealthValues()
    {
        _healthResource.Initialize(canvas._player.ResourceContainer.HealthValue);
    }
}
