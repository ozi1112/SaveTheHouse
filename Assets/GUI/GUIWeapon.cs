using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIWeapon : MBSingleton<GUIWeapon>  {

	public void UpdateWeaponState(WeaponController.WeaponState state)
	{
		
	}

	public void WeaponChange (WeaponManager.WeaponType activeWeapon)
	{
		
	}

	public void WeaponAmmoChange(int current, int capacity)
	{
	}

	public Weapon[] GetWeaponTable()
	{
		return WeaponManager.instance.GetWeaponTable ();
	}
}


