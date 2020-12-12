using Elebris.Rpg.Library.Utils;
using System;
using System.Text;
using UnityEngine;

[Serializable]
public class BaseActionData
{
    [Header("Info")]
    public string name; // Name used for display purposes for the GUI
                               // We don't need an identifier field, because this will actually be stored
                               // as a file on disk and thus implicitly have its own identifier string.

    public Sprite ActionIcon; //UI Icon
  
    public bool learnDefault; // normal attack etc.
    public bool showCastBar;
    [Header("Fx")]
    [SerializeField] private GameObject obj; //the base gameobject, which mostly controls the collider size/shape and perhaps some base functionality

    [Header("Requirements")]
    public string requiredWeaponCategory = ""; // "" = no weapon needed; "Weapon" = requires a weapon, "WeaponSword" = requires a sword weapon, etc.
    
    public ActionType permittedSlot; //Slot this action is permitted to be bound to

    public BaseActionData(string displayName, Sprite actionIcon, ActionType actionType, GameObject obj)
    {
        this.name = displayName;
        ActionIcon = actionIcon;
        this.permittedSlot = actionType;
        this.obj = obj;
    }



    public GameObject Obj { get => obj; set => obj = value; }
}



