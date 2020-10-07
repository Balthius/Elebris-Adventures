using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeybindManager : MonoBehaviour
{
    private static KeybindManager instance;



    public static KeybindManager MyInstance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<KeybindManager>();
            }
            return instance;
        }
    }

    public Dictionary<string, KeyCode> Keybinds { get; private set; }

    public Dictionary<string, KeyCode> ActionBinds { get; private set; }

    private string bindName;
	// Use this for initialization
	void Start () {
        Keybinds = new Dictionary<string, KeyCode>();
        ActionBinds = new Dictionary<string, KeyCode>();


        BindKey("UP", KeyCode.UpArrow);
        BindKey("LEFT", KeyCode.LeftArrow);
        BindKey("DOWN", KeyCode.DownArrow);
        BindKey("RIGHT", KeyCode.RightArrow);

        BindKey("ACTLIGHTATK", KeyCode.Z);
        BindKey("ACTHEAVYATK", KeyCode.X);
        BindKey("ACTSKILL1", KeyCode.A);
        BindKey("ACTSKILL2", KeyCode.S);
    }
	
	public void BindKey(string key, KeyCode keyBind)
    {
        Dictionary<string, KeyCode> currentDictionary = Keybinds;

        if(key.Contains("ACT"))
        {
            currentDictionary = ActionBinds;
        }

        if(!currentDictionary.ContainsValue(keyBind))
        {
            currentDictionary.Add(key, keyBind);
            UIManager.MyInstance.UpdateKeyText(key,keyBind);
        }
        else if(currentDictionary.ContainsValue(keyBind))
        {
            string myKey = currentDictionary.FirstOrDefault(x => x.Value == keyBind).Key;

            currentDictionary[myKey] = KeyCode.None;
            UIManager.MyInstance.UpdateKeyText(key, KeyCode.None);
        }

        currentDictionary[key] = keyBind;
        UIManager.MyInstance.UpdateKeyText(key, keyBind);
        bindName = string.Empty;
    }
}
