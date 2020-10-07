using Assets.Scripts.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : Character
{

    private static Player instance;



    public static Player MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Player>();
            }
            return instance;
        }
    }
    [SerializeField]
	private ResourceUIValue brawn;
	[SerializeField]
	private ResourceUIValue ability;
	[SerializeField]
	private ResourceUIValue dexterity;
	[SerializeField]
	private ResourceUIValue charge;




	private float initBrawn = 10;
	private float initAbility = 10;
	private float initDexterity = 10;

	private float initCharge = 50;
	private float maxCharge = 100;

	private float attackBegin;
	private float attackEnd;
	private float empowerTime = 1f;

	private float physDamageMod = 1f;

	public float skillDurationMod = 1f;
	public float skillSpeedMod = 1f;
	public Vector2 skillDirection;

	public bool altAttack;

	private bool onCoolDown = false;

	public bool empowered = false;

	private SkillBook skillBook;
	private AttackBook attackBook;

	[SerializeField]
	private GameObject weaponPrefab;
	[SerializeField]
	private GameObject center;


	private Vector3 min, max;
	protected override void Start ()
	{
		skillBook = GetComponent <SkillBook> ();
		attackBook = GetComponent <AttackBook> ();
		InitializeStats ();
		base.Start (); 
	}

	void InitializeStats ()
	{
		
		brawn.Initialize (initBrawn, initBrawn, radialBar);
		ability.Initialize (initAbility, initAbility, radialBar);
		dexterity.Initialize (initDexterity, initDexterity, radialBar);
		charge.Initialize (initCharge, maxCharge, straightBar);
	}

	protected override void Update ()
	{
		GetInput ();
		SetDirectionFacing ();

		transform.position = new Vector3 (Mathf.Clamp(transform.position.x, min.x,max.x), Mathf.Clamp (transform.position.y,min.y ,max.y),transform.position.z);
		base.Update ();
	}

	private void SetDirectionFacing()
	{
		if(IsMoving && !IsEmpowering)
		{
            DirectionFacing = Direction;
        }
	}
	private void GetInput()
	{
		direction = Vector2.zero;
		//float directionX = Input.GetAxisRaw ("Horizontal");
		//float directionY = Input.GetAxisRaw ("Vertical");     I need to learn more if I want to affect keybinds with this method
		//direction = new Vector2(directionX,directionY);
        if (Input.GetKey(KeybindManager.MyInstance.Keybinds["UP"]))
        {
            Direction += Vector2.up;
         
        }
        if (Input.GetKey(KeybindManager.MyInstance.Keybinds["DOWN"]))
        {
            Direction += Vector2.down;
           
        }
        if (Input.GetKey(KeybindManager.MyInstance.Keybinds["LEFT"]))
        {
            Direction += Vector2.left;
          
        }
        if (Input.GetKey(KeybindManager.MyInstance.Keybinds["RIGHT"]))
        {
            Direction += Vector2.right;
        }

        foreach(string action in KeybindManager.MyInstance.ActionBinds.Keys)
        {
            if(Input.GetKeyDown(KeybindManager.MyInstance.ActionBinds[action]))
            {
              //  UIManager.MyInstance
            }
        }


        if (Input.GetKeyDown(KeyCode.Z))
        {
            baseSpeed = speed;
            empowered = false;
            IsEmpowering = true;
            speed = speed * empoweredSpeedModifier;
            attackBegin = Time.time;
        }
        if (Input.GetKeyDown (KeyCode.X))
		{
			baseSpeed = speed;
			empowered = false;
			IsEmpowering = true;
			speed = speed * empoweredSpeedModifier;
			attackBegin = Time.time;
		}
		if(!IsAttackingSkill)
		{
			if(Input.GetKeyDown (KeyCode.A))
			{
				baseSpeed = speed;
				empowered = false;
				IsEmpowering = true;
				speed = speed * empoweredSpeedModifier;
				attackBegin = Time.time;
			}
			if(Input.GetKeyDown (KeyCode.S))
			{
				baseSpeed = speed;
				empowered = false;
				IsEmpowering = true;
				speed = speed * empoweredSpeedModifier;
				attackBegin = Time.time;
			}
		}
		else
		{
			Debug.Log ("Casting Skill");
		}

		//End Cast
		if(Input.GetKeyUp (KeyCode.Z))
		{
			IsEmpowering = false;
			speed = baseSpeed;
			attackEnd = Time.time;
			float chargeTime = attackEnd - attackBegin;
			StartAttack (0, chargeTime, empowerTime);
		}
		if(Input.GetKeyUp (KeyCode.X))
		{
			IsEmpowering = false;
			speed = baseSpeed;
			attackEnd = Time.time;
			float chargeTime = attackEnd - attackBegin;
			altAttack = true;
			StartAttack (1, chargeTime,empowerTime);
		}
		if(Input.GetKeyUp (KeyCode.A))
        {
            IsEmpowering = false;
            speed = baseSpeed;
            attackEnd = Time.time;
            float chargeTime = attackEnd - attackBegin;
            //CastSkill();
        }
        if (Input.GetKeyUp (KeyCode.S))
		{
			IsEmpowering = false;
			speed = baseSpeed;

            //CastSkill();
		}
	}

    private void CastSkill(string skillName)
    {
        if (!onCoolDown)
        {
            skillAttackRoutine = StartCoroutine(SkillAttack(skillName));//Rewire
            StartCoroutine("SkillCoolDownCount");
        }
    }

    public void SetLimits(Vector3 min, Vector3 max)
	{
		this.min = min;
		this.max = max;


	}
	public void StartAttack(int typeAttack, float chargeTime,float empoweredThreshold)
	{

		if(chargeTime > empoweredThreshold)
		{
			if (!IsAttacking && typeAttack == 0)
			{
				empowered = true;
				attackRoutine = StartCoroutine (WeaponAttack (0,DirectionFacing));
			}
			if (!IsAttacking && typeAttack == 1)
			{
				empowered = true;
				attackRoutine = StartCoroutine (WeaponAttack (1,DirectionFacing));
			}
		}
		else
		{
			if (!IsAttacking && typeAttack == 0)
			{
				attackRoutine = StartCoroutine (WeaponAttack (0,DirectionFacing));
			}
			if (!IsAttacking && typeAttack == 1)
			{
				attackRoutine = StartCoroutine (WeaponAttack (1,DirectionFacing));
			}
		}
	}
		
	public IEnumerator WeaponAttack(int attackIndex, Vector2 direction)
	{
		
		WeaponAttack newAttack = attackBook.UseAttack(attackIndex);
		IsAttacking = true;

		WeaponAttackScript melee = Instantiate (newAttack.MyAttackPrefab,center.transform.position,Quaternion.identity).GetComponent<WeaponAttackScript> ();
		melee.Initialize (newAttack.MyDamage, physDamageMod, newAttack.MyAnimationLength, altAttack, empowered, transform);
		melee.transform.parent = gameObject.transform;
		melee.transform.Translate ((direction/2.5f));

		Anim.SetBool ("attack", IsAttacking);
		yield return new WaitForSeconds (newAttack.MyAnimationLength);

		altAttack = false;
		StopAttack ();
	}
		
	public IEnumerator SkillAttack(string skillName)
	{

		Skill newSkill = skillBook.CastSkill(skillName);
		IsAttackingSkill = true;
		skillDirection = DirectionFacing;

		SkillScript projectile = Instantiate (newSkill.MySpellPrefab,transform.localPosition,Quaternion.identity).GetComponent<SkillScript> ();
		projectile.Initialize (newSkill.MyDamage, physDamageMod, newSkill.MySpeed ,skillSpeedMod ,newSkill.MyDuration ,skillDurationMod, skillDirection );
		projectile.transform.parent = gameObject.transform;

		Anim.SetBool ("skillattack", IsAttackingSkill);
		yield return new WaitForSeconds (newSkill.MyAnimationLength);

		StopAttack ();
	}

	private IEnumerator SkillCoolDownCount()
	{
		onCoolDown = true;
		yield return new WaitForSeconds (.3f);
		onCoolDown = false;
	}



	public void StopAttack()
	{
		if (attackRoutine != null)
		{
			StopCoroutine (attackRoutine);
		}
		if (skillAttackRoutine != null)
		{
			StopCoroutine (skillAttackRoutine); 
		}

		IsAttacking = false;
		IsAttackingSkill = false;

		Anim.SetBool ("attack", IsAttacking);
		Anim.SetBool ("skillattack", IsAttackingSkill);
	}

	//canvas group can be used to fade out skills/attacks from 22 minutes into unity rpg tutorial 4.1



}