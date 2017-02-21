using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WeaponController))]
[RequireComponent(typeof(WeaponManager))]

public class PlayerController : MBSingleton<PlayerController>, IHealth
{
	int currentMoney = 100;

	public int CurrentMoney
	{
		get {
			return currentMoney;
		}
		private set{
			currentMoney = value;
			GUIPlayer.instance.MoneyChange(currentMoney);
		}
	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			StartShooting ();
		}
		else if(Input.GetMouseButtonUp (0))
		{
			StopShooting ();
		}
	}

	public void EarnMoney(int value)
	{
		currentMoney += value;
	}

	public bool SpendMoney(int value)
	{
		bool retVal = (currentMoney - value) >= 0;
		if (retVal) {
			currentMoney -= value;
		}
		return retVal;
	}
		

	public void SetActiveWeapon(WeaponManager.Weapons weapon)
	{
		WeaponManager.instance.SwitchWeapon (weapon);
	}

	public void StartShooting()
	{
		WeaponController.instance.StartShooting ();
	}

	public void StopShooting()
	{
		WeaponController.instance.StopShooting ();
	}

	public void Reload()
	{
		WeaponController.instance.Reload ();
	}

	#region IHealth implementation
	public void Die ()
	{
		//throw new System.NotImplementedException ();
	}
	#endregion
}
