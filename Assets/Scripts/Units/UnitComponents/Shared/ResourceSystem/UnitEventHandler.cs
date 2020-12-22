using Elebris.UnitCreation.Library.StatGeneration;
using System;
using UnityEngine;

namespace Assets.Scripts.Units
{
    public class UnitEventHandler
    {

        public UnitData _unitData;
        public event Action<Transform, float> OnHealthChanged;

        public UnitEventHandler(UnitData unit)
        {
            _unitData = unit; //keeps the parent consistent
            PopulateEvents();
        }

        public void PopulateEvents()
        {
            _unitData.ValueContainer.DataHandler.PairEvents();
        }
    }

}