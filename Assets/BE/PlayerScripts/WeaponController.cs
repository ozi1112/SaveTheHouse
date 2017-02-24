using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(ToggleGroup))]

[System.Serializable]
public class WeaponController : MBSingleton<WeaponController> 
{

	AudioClip clipShoot, clipReload;

	public enum WeaponState
	{
		Idle,
		Shooting,
		Reloading
	}

    WeaponState _weaponState = WeaponState.Idle;
    public WeaponState weaponState
	{
		get {
			return _weaponState;
		}
		set{
			_weaponState = value;

            if (GUIWeapon.instance != null)
            {
                GUIWeapon.instance.UpdateWeaponState(_weaponState);
            }
            else
            {
                Debug.Log("Null GUIWeapon");
            }
		}
	}
    
	[SerializeField]
    int _currentAmmo = 100;
    public int currentAmmo
    {
        get
        {
            return _currentAmmo;
        }
        set
        {
            _currentAmmo = value;

            if (GUIWeapon.instance != null)
            {
                GUIWeapon.instance.WeaponAmmoChange(_currentAmmo, _maxAmmo);
            }
            else
            {
                Debug.Log("Null GUIWeapon");
            }
        }
    }

	[SerializeField]
    int _maxAmmo = 100;
    public int maxAmmo
    {
        get
        {
            return _maxAmmo;
        }
        set
        {
            _maxAmmo = value;

            if (GUIWeapon.instance != null)
            {
                GUIWeapon.instance.WeaponAmmoChange(_currentAmmo, _maxAmmo);
            }
            else
            {
                Debug.Log("Null GUIWeapon");
            }
        }
    }

    bool stopShootingRequest = false;


    float reloadTime = 1;
	/// <summary>
	/// Weapon power - can't be 0.
	/// </summary>
	int power = 1;

	bool continousFire = false;
	int shootPerSecond = 10;


	public void StartShooting()
	{
		stopShootingRequest = false;
		if(weaponState == WeaponState.Idle)
		{
			if (continousFire) {
				StartCoroutine (CoContinousFire());
			} else {
				StartCoroutine (CoShoot());
			}
		}
	}

	public void SetWeaponParams(int maxAmmo, float reloadTime, int power, bool continousFire, int shootPerSecond)
	{
		this.maxAmmo = maxAmmo;
		this.currentAmmo = maxAmmo;
		this.reloadTime = reloadTime;
		this.power = power;
		this.continousFire = continousFire;
		this.shootPerSecond = shootPerSecond;
	}

	public void StopShooting()
	{
		//Request to break shooting loop
		stopShootingRequest = true;
	}

	public void Reload()
	{
		if(!IsFullAmmo())
		{
			StopAllCoroutines ();
			StartCoroutine (CoReload());
			stopShootingRequest = false;
		}
	}

	IEnumerator CoShoot()
	{
		weaponState = WeaponState.Shooting;
		if (currentAmmo > 0) 
		{
			Debug.Log (string.Format("Shoot {0} / {1}", currentAmmo, maxAmmo));
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit[] hitObjects;
			hitObjects = Physics.RaycastAll (ray, 100.0f);
			foreach (RaycastHit hit in hitObjects) 
			{
				Health hp = hit.collider.gameObject.GetComponent<Health> ();
				if (hp != null) 
				{
					hp.Loss (power);
				}
			}

		}

		currentAmmo --;
		if (currentAmmo == 0) 
		{
			yield return CoReload ();
		}


		if (continousFire) {
			yield return new WaitForSeconds (1.0f/shootPerSecond);
		} else {
			weaponState = WeaponState.Idle;
		}
	}

	IEnumerator CoContinousFire()
	{
		Debug.Log ("ContinousFire");
		weaponState = WeaponState.Shooting;
		while (!stopShootingRequest) {
			yield return CoShoot ();
		}

		_weaponState = WeaponState.Idle;
		stopShootingRequest = false;

		yield return 1;
	}


	IEnumerator CoReload()
	{
		Debug.Log (string.Format( "Reload {0} / {1}", currentAmmo, maxAmmo ));
		weaponState = WeaponState.Reloading;
		yield return new WaitForSeconds (reloadTime);
		currentAmmo = maxAmmo;
		weaponState = WeaponState.Idle;
	}

	bool IsFullAmmo()
	{
		return currentAmmo == maxAmmo;
	}


}
