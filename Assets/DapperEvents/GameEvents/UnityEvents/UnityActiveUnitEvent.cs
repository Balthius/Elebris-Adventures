using UnityEngine;
using UnityEngine.Events; 
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Units;

namespace Assets.DapperEvents.GameEvents {
[System.Serializable]
public class UnityActiveUnitEvent : UnityEvent<Unit> {
}
}
