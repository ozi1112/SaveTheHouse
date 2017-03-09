using UnityEngine;

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

public class Weapon
{
	public WeaponType type; 
	/// <summary>
	/// Weapon name - should be the same as sprite and audioName.
	/// icon - images / 'name'
	/// audioReload - sounds / 'name'Reload
	/// audioShoot - sounds / 'name'Shoot
	/// </summary>
	public string m_Name;
	public int m_MaxAmmo;
	public float m_ReloadTime;
	public int m_Power;
	public bool m_ContinousFire;
	public float m_ShootPerSecond;
	
	/// <summary>
	/// Use Activate() to make weapon possible to use
	/// </summary>
	public bool m_Active = false;

	public int m_Price;

	Sprite _icon;

	public WeaponParams[] upgradeTable;

	int upgradeLvl = 0;

	public Sprite icon
	{
		get
		{
			if(_icon == null)
			{
				_icon = Resources.Load<Sprite>(string.Format("images/{0}", m_Name));
				if(_icon == null)
				{
					Debug.LogError("NULL resource not found" + string.Format("images/{0}", m_Name));
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
				_audioReload = Resources.Load(string.Format("sounds/{0}Reload", m_Name)) as AudioClip;
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
				_audioShoot = Resources.Load(string.Format("sounds/{0}Shoot", m_Name)) as AudioClip;
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
			this.m_Name = name;
			this.m_MaxAmmo = maxAmmo;
			this.m_ReloadTime = reloadTime; 
			this.m_Power = power;
			this.m_ContinousFire = continousFire;
			this.m_ShootPerSecond = shootPerSecond;
			this.m_Active = active;
			this.m_Price = price;

			upgradeTable = new WeaponParams[5];
			upgradeTable[0] = new WeaponParams(20, 1, 1, 2, 100, true);
			upgradeTable[1] = new WeaponParams(20, 1, 1, 2, 100, true);
			upgradeTable[2] = new WeaponParams(20, 1, 1, 2, 100, true);
			upgradeTable[3] = new WeaponParams(20, 1, 1, 2, 100, true);
			upgradeTable[4] = new WeaponParams(20, 1, 1, 2, 100, true);
	}

	public WeaponParams GetWeaponParams()
	{
		return upgradeTable[upgradeLvl];
	}

	public bool UpgradeWeapon()
	{
		
		if((upgradeLvl  == upgradeTable.Length - 1) || !m_Active)
		{
			Debug.Log("UpgradeWeapon False");
			return false;
		}
		else
		{
			Debug.Log("UpgradeWeapon True");
			upgradeLvl += 1;
			WeaponManager.instance.UpdateWeapon(type);
			return true;
		}
	}

	public int GetUpgradeCost()
	{
		//Full upgrade
		if(upgradeLvl  == upgradeTable.Length - 1)
		{
			return 0;
		}
		//Can be upgraded
		else
		{
			return upgradeTable[upgradeLvl + 1].cost;
		}
	}

	public void Activate()
	{
		m_Active = true;
	}

	
}