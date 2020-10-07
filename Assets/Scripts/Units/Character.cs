using Assets.Scripts.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public abstract class Character : MonoBehaviour
{

	[SerializeField]
	protected float speed;

	public float Speed
    {
		get {
			return speed;
		}
		set {
			speed = value;
		}
	}

	[SerializeField]
	protected Transform target;
	public Transform MyTarget
    {
		get {
			return target;
		}
		set {
			target = value;
		}
	}

	protected float baseSpeed;
	protected float empoweredSpeedModifier = .4f;

	protected float radialBar = 3f;
	protected float straightBar = 1f;

	[SerializeField]
	protected ResourceUIValue health;
	[SerializeField]
	private float initHealth;

    public ResourceUIValue MyHealth // Did this ever show up in Vids?
    {
        get { return health; }
    }

    private Rigidbody2D myRigidBody;

	private Animator anim;

	public Animator Anim
    {
		get {
			return anim;
		}
		set {
			anim = value;
		}
	}

	protected Vector2 direction;

	public Vector2 Direction
    {
		get {
			return direction;
		}
		set {
			direction = value;
		}
	}

	private Vector2 directionFacing = new Vector2(0,-1);

	public Vector2 DirectionFacing
    {
		get {
			return directionFacing;
		}
		set {
			directionFacing = value;
		}
	}

	private bool isAttackingSkill = false;

	public bool IsAttackingSkill
    {
		get {
			return isAttackingSkill;
		}
		set {
			isAttackingSkill = value;
		}
	}

	private bool isAttacking = false;

	public bool IsAttacking
    {
		get {
			return isAttacking;
		}
		set {
			isAttacking = value;
		}
	}

	private bool isEmpowering = false;

	public bool IsEmpowering
    {
		get {
			return isEmpowering;
		}
		set {
			isEmpowering = value;
		}
	}

	private bool isAlive = true;

	public bool IsAlive
    {
		get {
			return health.MyCurrentValue > 0;
		}

	}
 

	protected Coroutine attackRoutine;
	protected Coroutine skillAttackRoutine;

	[SerializeField]
	protected Transform hitBox;

	public bool IsMoving
	{
		get
		{
			return direction.x != 0 || direction.y != 0;
		}
	}

	protected virtual void Start () {

		health.Initialize (initHealth, initHealth, straightBar);
		Anim = GetComponent <Animator> ();
		myRigidBody = GetComponent <Rigidbody2D> ();
		ChangeSprite ();
	}

	protected  virtual void Update ()
	{

		HandleLayers ();
	}

	private void FixedUpdate()
	{
		if (!isAttacking && !isAttackingSkill) 
		{
			Move ();
		}
		if (isAttacking || isAttackingSkill)
		{
			myRigidBody.velocity = Vector2.zero;
		}
	}

	public void Move()
	{
		myRigidBody.velocity = direction.normalized * speed;
	}

	public void HandleLayers()
	{
		if(IsAlive)
		{
			if (IsAttackingSkill)
			{

				ActivateLayer ("SkillLayer");
			}
			else if (IsAttacking)
			{

				ActivateLayer ("AttackLayer");
			}
			else if (IsEmpowering)
			{
				ActivateLayer ("EmpowerLayer");
			}
			else if (IsMoving)
			{
				ActivateLayer ("WalkLayer");
				ChangeSprite ();
			}
			else
			{
				ActivateLayer ("IdleLayer");
			}
		}

		else
		{
			ActivateLayer ("DeathLayer");
		}


	}

	void ChangeSprite ()
	{
		Anim.SetFloat ("x", directionFacing.x);
		Anim.SetFloat ("y", directionFacing.y);
	}
		
	public void ActivateLayer(string layerName)
	{
		for (int i = 0; i < Anim.layerCount; i++)
		{
			Anim.SetLayerWeight (i,0);
		}
		Anim.SetLayerWeight (Anim.GetLayerIndex (layerName), 1);
	}


	public virtual void TakeDamage(float damage, Transform source)
	{
		health.MyCurrentValue -= damage;

	    //source not currently used

		//Code will not drop int below 0, unsure why
		if (health.MyCurrentValue <= 0)
		{
			Direction = Vector2.zero;
			myRigidBody.velocity = Direction;
			Anim.SetTrigger ("die");
		}
	}

	public void Death()
	{
		Destroy(gameObject);
	}

}
