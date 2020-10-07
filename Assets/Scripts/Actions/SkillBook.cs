using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SkillBook : MonoBehaviour {
    private static SkillBook instance;



    public static SkillBook MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SkillBook>();
            }
            return instance;
        }
    }

    [SerializeField]
	private Skill[] skills;

	private Coroutine skillRoutineOne;



	public Skill CastSkill(string skillName)
	{

        Skill skill = Array.Find(skills, x => x.MyName == skillName);


        return skill;
	}

    public Skill GetSkill(string skillName)
    {
        Skill skill = Array.Find(skills, x => x.MyName == skillName);
        return skill;
    }


}
