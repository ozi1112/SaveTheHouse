using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyController))]

public class Health : MonoBehaviour 
{

	public int maximum = 100;
	public int current = 100;

	public void Loss(int value)
	{
		current -= value;
		if (current < 0) 
		{
			current = 0;
			IHealth iHealth = GetComponent<IHealth> ();
			if(iHealth != null)
			{
				iHealth.Die ();
			}
			else
			{
				Debug.Log ("NULL IHealth");
			}
		}


	}

	public void Gain(int value)
	{
		current += value;
		if (current > maximum)
			current = maximum;
	}


}
