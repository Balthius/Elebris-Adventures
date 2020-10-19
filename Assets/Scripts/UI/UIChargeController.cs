using Assets.Scripts.Units;
using UnityEngine;

public class UIChargeController : UIBaseController
{

    //reference to the resources this is the parent of for initialization
    [SerializeField] private UIUnitBar _chargeBar;
    [SerializeField] private UIUnitText _chargeText;
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
        _chargeBar.Initialize(canvas._player.chargeAmount); //this is supposed to use chargeTime but I'm workin on other features right now
        _chargeText.Initialize(canvas._player.chargeAmount);
    }
}
