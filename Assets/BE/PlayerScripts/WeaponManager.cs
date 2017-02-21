using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[RequireComponent(typeof(WeaponController))]
public class Weapon
{
	public string name;
	public int maxAmmo;
	public float reloadTime;
	public int power;
	public bool continousFire;
	public int shootPerSecond;
	/// <summary>
	/// Bought?
	/// </summary>
	public bool available;

	//TODO sound
	//TODO image
}

public class WeaponManager : MBSingleton<WeaponManager> {

	public enum Weapons
	{
		Pistol,
		Uzi,
		Last
	}

	Weapon[] weaponTable;

	public void SwitchWeapon(Weapons weaponType)
	{
		Weapon weapon = weaponTable [(int)weaponType];
		if (weapon.available) {
			GetComponent<WeaponController> ().SetWeaponParams (weapon.maxAmmo, 
				weapon.reloadTime, 
				weapon.power,
				weapon.continousFire,
				weapon.shootPerSecond);
			//TODO GUI weapon change & display graphic
		}
	}

	public void AtivateWeapon(Weapons weaponType)
	{
		Weapon weapon = weaponTable [(int)weaponType];
		weapon.available = true;
	}

	// Use this for initialization
	void Start ()
	{
		weaponTable = new Weapon[(int)Weapons.Last];

		weaponTable[(int)Weapons.Pistol] = 
			new Weapon{ 
			name = "Pistol",
			maxAmmo=100, 
			reloadTime=1.0f, 
			power=1, 
			continousFire=false,
			shootPerSecond=1,
			available=true
			};

		weaponTable[(int)Weapons.Uzi] = 
			new Weapon{ 
			name = "Uzi",
			maxAmmo = 100, 
			reloadTime = 1.0f, 
			power = 1, 
			continousFire = true,
			shootPerSecond = 4,
			available=true
		};
	}

	public Weapon[] GetWeaponTable()
	{
		return weaponTable;
	}

	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
