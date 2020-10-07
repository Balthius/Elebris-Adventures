using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttackScript: MonoBehaviour 
{
	private Rigidbody2D weaponRigidBody;
	private CircleCollider2D weaponColliderSize;
	private Player parentPlayer;

	private float durationDone;

    private Transform source;

	//private Element elementType;
	//private float elementDamage;
	private float physDamage;

	public void Initialize(int damage, float damageMod,  float duration, bool altAttack, bool empoweredAttack, Transform source )
	{
		weaponRigidBody = GetComponent <Rigidbody2D> ();
		weaponColliderSize = transform.GetComponent <CircleCollider2D> ();
		parentPlayer = FindObjectOfType<Player> ();
		this.physDamage = damage * damageMod;
        this.source = source;

		if (altAttack)
		{
			IncreaseRadius ();
		}
		if(empoweredAttack)
		{
			IncreaseRadius ();
		}

		durationDone = Time.time + .3f;
	}

	private void FixedUpdate()
	{
		if (Time.time >= durationDone)
		{
				Destroy (gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "Enemy")
		{
            Character c = col.GetComponentInParent<Character>();
            c.TakeDamage(physDamage, source);
		}
	}

	public void IncreaseRadius()
	{
		weaponColliderSize.transform.localScale *= 1.3f;// Was .radius, wouldn't have affected visual, amy revert if I set up different animations
	}
}
