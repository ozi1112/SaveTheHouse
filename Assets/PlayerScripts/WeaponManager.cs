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

public class WeaponManager : MonoBehaviour {

	public enum Weapons
	{
		Standard,
		Last
	}

	Weapon[] weaponTable;

	void SwitchWeapon(Weapons weaponType)
	{
		Weapon weapon = weaponTable [(int)weaponType];
		if (weapon.available) {
			GetComponent<WeaponController> ().SetWeaponParams (weapon.maxAmmo, 
				weapon.reloadTime, 
				weapon.power,
				weapon.continousFire,
				weapon.shootPerSecond);
		}
	}

	void AtivateWeapon(Weapons weaponType)
	{
		Weapon weapon = weaponTable [(int)weaponType];
		weapon.available = true;
	}

	// Use this for initialization
	void Start () 
	{
		weaponTable = new Weapon[Weapons.Last];

		weaponTable[(int)Weapons.Standard] = 
			new Weapon{ 
			name = "",
			maxAmmo=100, 
			reloadTime=1.0f, 
			power=1, 
			continousFire=true,
			shootPerSecond=2,
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
