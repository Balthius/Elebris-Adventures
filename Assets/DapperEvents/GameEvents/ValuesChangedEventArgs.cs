
using Elebris.Library.Units;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.GameEvents
{
    public class ValuesChangedEventArgs : EventArgs
    {
        public List<AttributeObject> CharacterAttributes { get; set; }
        public List<StatObject> CharacterStats { get; set; }

    }
}