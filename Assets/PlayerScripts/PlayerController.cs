using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WeaponBase))]
[RequireComponent(typeof(WeaponManager))]

public class PlayerController : MonoBehaviour, IHealth
{
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) 
		{
			StartShooting ();
		}
		else if(Input.GetMouseButtonUp (0))
		{
			StopShooting ();
		}
	}
		

	void SetActiveWeapon(WeaponBase weapon)
	{
		
	}

	void StartShooting()
	{
		GetComponent<WeaponBase> ().StartShooting ();
	}

	void StopShooting()
	{
		GetComponent<WeaponBase> ().StopShooting ();
	}

	#region IHealth implementation
	public void Die ()
	{
		throw new System.NotImplementedException ();
	}
	#endregion
}
