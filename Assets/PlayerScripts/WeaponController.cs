using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour 
{
	enum WeaponState
	{
		Idle,
		Shooting,
		Reloading
	}

	WeaponState weaponState = WeaponState.Idle;
	bool stopShooting = false;

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
		if(weaponState == WeaponState.Idle)
		{
			if (continousFire) {
				StartCoroutine (ContinousFire());
			} else {
				StartCoroutine (Shoot());
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
		stopShooting = true;
	}

	IEnumerator Shoot()
	{
		weaponState = WeaponState.Shooting;
		if (currentAmmo > 0) {
			Debug.Log (string.Format("Shoot {0} / {1}", currentAmmo, maxAmmo));
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit[] hitObjects;
			hitObjects = Physics.RaycastAll (ray, 100.0f);
			foreach (RaycastHit hit in hitObjects) {
				Health hp = hit.collider.gameObject.GetComponent<Health> ();
				if (hp != null) {
					hp.Loss (power);
				}
			}

		}

		currentAmmo --;
		if (currentAmmo == 0) {
			yield return Reload ();
		}


		if (continousFire) {
			yield return new WaitForSeconds (1/shootPerSecond);
		} else {
			weaponState = WeaponState.Idle;
		}
	}

	IEnumerator ContinousFire()
	{
		Debug.Log ("ContinousFire");
		weaponState = WeaponState.Shooting;
		while (!stopShooting) {
			yield return Shoot ();
		}

		weaponState = WeaponState.Idle;
		stopShooting = false;

		yield return 1;
	}


	IEnumerator Reload()
	{
		Debug.Log (string.Format( "Reload {0} / {1}", currentAmmo, maxAmmo ));
		weaponState = WeaponState.Reloading;
		yield return new WaitForSeconds (reloadTime);
		currentAmmo = maxAmmo;
	}


}
