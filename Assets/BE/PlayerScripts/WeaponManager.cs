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
}

public class WeaponManager : MBSingleton<WeaponManager> {

	public enum WeaponType
	{
		Pistol,
		Uzi,
		Last
	}

	Weapon[] weaponTable;
	WeaponType _activeWeapon;

	WeaponType activeWeapon
	{
		get{
			return _activeWeapon;
		}
		set{
			_activeWeapon = value;
            //update GUI
            if (GUIWeapon.instance != null)
            {
                GUIWeapon.instance.WeaponChange(_activeWeapon);
            }
            else
            {
                Debug.Log("Null GUIWeapon");
            }
            
		}
	}


	public void SwitchWeapon(WeaponType weaponType)
	{
		Weapon weapon = weaponTable [(int)weaponType];
		if (weapon.available) {
			activeWeapon = weaponType;
			GetComponent<WeaponController> ().SetWeaponParams (weapon.maxAmmo, 
				weapon.reloadTime, 
				weapon.power,
				weapon.continousFire,
				weapon.shootPerSecond
			);
		}
	}

	public void AtivateWeapon(WeaponType weaponType)
	{
		activeWeapon = weaponType;
		Weapon weapon = weaponTable [(int)weaponType];
		weapon.available = true;
	}

	// Use this for initialization
	void Start ()
	{
		weaponTable = new Weapon[(int)WeaponType.Last];

		weaponTable[(int)WeaponType.Pistol] = 
			new Weapon{ 
			name = "Pistol",
			maxAmmo=100, 
			reloadTime=1.0f, 
			power=1, 
			continousFire=false,
			shootPerSecond=1,
			available=true
			};

		weaponTable[(int)WeaponType.Uzi] = 
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
