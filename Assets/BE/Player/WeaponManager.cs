using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
	Pistol,
	Uzi,
	Last
}

public class WeaponParams
{
	public int maxAmmo;
	public float reloadTime;
	public int power;
	public int shootPerSecond;
	public int cost;
	bool continousFire;

	public WeaponParams(
	int maxAmmo, 
	float reloadTime, 
	int power, 
	int shootPerSecond,
	int cost,
	bool continousFire
	)
	{
		this.maxAmmo = maxAmmo;
		this.reloadTime = reloadTime;
		this.power = power;
		this.shootPerSecond = shootPerSecond;
		this.cost = cost;
		this.continousFire = continousFire;
	}
}

public abstract class Weapon
{
	public WeaponType type; 
	/// <summary>
	/// Weapon name - should be the same as sprite and audioName.
	/// icon - images / 'name'
	/// audioReload - sounds / 'name'Reload
	/// audioShoot - sounds / 'name'Shoot
	/// </summary>
	public string name;
	public int maxAmmo;
	public float reloadTime;
	public int power;
	public bool continousFire;
	public float shootPerSecond;
	
	/// <summary>
	/// Use Activate() to make weapon possible to use
	/// </summary>
	public bool active = false;

	public int price;

	Sprite _icon;

	public WeaponParams[] upgradeTable;

	int upgradeLvl = 0;

	public Sprite icon
	{
		get
		{
			if(_icon == null)
			{
				_icon = Resources.Load<Sprite>(string.Format("images/{0}", name));
				if(_icon == null)
				{
					Debug.LogError("NULL resource not found" + string.Format("images/{0}", name));
				}
			}
			return _icon;
		}
	}

	AudioClip _audioReload;

	public AudioClip audioReload
	{
		get
		{
			if(_audioReload == null)
			{
				_audioReload = Resources.Load(string.Format("sounds/{0}Reload", name)) as AudioClip;
			}
			return _audioReload;
		}
	}

	AudioClip _audioShoot;

	public AudioClip audioShoot
	{
		get
		{
			if(_audioShoot == null)
			{
				_audioShoot = Resources.Load(string.Format("sounds/{0}Shoot", name)) as AudioClip;
			}
			return _audioShoot;
		}
	}

	public Weapon(	WeaponType type, 
					string name, 
					int maxAmmo, 
					float reloadTime, 
					int power, 
					bool continousFire, 
					float shootPerSecond, 
					bool active, 
					int price )
	{
		
			this.type = type;
			this.name = name;
			this.maxAmmo = maxAmmo;
			this.reloadTime = reloadTime; 
			this.power = power;
			this.continousFire = continousFire;
			this.shootPerSecond = shootPerSecond;
			this.active = active;
			this.price = price;

			upgradeTable = new WeaponParams[5];
			upgradeTable[0] = new WeaponParams(20, 1, 1, 2, 100, true);
			upgradeTable[1] = new WeaponParams(20, 1, 1, 2, 100, true);
			upgradeTable[2] = new WeaponParams(20, 1, 1, 2, 100, true);
			upgradeTable[3] = new WeaponParams(20, 1, 1, 2, 100, true);
			upgradeTable[4] = new WeaponParams(20, 1, 1, 2, 100, true);
	}

	WeaponParams GetWeaponParams()
	{
		return upgradeTable[upgradeLvl];
	}

	bool UpgradeWeapon()
	{
		
		if(upgradeLvl  == upgradeTable.Length - 1)
		{
			return false;
		}
		else
		{
			upgradeLvl += 1;
			return true;
		}
			
	}

	
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
		upgradeTable[0] = new WeaponParams(20, 1, 1, 2, 100, false);
		upgradeTable[1] = new WeaponParams(20, 1, 1, 2, 100, false);
		upgradeTable[2] = new WeaponParams(20, 1, 1, 2, 100, false);
		upgradeTable[3] = new WeaponParams(20, 1, 1, 2, 100, false);
		upgradeTable[4] = new WeaponParams(20, 1, 1, 2, 100, false);
	}
}

[RequireComponent(typeof(WeaponController))]
public class WeaponManager : MBSingleton<WeaponManager> 
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
		if (weapon.active) {
			currentWeapon.val = weaponType;
			GetComponent<WeaponController> ().SetWeaponParams 
			(	weapon.maxAmmo, 
				weapon.reloadTime, 
				weapon.power,
				weapon.continousFire,
				weapon.shootPerSecond
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
		weapon.active = true;
	}

	// Use this for initialization
	void Awake ()
	{
		weaponTable = new Weapon[(int)WeaponType.Last];

		weaponTable[(int)WeaponType.Pistol] = new Pistol();

		weaponTable[(int)WeaponType.Uzi] = new Uzi();
	}

	public Weapon[] GetWeaponTable()
	{
		return weaponTable;
	}
}
