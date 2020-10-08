using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void HealthChanged(float health); //Has to do with enemy unit frame video 5.2 stopped at 23:30

public class NPC : Character
{
	
    public event HealthChanged healthChanged; //Has to do with enemy unit frame video 5.2
    public virtual void DeSelect()
	{
		
	} 

	public virtual Transform Select()
	{
		return hitBox;
	}

    public void OnHealthChanged(float health)
    {
        if(healthChanged != null)
        {
            healthChanged(health); //Has to do with enemy unit frame video 5.2
        }
    }
}
