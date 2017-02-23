using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[RequireComponent(typeof(WeaponController))]
public class Weapon
{
	public WeaponManager.WeaponType type; 
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
	public int shootPerSecond;
	/// <summary>
	/// Use Activate() to make weapon possible to use
	/// </summary>
	public bool active = false;

	public int price;

	Sprite _icon;

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

}

public class WeaponManager : MBSingleton<WeaponManager> 
{

	public enum WeaponType
	{
		Pistol,
		Uzi,
		Last
	}

	Weapon[] weaponTable;
	WeaponType _currentWeapon;


	
	///<summary>
	/// Currently active weapon
	///<summary>
	WeaponType currentWeapon
	{
		get{
			return _currentWeapon;
		}
		set{
			_currentWeapon = value;
            //update GUI
            if (GUIWeapon.instance != null)
            {
                GUIWeapon.instance.WeaponChange(_currentWeapon);
            }
            else
            {
                Debug.Log("Null GUIWeapon");
            }
            
		}
	}


	
	///<summary>
	/// Switch current weapon 
	///<summary>
	public void SwitchWeapon(WeaponType weaponType)
	{
		Weapon weapon = weaponTable [(int)weaponType];
		if (weapon.active) {
			currentWeapon = weaponType;
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
		currentWeapon = weaponType;
		Weapon weapon = weaponTable [(int)weaponType];
		weapon.active = true;
	}

	// Use this for initialization
	void Awake ()
	{
		weaponTable = new Weapon[(int)WeaponType.Last];

		weaponTable[(int)WeaponType.Pistol] = 
			new Weapon{ 
			type = WeaponType.Pistol,
			name = "Pistol",
			maxAmmo=100, 
			reloadTime=1.0f, 
			power=1, 
			continousFire=false,
			shootPerSecond=1,
			active=true,
			price=200
			};

		weaponTable[(int)WeaponType.Uzi] = 
			new Weapon{ 
			type = WeaponType.Uzi,
			name = "Uzi",
			maxAmmo = 100, 
			reloadTime = 1.0f, 
			power = 1, 
			continousFire = true,
			shootPerSecond = 4,
			active=true,
			price=100
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
