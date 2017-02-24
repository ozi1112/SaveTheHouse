using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyController))]


///<summary>
/// Class controls target healh and calls Die function if dead.
///</summary>
public class Health : MonoBehaviour 
{
	///<summary>
	/// Maximum possible health.
	///</summary>
	public int maximum = 100;
	///<summary>
	/// Current health.
	///</summary>
	public int current = 100;

	///<summary>
	/// Deal damage.
	///</summary>
	///<returns>
	/// True if target dies, otherwise false.
	///</returns>
	public bool Loss(int value)
	{
		bool retVal = false;
		current -= value;
		if (current < 0) 
		{
			retVal = true;
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
		return retVal;


	}

	///<summary>
	/// Add health.
	///</summary>
	/// <param name="value">
	/// Health to add.
	/// </param>
	public void Gain(int value)
	{
		current += value;
		if (current > maximum)
			current = maximum;
	}


}
