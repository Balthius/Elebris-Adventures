using Assets.Scripts.UI;
using Assets.Scripts.Units;
using Elebris.Core.Library.CharacterValues.Mutable;
using Elebris.Core.Library.Components;
using Elebris.UnitCreation.Library.StatGeneration;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInteractionManager : MonoBehaviour
{

    public ConstantPopupManager popupManager = null;
    protected IUnitController unitController;


    UnitData unitData;
    public IUnitController UnitController { get => unitController; set => unitController = value; }
    private void Awake()
    {
        UnitController = GetComponent<IUnitController>();
        unitData = GetComponent<UnitData>();
    }

    public ResourceBarValue RetrieveResourceBar(ResourceStats stat)
    {
        return unitData.ValueContainer.DataHandler.RetrieveResourceData(stat);
    }

    public BaseActionData RetrieveActionData(BindableActions selectedAction)
    {
        return unitData.ActionContainer.CharacterActions[selectedAction].Data;
    }

}
