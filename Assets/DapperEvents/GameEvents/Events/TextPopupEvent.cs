using UnityEngine;
using UnityEngine.Events; 
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.UI;

namespace Assets.DapperEvents.GameEvents
{
[CreateAssetMenu(fileName = "NewTextPopupEvent", menuName = "Game Events /TextPopupEvent")]
public class TextPopupEvent : BaseGameEvent <TextBundle>
    {
    }
}
