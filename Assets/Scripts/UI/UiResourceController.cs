using Assets.Scripts.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIResourceController : UIBaseController
{
    //reference to the resources this is the parent of for initialization
    [SerializeField] private UIResource _healthResource;
    [SerializeField] private UIResource _spiritResource;
    [SerializeField] private UIResource _staminaResource;
    [SerializeField] private UIResource _manaResource;
    //all resources are updated from here
    // Start is called before the first frame update

    public override void Start()
    {
        base.Start();
    }
    public override void OnPlayerUpdated()
    {
        SetResourceValues();
    }
    private void SetResourceValues()
    {
        //pass in the value you want the ui elements to be tracking
        _healthResource.Initialize(canvas._player.ResourceContainer.HealthValue);
        _spiritResource.Initialize(canvas._player.ResourceContainer.SpiritValue);
        _staminaResource.Initialize(canvas._player.ResourceContainer.StaminaValue);
        _manaResource.Initialize(canvas._player.ResourceContainer.ManaValue);
    }
}
