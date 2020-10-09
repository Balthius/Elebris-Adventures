using UnityEngine;
using UnityEngine.Events; 
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Units;

namespace Assets.DapperEvents.GameEvents {
[CreateAssetMenu(fileName = "NewActiveUnitEvent", menuName = "Game Events /ActiveUnitEvent")]
public class ActiveUnitEvent : BaseGameEvent <Unit> {
}
}
