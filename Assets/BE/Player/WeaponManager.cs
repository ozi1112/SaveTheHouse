using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
	Pistol,
	Uzi,
	Last
}



public class Uzi : Weapon
{
	public Uzi() : 
	base (WeaponType.Uzi, "Uzi", 100, 1.0f, 1, true, 1, false, 200)
	{
		upgradeTable = new WeaponParams[5];
		upgradeTable[0] = new WeaponParams(20, 1, 1, 2, 100, true);
		upgradeTable[1] = new WeaponParams(20, 1, 1, 2, 100, true);
		upgradeTable[2] = new WeaponParams(20, 1, 1, 2, 100, true);
		upgradeTable[3] = new WeaponParams(20, 1, 1, 2, 100, true);
		upgradeTable[4] = new WeaponParams(20, 1, 1, 2, 100, true);
	}
}

public class Pistol : Weapon
{
	public Pistol() : 
	base (WeaponType.Pistol, "Pistol", 10, 1.0f, 1, true, 1, true, 200)
	{
		upgradeTable = new WeaponParams[5];
		upgradeTable[0] = new WeaponParams(10, 1, 1, 1, 100, false);
		upgradeTable[1] = new WeaponParams(20, 0.5f, 2, 4, 100, false);
		upgradeTable[2] = new WeaponParams(20, 0.4f, 3, 3, 100, false);
		upgradeTable[3] = new WeaponParams(20, 0.3f, 4, 5, 100, false);
		upgradeTable[4] = new WeaponParams(20, 0.2f, 5, 6, 100, false);
	}
}

[RequireComponent(typeof(WeaponController))]
public partial class WeaponManager : MBSingleton<WeaponManager> 
{
	Weapon[] weaponTable;
	
	///<summary>
	/// Currently active weapon
	///<summary>
	public EventProperty <WeaponType> currentWeapon = new EventProperty <WeaponType>(WeaponType.Pistol);

	
	///<summary>
	/// Switch current weapon 
	///<summary>
	public void UpdateWeapon(WeaponType weaponType)
	{
		Weapon weapon = weaponTable [(int)weaponType];
		if (weapon.m_Active) {
			currentWeapon.val = weaponType;
			GetComponent<WeaponController> ().SetWeaponParams 
			(	weapon.GetWeaponParams().maxAmmo, 
				weapon.GetWeaponParams().reloadTime, 
				weapon.GetWeaponParams().power,
				weapon.m_ContinousFire,
				weapon.GetWeaponParams().shootPerSecond
			);
		}
	}

	/// <summary>
	/// Make weapon possible to switch
	/// </summary>
	/// <param name="weaponType">Type</param>
	public void AtivateWeapon(WeaponType weaponType)
	{
		currentWeapon.val = weaponType;
		Weapon weapon = weaponTable [(int)weaponType];
		weapon.m_Active = true;
	}

	public Weapon GetWeapon(WeaponType weaponType)
	{
		return weaponTable [(int)weaponType];
	}

	// Use this for initialization
	void Awake ()
	{
		weaponTable = new Weapon[(int)WeaponType.Last];

		weaponTable[(int)WeaponType.Pistol] = new Pistol();

		weaponTable[(int)WeaponType.Uzi] = new Uzi();

		UpdateWeapon(WeaponType.Pistol);
	}

	public Weapon[] GetWeaponTable()
	{
		return weaponTable;
	}
}
