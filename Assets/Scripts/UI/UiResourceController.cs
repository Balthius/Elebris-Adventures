using Assets.DapperEvents.GameEvents;
using Assets.Scripts.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiResourceController : MonoBehaviour
{
    //set all values from this centralized class attached to the canvas
    [SerializeField] private UiResource _healthResource;
    [SerializeField] private DevelopmentPanel developmentPanel;
     private Unit _player;

    public void UpdateActivePlayer(Unit player)
    {
        //connected up to an "activeplayer" dapperevent
        _player = player;
        developmentPanel.Initialize(_player);
        SetHealthValues();
    }
    private void SetHealthValues()
    {
        float currentHP = _player.ResourceContainer.HealthValue.currentValue;
        float maxHP = _player.ResourceContainer.HealthValue.maxValue;
        _healthResource.Initialize(currentHP, maxHP);
    }


    // Update is called once per frame
    void Update()
    {
        if (_player == null) return;
        _healthResource.MyCurrentValue = _player.ResourceContainer.HealthValue.currentValue;
    }
}
