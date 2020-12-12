using UnityEngine;
using UnityEngine.Events; 
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.UI;

namespace Assets.DapperEvents.GameEvents
{
[System.Serializable]
public class UnityTextPopupEvent : UnityEvent<TextBundle>
    {
    }
}
