using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour {


    private static UIManager instance;



    public static UIManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }
            return instance;
        }
    }
    //unity rpg 3.4
    [SerializeField]
	private ActionButton[] actionButtons;

	private KeyCode action1, action2, action3, action4;

    [SerializeField]
    private CanvasGroup keybindMenu;

    private GameObject[] keybindButtons;

    private void Awake()
    {
        keybindButtons = GameObject.FindGameObjectsWithTag("Keybind");
    }

    // Use this for initialization
    void Start () 
	{

        SetUseable(actionButtons[0], SkillBook.MyInstance.GetSkill("Knife"));
        SetUseable(actionButtons[1], SkillBook.MyInstance.GetSkill("Star"));

    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (action1))
		{
			
		}
		if (Input.GetKeyDown (action2))
		{
			
		}
		if (Input.GetKeyDown (action3))
		{
		
		}
		if (Input.GetKeyDown (action4))
		{
			
		}

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OpenCloseMenu();
        }
	}

	
    public void OpenCloseMenu()
    {
        // X = X > Y ? Y : Z    reads as set X to the result of this inline if statement. For if = true, return Y, if = false, return Z
        keybindMenu.alpha = keybindMenu.alpha > 0 ? 0 : 1;
        keybindMenu.blocksRaycasts = keybindMenu.blocksRaycasts == true ? false : true;

        Time.timeScale = Time.timeScale > 0 ? 0 : 1;
    }

    public void UpdateKeyText(string key, KeyCode code)
    {
        Text tmp = Array.Find(keybindButtons, x => x.name == key).GetComponentInChildren<Text>();
        tmp.text = code.ToString();
    }

    public void PressActionButton(string buttonName)
    {
      //  Array.Find(actionButtons, x=> x.gameObject.name == buttonName).MyButton.onClick.Invoke();
    }

    public void SetUseable(ActionButton btn, IUseable useable)
    {
        btn.MyButton.image.sprite = useable.MyIcon;
        btn.MyButton.image.color = Color.white;
        btn.MyUseable = useable;
    }
}
 