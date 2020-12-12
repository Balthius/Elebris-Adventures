using Assets.Scripts.Units;
using Elebris.Core.Library.Components;
using UnityEngine;

public class UIInputIndicatorController : UIBaseController
{

    [SerializeField] InputIndicator lightAttack = null;
    [SerializeField] InputIndicator heavyAttack = null;
    [SerializeField] InputIndicator select = null;
    [SerializeField] InputIndicator maneuver = null;
    [SerializeField] InputIndicator skillOne = null;
    [SerializeField] InputIndicator skillTwo = null;
    [SerializeField] InputIndicator skillThree = null;
    [SerializeField] InputIndicator skillFour = null;

    [SerializeField] Sprite DefaultSprite;
    private IUnitController unitController = null;
    private CharacterActionContainer container = null;

    public override void Start()
    {
        base.Start();
    }
    public override void OnPlayerUpdated()
    {
        container = canvas._player.unitData.ActionContainer;
        unitController = canvas._player.UnitController;
        //Debug.Log("Player Updated");
        //update images of skills, and the state a skill is in
        
        lightAttack.SetImage(GetSpriteFromBoundAction(BindableActions.LightAttack));
        heavyAttack.SetImage(GetSpriteFromBoundAction(BindableActions.HeavyAttack));
        //select.SetImage(canvas._player.XXXX.Select.actionIcon);
        //maneuver.SetImage(canvas._player.XXXX.Select.actionIcon);
        skillOne.SetImage(GetSpriteFromBoundAction(BindableActions.SkillOne));
        skillTwo.SetImage(GetSpriteFromBoundAction(BindableActions.SkillTwo));
        skillThree.SetImage(GetSpriteFromBoundAction(BindableActions.SkillThree));

        skillFour.SetImage(GetSpriteFromBoundAction(BindableActions.SkillFour));
    }
    public Sprite GetSpriteFromBoundAction(BindableActions action)
    {
        if(container.CharacterActions[action] == null)
        {
            return DefaultSprite;
        }
        //and an else that returns an "empty" icon if image is null, action is null etc
        return container.CharacterActions[action].data.ActionIcon;
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
