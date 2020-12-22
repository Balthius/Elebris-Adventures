using Assets.Scripts.Units;
using Elebris.Core.Library.CharacterValues.Mutable;
using Elebris.UnitCreation.Library.StatGeneration;
using UnityEngine;

public class UIHeroResourceController : UIBaseController
{
    //reference to the resources this is the parent of for initialization
    [SerializeField] private UIResource _healthResource = null;
    [SerializeField] private UIResource _spiritResource = null;
    [SerializeField] private UIResource _staminaResource = null;
    [SerializeField] private UIResource _manaResource = null;
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
        _healthResource.Initialize(canvas._player.uiManager.RetrieveResourceBar(ResourceStats.HealthResource));
        _spiritResource.Initialize(canvas._player.uiManager.RetrieveResourceBar(ResourceStats.SpiritResource));
        _staminaResource.Initialize(canvas._player.uiManager.RetrieveResourceBar(ResourceStats.StaminaResource));
        _manaResource.Initialize(canvas._player.uiManager.RetrieveResourceBar(ResourceStats.ManaResource));
    }

}
