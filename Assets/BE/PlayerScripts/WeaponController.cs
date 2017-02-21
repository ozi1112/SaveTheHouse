using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class WeaponController : MBSingleton<WeaponController> 
{

	AudioClip clipShoot, clipReload;

	public enum WeaponState
	{
		Idle,
		Shooting,
		Reloading
	}

	public WeaponState weaponState
	{
		get {
			return _weaponState;
		}
		set{
			_weaponState = value;
			GUIWeapon.instance.UpdateWeaponState (_weaponState);
		}
	}

	WeaponState _weaponState = WeaponState.Idle;

	bool stopShootingRequest = false;

	int currentAmmo = 100;
	int maxAmmo = 100;




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

	public void SetWeaponParams(int maxAmmo, float reloadTime, int power, bool continousFire, int shootPerSecond, 
								AudioClip clipShoot = null, AudioClip clipReload = null)
	{
		this.maxAmmo = maxAmmo;
		this.currentAmmo = maxAmmo;
		this.reloadTime = reloadTime;
		this.power = power;
		this.continousFire = continousFire;
		this.shootPerSecond = shootPerSecond;
		this.clipReload = clipReload;
		this.clipShoot = clipShoot;
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
			yield return new WaitForSeconds (1/shootPerSecond);
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
	}

	bool IsFullAmmo()
	{
		return currentAmmo == maxAmmo;
	}


}
