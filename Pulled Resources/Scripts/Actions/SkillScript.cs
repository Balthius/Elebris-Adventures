using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillScript : MonoBehaviour
{
	private Rigidbody2D skillRigidBody;
	private Player parentPlayer;

	//private Element elementType;
	//private float elementDamage;
	private float physDamage;
	private float durationDone;
	private float speed;
	private Vector2 direction;

    private Transform source;

    private bool targetHit = false;

	public void Initialize(int damage, float damageMod, float skillSpeed, float speedMod, float duration,float durationMod, Vector2 directionCast)
	{
		skillRigidBody = GetComponent <Rigidbody2D> ();
		parentPlayer = FindObjectOfType<Player> ();
		this.physDamage = damage * damageMod;
		this.durationDone = Time.time + (duration * durationMod);
		this.speed = skillSpeed * speedMod;
		this.direction = directionCast;
	}

	private void FixedUpdate()
	{
		if (!targetHit) 
		{
			skillRigidBody.velocity = direction.normalized * speed;
		}
		if (Time.time >= durationDone)
		{
			DestroySkill ();
		}
	}
		
	private void OnTriggerEnter2D(Collider2D col)
	{
		
		if(col.tag == "Enemy")
		{
			speed = 0;
            Character c = col.GetComponentInParent<Character>();
            c.TakeDamage(physDamage, source);

            skillRigidBody.velocity = Vector2.zero;
			targetHit = true;
			DestroySkill ();
		}
	}

	private void DestroySkill()
	{
		Destroy (gameObject);
	}
}
