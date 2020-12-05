using Assets.Scripts.Units;
using Elebris.Core.Library.CharacterValues.Mutable;
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
        _healthResource.Initialize(canvas._player.container.StoredResourceBars[Elebris.UnitCreation.Library.StatGeneration.Resources.HealthResource]);
        _spiritResource.Initialize(canvas._player.container.StoredResourceBars[Elebris.UnitCreation.Library.StatGeneration.Resources.SpiritResource]);
        _staminaResource.Initialize(canvas._player.container.StoredResourceBars[Elebris.UnitCreation.Library.StatGeneration.Resources.StaminaResource]);
        _manaResource.Initialize(canvas._player.container.StoredResourceBars[Elebris.UnitCreation.Library.StatGeneration.Resources.ManaResource]);
    }
}
