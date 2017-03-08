using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
/// Class controls target healh and calls Die function if dead.
///</summary>
public class Health : MonoBehaviour 
{
	///<summary>
	/// Maximum possible health.
	///</summary>
	public EventProperty<int> maximum = new EventProperty<int>(100);
	///<summary>
	/// Current health.
	///</summary>
	public EventProperty<int> current = new EventProperty<int>(100);

	///<summary>
	/// Deal damage.
	///</summary>
	///<returns>
	/// True if target dies, otherwise false.
	///</returns>
	public bool Loss(int value)
	{
		bool retVal = false;
		current.val -= value;
		if (current.val < 0) 
		{
			retVal = true;
			current.val = 0;
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
		current.val += value;
		if (current.val > maximum.val)
			current.val = maximum.val;
	}

}
