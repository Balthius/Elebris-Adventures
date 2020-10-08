using Assets.Scripts.AI.EnemyStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : NPC
{
	[SerializeField]
	private CanvasGroup healthGroup;
	[SerializeField]
	private GameObject hit;

    [SerializeField]
    private float initAggroRange;

    public float MyAggroRange { get; set; }

    public bool InRange
    {
        get
        {
            return Vector2.Distance(transform.position, MyTarget.position) < MyAggroRange;
        }
    }

	private float myAttackTime;
	public float MyAttackTime {
		get {
			return myAttackTime;
		}
		set {
			myAttackTime = value;
		}
	}

    public Vector3 MyStartPosition { get; set; }

	[SerializeField]
	private float myAttackRange;
	public float MyAttackRange {
		get {
			return myAttackRange;
		}
		set {
			myAttackRange = value;
		}
	}

	private IState currentState;


	protected void Awake()
	{
        MyStartPosition = transform.position;
        MyAggroRange = initAggroRange;
		ChangeState (new IdleState()); 
	}


	protected override void Update ()
	{
		if(IsAlive)
		{
			if (!IsAttacking && !IsAttackingSkill)
			{
				MyAttackTime += Time.deltaTime;
			}
			currentState.Update ();
		}
		base.Update ();
        Debug.Log(MyTarget);
    }

	public override Transform Select()
	{
		healthGroup.alpha = 1;
		return base.Select ();
	}

	public override void DeSelect()
	{
		healthGroup.alpha = 0;
		base.DeSelect ();
	}

	private void OnTriggerEnter2D(Collider2D col)
	{

        if(col.tag == "Skill")
        {
            Select();
            Instantiate(hit, col.transform.position, Quaternion.identity);//set pivot point to center on enemies
        }
		
	}

    public override void TakeDamage(float damage, Transform source)
    {

        if (!(currentState is EvadeState))
        {
            SetTarget(source);
            base.TakeDamage(damage, source);

            OnHealthChanged(health.MyCurrentValue);//Has to do with enemy unit frame video 5.2
        }
        
    }

    public void ChangeState(IState newState)
	{
		if(currentState != null)
		{
			currentState.Exit ();
		}

		currentState = newState;

		currentState.Enter (this);
        Debug.Log(currentState);
	}

    public void SetTarget(Transform target)
    {
        if (MyTarget == null && !(currentState is EvadeState))
        {
            float distance = Vector2.Distance(transform.position, target.position);
            MyAggroRange = initAggroRange; // reset aggro range to init to prevent compounding distance
            MyAggroRange += distance;
            MyTarget = target;
           
        }
        
    }

    public void Reset()
    {
        this.MyTarget = null;
        this.MyAggroRange = initAggroRange;
        this.health.MyCurrentValue = this.health.MyMaxValue;
        //this.MyHealth.MyCurrentValue = this.MyHealth.MyMaxValue; // Why did he use this?
        //OnHealthChanged(health.MyCurrentValue);
    }
}
