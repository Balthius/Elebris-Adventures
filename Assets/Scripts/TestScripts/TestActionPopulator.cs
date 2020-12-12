using Assets.Scripts.Units;
using Elebris.Core.Library.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestActionPopulator : MonoBehaviour
{

    [SerializeField] DamageActionBuilder builder;
    private void Start()
    {
        UnitData data = GetComponentInParent<UnitData>();

        data.ActionContainer.CharacterActions.Add(BindableActions.LightAttack, new HotbarBoundAction());
    }
}
