using Assets.Scripts.Units;
using UnityEngine;

public class UIInputIndicatorController : UIBaseController
{

    [SerializeField] InputIndicator lightAttack;
    [SerializeField] InputIndicator heavyAttack;
    [SerializeField] InputIndicator select;
    [SerializeField] InputIndicator maneuver;
    [SerializeField] InputIndicator skillOne;
    [SerializeField] InputIndicator skillTwo;
    [SerializeField] InputIndicator skillThree;
    [SerializeField] InputIndicator skillFour;


    private IUnitController unitController;


    public override void Start()
    {
        base.Start();
    }
    public override void OnPlayerUpdated()
    {
        //Debug.Log("Player Updated");
        unitController = canvas._player.UnitController;
        //update images of skills, and the state a skill is in
        UnityActionContainer container = canvas._player.ActionContainer;
        lightAttack.SetImage(container.LightAttack.actionBehaviour.ActionIcon);
        heavyAttack.SetImage(container.HeavyAttack.actionBehaviour.ActionIcon);
        //select.SetImage(canvas._player.XXXX.Select.actionIcon);
        //maneuver.SetImage(canvas._player.XXXX.Select.actionIcon);
        skillOne.SetImage(container.SkillOne.actionBehaviour.ActionIcon);
        skillTwo.SetImage(container.SkillTwo.actionBehaviour.ActionIcon);
        skillThree.SetImage(container.SkillThree.actionBehaviour.ActionIcon);

        skillFour.SetImage(container.SkillFour.actionBehaviour.ActionIcon);
    }

    private void FixedUpdate()
    {
        //to properly mirror the resource bar I should really either change that to update like this, or find a workaround to add an initialize to this mirroring that,
        //but i'm not sure how I'd reference a bool-returning property
        if (unitController == null) return;
        lightAttack.AlterImageState(unitController.ChargingLightAttack);
        heavyAttack.AlterImageState(unitController.ChargingHeavyAttack);
        select.AlterImageState(unitController.ChargingSelect);
        maneuver.AlterImageState(unitController.ChargingManeuver);
        skillOne.AlterImageState(unitController.ChargingSkillOne);
        skillTwo.AlterImageState(unitController.ChargingSkillTwo);
        skillThree.AlterImageState(unitController.ChargingSkillThree);
        skillFour.AlterImageState(unitController.ChargingSkillFour);
    }

}
